using System.Net;
using FluentEmail.Core;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.MailDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.MailInterfaces;
using GoatEdu.Core.Interfaces.UserInterfaces;
using GoatEdu.Core.Models;
using Infrastructure;

namespace GoatEdu.Core.Services;

public class MailService : IMailService
{
    private readonly IFluentEmail _iFluentEmail;
    private readonly IUnitOfWork _unitOfWork;

    public MailService(IFluentEmail iFluentEmail, IUnitOfWork unitOfWork)
    {
        _iFluentEmail = iFluentEmail;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> SendUsingTemplateFromFile(string template, string title, UserMail userMail)
    {
        var response = await _iFluentEmail.To(userMail.Email)
            .Subject(title)
            .UsingTemplateFromFile(template, userMail, true)
            .SendAsync();

        return response.Successful;

    }

    public async Task<ResponseDto> ConfirmEmailComplete(ConfirmMailDto dto)
    {
        User user = await _unitOfWork.UserRepository.GetUserByUserId(dto.id); // Assuming dto.userId is a dif
        if (user == null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest,"User not found");
        }

        var hashedUsername = BCrypt.Net.BCrypt.HashPassword(user.Username);

        // Compare the hashed username with the hashed parameter
        bool isUsernameMatch = BCrypt.Net.BCrypt.Verify(dto.userId, hashedUsername);

        // Compare the hashed password in dto with the hashed password in the database
        bool isPasswordMatch = user.Password.Equals(dto.token);
        if (!isUsernameMatch && !isPasswordMatch)
        {
            return new ResponseDto(HttpStatusCode.BadRequest,"User/Password not right");
        }

        user.EmailVerify = true;
        await _unitOfWork.SaveChangesAsync();
        return new ResponseDto(HttpStatusCode.OK,"Success");
    }
}