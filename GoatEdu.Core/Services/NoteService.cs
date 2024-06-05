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
        if (noteFound == null) return new ResponseDto(HttpStatusCode.NotFound, "Kiếm không thấy :))");
        
        var mapperNote = _mapper.Map<NoteDto>(noteFound);
        return new ResponseDto(HttpStatusCode.OK, "", mapperNote);
    }

    public async Task<PagedList<NoteDto>> GetNoteByFilter(NoteQueryFilter queryFilter)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;
        
        var userId = _claimsService.GetCurrentUserId;
        var listNote = await _unitOfWork.NoteRepository.GetNoteByFilters(userId, queryFilter);
        
        if (!listNote.Any()) return new PagedList<NoteDto>(new List<NoteDto>(), 0, 0, 0);
        
        var mapperList = _mapper.Map<List<NoteDto>>(listNote);
        return PagedList<NoteDto>.Create(mapperList, queryFilter.page_number, queryFilter.page_size);
    }
    
    public async Task<ResponseDto> InsertNote(NoteDto noteRequestDto)
    {
        var note = _mapper.Map<Note>(noteRequestDto);
        note.CreatedAt = _currentTime.GetCurrentTime();
        note.CreatedBy = _claimsService.GetCurrentFullname;
        note.IsDeleted = false;
        
        await _unitOfWork.NoteRepository.AddAsync(note);
        var result = await _unitOfWork.SaveChangesAsync();
        
        return result > 0 ? new ResponseDto(HttpStatusCode.OK, "Add Successfully!") : new ResponseDto(HttpStatusCode.BadRequest, "Add Failed !");
    }

    public async Task<ResponseDto> DeleteNotes(List<Guid> guids)
    {
        await _unitOfWork.NoteRepository.SoftDelete(guids);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? new ResponseDto(HttpStatusCode.OK, "Delete Successfully !") : new ResponseDto(HttpStatusCode.BadRequest, "Delete Failed !");
    }

    public async Task<ResponseDto> UpdateNote(Guid guid, NoteDto noteRequestDto)
    {
        var note = await _unitOfWork.NoteRepository.GetByIdAsync(guid);
        
        if (note == null) return new ResponseDto(HttpStatusCode.NotFound, "Kiếm không có thấy");
        
        note = _mapper.Map(noteRequestDto, note);
        note.UpdatedBy = _claimsService.GetCurrentFullname;
        note.UpdatedAt = _currentTime.GetCurrentTime();
        
        _unitOfWork.NoteRepository.Update(note);
        var result = await _unitOfWork.SaveChangesAsync();
        
        return result > 0 ? new ResponseDto(HttpStatusCode.OK,"Update Successfully!") : new ResponseDto(HttpStatusCode.BadRequest, "Update Failed!");
    }
}