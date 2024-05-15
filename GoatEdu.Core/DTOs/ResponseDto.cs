using System.Net;

namespace GoatEdu.Core.DTOs;

public class ResponseDto
{
    public HttpStatusCode? Status { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }

    public ResponseDto()
    {
    }
    public ResponseDto(HttpStatusCode? status, string? message, object? data)
    {
        Status = status;
        Message = message;
        Data = data;
    }

    public ResponseDto(HttpStatusCode? status, string? message)
    {
        Status = status;
        Message = message;
    }
}