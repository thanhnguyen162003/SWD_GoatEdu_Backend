using System.Net;
using AutoMapper;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.CloudinaryInterfaces;
using GoatEdu.Core.Interfaces.SubjectInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace GoatEdu.Core.Services;

public class SubjectService : ISubjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly PaginationOptions _paginationOptions;
    private readonly ICloudinaryService _cloudinaryService;

    public SubjectService(
        IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> paginationOptions,ICloudinaryService cloudinaryService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _paginationOptions = paginationOptions.Value;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<ICollection<SubjectResponseDto>> GetAllSubjects(SubjectQueryFilter queryFilter)
    {
        queryFilter.PageNumber = queryFilter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.PageNumber;
        queryFilter.PageSize = queryFilter.PageSize == 0 ? _paginationOptions.DefaultPageSize : queryFilter.PageSize;
        
        var listSubject = await _unitOfWork.SubjectRepository.GetAllSubjects(queryFilter);
        
        if (!listSubject.Any())
        {
            return new PagedList<SubjectResponseDto>(new List<SubjectResponseDto>(), 0, 0, 0);
        }
        var mapperList = _mapper.Map<List<SubjectResponseDto>>(listSubject);
        return PagedList<SubjectResponseDto>.Create(mapperList, queryFilter.PageNumber, queryFilter.PageSize);
    }

    public async Task<SubjectResponseDto> GetSubjectBySubjectId(Guid id)
    {
        return await _unitOfWork.SubjectRepository.GetSubjectBySubjectId(id);
    }

    public async Task<ResponseDto> DeleteSubject(Guid id)
    {
        return await _unitOfWork.SubjectRepository.DeleteSubject(id);
    }
    
    //process image

    public async Task<ResponseDto> UpdateSubject(SubjectCreateDto dto)
    {
        string imageUrl = null;

        if (dto.image != null)
        {
            var uploadResult = await _cloudinaryService.UploadAsync(dto.image);
            if (uploadResult.Error != null)
            {
                return new ResponseDto(HttpStatusCode.BadRequest, uploadResult.Error.Message);
            }
            imageUrl = uploadResult.Url.ToString();
        }
        
        var updateSubject = new Subject()
        {
            Id = dto.Id,
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
        var uploadResult = await _cloudinaryService.UploadAsync(dto.image);
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

    public Task<SubjectResponseDto> GetSubjectBySubjectName(string subjectName)
    {
        return _unitOfWork.SubjectRepository.GetSubjectBySubjectName(subjectName);
    }
}