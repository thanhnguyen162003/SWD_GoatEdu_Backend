using System.Collections;
using System.Net;
using AutoMapper;
using FluentValidation;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.CloudinaryInterfaces;
using GoatEdu.Core.Interfaces.SubjectInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;
using Microsoft.Extensions.Options;

namespace GoatEdu.Core.Services;

public class SubjectService : ISubjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly PaginationOptions _paginationOptions;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IValidator<SubjectDto> _validator;


    public SubjectService(
        IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> paginationOptions,ICloudinaryService cloudinaryService, IValidator<SubjectDto> validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _paginationOptions = paginationOptions.Value;
        _cloudinaryService = cloudinaryService;
        _validator = validator;
    }

    public async Task<IEnumerable<SubjectDto>> GetAllSubjects(SubjectQueryFilter queryFilter)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;
        
        var listSubject = await _unitOfWork.SubjectRepository.GetAllSubjects(queryFilter);
        
        if (!listSubject.Any())
        {
            return new PagedList<SubjectDto>(new List<SubjectDto>(), 0, 0, 0);
        }
        var mapperList = _mapper.Map<List<SubjectDto>>(listSubject);
        return PagedList<SubjectDto>.Create(mapperList, queryFilter.page_number, queryFilter.page_size);
    }

    public async Task<IEnumerable<SubjectDto>> GetSubjectByClass(SubjectQueryFilter queryFilter, string classes)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;
        
        var listSubject = await _unitOfWork.SubjectRepository.GetSubjectByClass(classes, queryFilter);
        
        if (!listSubject.Any())
        {
            return new PagedList<SubjectDto>(new List<SubjectDto>(), 0, 0, 0);
        }
        var mapperList = _mapper.Map<List<SubjectDto>>(listSubject);
        return PagedList<SubjectDto>.Create(mapperList, queryFilter.page_number, queryFilter.page_size);
    }

    public async Task<SubjectDto> GetSubjectBySubjectId(Guid id)
    {
        return await _unitOfWork.SubjectRepository.GetSubjectBySubjectId(id);
    }

    public async Task<ICollection<ChapterSubjectDto>> GetChaptersBySubject(Guid subjectId)
    {
        ICollection<Chapter> chapters = await _unitOfWork.ChapterRepository.GetChaptersBySubject(subjectId);
        var mapperList = _mapper.Map<ICollection<ChapterSubjectDto>>(chapters);
        return mapperList;
    }

    public async Task<ResponseDto> DeleteSubject(Guid id)
    {
        return await _unitOfWork.SubjectRepository.DeleteSubject(id);
    }
    
    //process image

    public async Task<ResponseDto> UpdateSubject(SubjectDto dto, Guid id)
    {
       
        string imageUrl = null;

        if (dto.Image != null)
        {
            var uploadResult = await _cloudinaryService.UploadAsync(dto.ImageConvert);
            if (uploadResult.Error != null)
            {
                return new ResponseDto(HttpStatusCode.BadRequest, uploadResult.Error.Message);
            }
            imageUrl = uploadResult.Url.ToString();
        }
        
        var updateSubject = new Subject()
        {
            Id = id,
            SubjectName = dto.SubjectName,
            SubjectCode = dto.SubjectCode,
            Information = dto.Information,
            Image = imageUrl,
            Class = dto.Class
        };
        return await _unitOfWork.SubjectRepository.UpdateSubject(updateSubject);
    }

    //process image
    public async Task<ResponseDto> CreateSubject(SubjectDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", errors);
        }
        
        var uploadResult = await _cloudinaryService.UploadAsync(dto.ImageConvert);
        if (uploadResult.Error != null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, uploadResult.Error.Message);
        }
        var newSubject = new Subject
        {
            SubjectName = dto.SubjectName,
            SubjectCode = dto.SubjectCode,
            Information = dto.Information,
            Image = uploadResult.Url.ToString(),
            Class = dto.Class,
            CreatedAt = DateTime.Now,
            IsDeleted = false
        };

        await _unitOfWork.SubjectRepository.CreateSubject(newSubject);

        return new ResponseDto(HttpStatusCode.Created, "Subject created successfully.", newSubject.Id);
    }

    public Task<SubjectDto> GetSubjectBySubjectName(string subjectName)
    {
        return _unitOfWork.SubjectRepository.GetSubjectBySubjectName(subjectName);
    }
}