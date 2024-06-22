using System.Net;
using AutoMapper;
using FluentValidation;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.TheoryDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.CloudinaryInterfaces;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.Interfaces.TheoryInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;
using Microsoft.Extensions.Options;

namespace GoatEdu.Core.Services;

public class TheoryService : ITheoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentTime _currentTime;
    private readonly IMapper _mapper;
    private readonly IValidator<TheoryDto> _validator;
    private readonly IGoogleCloudService _cloudService;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly PaginationOptions _paginationOptions;

    public TheoryService(IUnitOfWork unitOfWork, ICurrentTime currentTime, IMapper mapper,
        IValidator<TheoryDto> validator, IGoogleCloudService cloudService, ICloudinaryService cloudinaryService,
        IOptions<PaginationOptions> options)
    {
        _unitOfWork = unitOfWork;
        _currentTime = currentTime;
        _mapper = mapper;
        _validator = validator;
        _cloudService = cloudService;
        _cloudinaryService = cloudinaryService;
        _paginationOptions = options.Value;
    }

    public async Task<ResponseDto> CreateTheory(TheoryDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", errors);
        }

        var theory = _mapper.Map<Theory>(dto);

        if (dto.FormFile != null)
        {
            var form = await _cloudService.UploadFileAsync(dto.FormFile, ObjectName.THEORY);
            theory.File = form;
        }

        if (dto.ImageFile != null)
        {
            var image = await _cloudinaryService.UploadAsync(dto.ImageFile);
            if (image.Error != null)
            {
                return new ResponseDto(HttpStatusCode.BadRequest, image.Error.Message);
            }
            theory.Image = image.Url.ToString();
        }

        theory.CreatedAt = _currentTime.GetCurrentTime();
        theory.IsDeleted = false;

        await _unitOfWork.TheoryRepository.AddAsync(theory);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.Created, "Create Theory Successfully!", theory.Id)
            : new ResponseDto(HttpStatusCode.BadRequest, "Create Theory Failed!");
    }

    public async Task<ResponseDto> UpdateTheory(Guid theoryId, TheoryDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", errors);
        }

        var theory = await _unitOfWork.TheoryRepository.GetByIdAsync(theoryId);

        if (theory is null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Theory not found!");
        }
        
        if (dto.FormFile != null)
        {
            var objectName = ExtractObjectName(theory.File);
            if (objectName is null)
            {
                return new ResponseDto(HttpStatusCode.BadRequest, "File is not found");
            }
            await _cloudService.DeleteFileAsync(objectName);
            var form = await _cloudService.UploadFileAsync(dto.FormFile, ObjectName.THEORY);
            if (form is null)
            {
                return new ResponseDto(HttpStatusCode.BadRequest, "Upload File Failed!");
            }
            theory.File = form;
        }

        if (dto.ImageFile != null)
        {
            var image = await _cloudinaryService.UploadAsync(dto.ImageFile);
            if (image.Error != null)
            {
                return new ResponseDto(HttpStatusCode.BadRequest, image.Error.Message);
            }
            theory.Image = image.Url.ToString();
        }
        
        theory.TheoryName = dto.TheoryName ?? theory.TheoryName;
        theory.TheoryContent = dto.TheoryContent ?? theory.TheoryContent;
        theory.UpdatedAt = _currentTime.GetCurrentTime();
        
        _unitOfWork.TheoryRepository.Update(theory);
        var result = await _unitOfWork.SaveChangesAsync();

        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Update Theory Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Update Theory Failed!");
    }

    public async Task<ResponseDto> DeleteTheory(IEnumerable<Guid> guids)
    {
        await _unitOfWork.TheoryRepository.SoftDelete(guids);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Delete Theory Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Delete Theory Failed!");
    }

    public async Task<TheoryDto?> GetTheoryById(Guid theoryId)
    {
        var theory = await _unitOfWork.TheoryRepository.GetByIdAsync(theoryId);
        var mapper = _mapper.Map<TheoryDto>(theory);
        return mapper;
    }

    public async Task<PagedList<TheoryDto>> GetTheoriesByFilter(Guid? lessonId, TheoryQueryFilter queryFilter)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;
        
        var theories = await _unitOfWork.TheoryRepository.GetTheoryByFilters(lessonId, queryFilter);
        if (!theories.Any()) return new PagedList<TheoryDto>(new List<TheoryDto>(), 0, 0, 0);

        var mapper = _mapper.Map<IEnumerable<TheoryDto>>(theories);
        return PagedList<TheoryDto>.Create(mapper, queryFilter.page_number, queryFilter.page_size);
    }

    private string? ExtractObjectName(string url)
    {
        var prefix = "https://storage.googleapis.com/swd392/";
        if (!url.StartsWith(prefix)) return null;
        var objectName = url.Replace(prefix, "");
        return objectName;
    }
}