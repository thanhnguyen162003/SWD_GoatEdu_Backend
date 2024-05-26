using System.Net;
using AutoMapper;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.NoteDto;
using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.Interfaces.NoteInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GoatEdu.Core.Services;

public class NoteService : INoteService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentTime _currentTime;
    private readonly IClaimsService _claimsService;
    private readonly PaginationOptions _paginationOptions;


    public NoteService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentTime currentTime, IClaimsService claimsService, IOptions<PaginationOptions> options)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentTime = currentTime;
        _claimsService = claimsService;
        _paginationOptions = options.Value;
    }


    public async Task<ResponseDto> GetNoteById(Guid id)
    {
        var noteFound = await _unitOfWork.NoteRepository.GetByIdAsync(id);
        if (noteFound != null)
        {
            var mapperNote = _mapper.Map<NoteDetailResponseDto>(noteFound);
            return new ResponseDto(HttpStatusCode.OK, "", mapperNote);
        }
        return new ResponseDto(HttpStatusCode.OK, "Kiếm không thấy :))");
    }

    public async Task<PagedList<NoteResponseDto>> GetNoteByFilter(NoteQueryFilter queryFilter)
    {
        queryFilter.PageNumber = queryFilter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.PageNumber;
        queryFilter.PageSize = queryFilter.PageSize == 0 ? _paginationOptions.DefaultPageSize : queryFilter.PageSize;
        
        var listNote = await _unitOfWork.NoteRepository.GetNoteByFilters(queryFilter);
        
        if (!listNote.Any())
        {
            return new PagedList<NoteResponseDto>(new List<NoteResponseDto>(), 0, 0, 0);
        }
        var mapperList = _mapper.Map<List<NoteResponseDto>>(listNote);
        return PagedList<NoteResponseDto>.Create(mapperList, queryFilter.PageNumber, queryFilter.PageSize);
    }
    
    public async Task<ResponseDto> InsertNote(NoteRequestDto noteRequestDto)
    {
        var note = _mapper.Map<Note>(noteRequestDto);
        note.CreatedAt = _currentTime.GetCurrentTime();
        note.CreatedBy = _claimsService.GetCurrentUserId.ToString();
        note.IsDeleted = false;
        await _unitOfWork.NoteRepository.AddAsync(note);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result > 0)
        {
            return new ResponseDto(HttpStatusCode.OK, "Add Successfully!");
        }
        return new ResponseDto(HttpStatusCode.OK, "Add Failed !");
    }

    public async Task<ResponseDto> DeleteNotes(List<Guid> guids)
    {
        await _unitOfWork.NoteRepository.SoftDelete(guids);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result > 0)
        {
            return new ResponseDto(HttpStatusCode.OK, "Delete Successfully !");
        }
        return new ResponseDto(HttpStatusCode.OK, "Delete Failed !");
    }

    public async Task<ResponseDto> UpdateNote(Guid guid, NoteRequestDto noteRequestDto)
    {
        var note = await _unitOfWork.NoteRepository.GetByIdAsync(guid);
        if (note == null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Kiếm không có thấy");
        }

        note = _mapper.Map(noteRequestDto, note);
        _unitOfWork.NoteRepository.Update(note);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result > 0)
        {
            return new ResponseDto(HttpStatusCode.OK,"Update Successfully!");
        }

        return new ResponseDto(HttpStatusCode.BadRequest, "Update Failed!");
    }
}