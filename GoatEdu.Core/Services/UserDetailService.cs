using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.UserDetailDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.CloudinaryInterfaces;
using GoatEdu.Core.Interfaces.UserDetailInterfaces;
using Infrastructure;

namespace GoatEdu.Core.Services;

public class UserDetailService : IUserDetailService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IClaimsService _claimsService;


    public UserDetailService(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService,IClaimsService claimsService)
    {
        _unitOfWork = unitOfWork;
        _cloudinaryService = cloudinaryService;
        _claimsService = claimsService;
    }

    public async Task<ResponseDto> UpdateProfile(UserUpdateDto user)
    {
        var userId = _claimsService.GetCurrentUserId;
        var userResult = await _unitOfWork.UserRepository.GetUserByUserId(userId);
        if (userResult is null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Somethings has error, We cant find your account");
        }
        
        // if (!BCrypt.Net.BCrypt.Verify(user.password, userResult.Password))
        // {
        //     return new ResponseDto(HttpStatusCode.BadRequest, "Wrong password!");
        // }

        string imageUrl = userResult.Image; // Keep the existing image URL by default
        if (user.Image != null)
        {
            var uploadResult = await _cloudinaryService.UploadAsync(user.Image);
            if (uploadResult.Error != null)
            {
                return new ResponseDto(HttpStatusCode.BadRequest, uploadResult.Error.Message);
            }
            imageUrl = uploadResult.SecureUrl.AbsoluteUri;
        }
        else
        {
            imageUrl = null;
        }
        
        User userUpdate = new User()
        {
            Id = userId,
            Fullname = user.FullName,
            PhoneNumber = user.PhoneNumber,
            Image = imageUrl 
        };
        return await _unitOfWork.UserDetailRepository.UpdateProfile(userUpdate);
    }
    
    public async Task<ResponseDto> UpdatePassword(string oldPassword, string newPassword)
    {
        var userId = _claimsService.GetCurrentUserId;
        var userResult = await _unitOfWork.UserRepository.GetUserByUserId(userId);
        
        if (userResult is null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Somethings has error, We cant find your account");
        }
        
        if (!BCrypt.Net.BCrypt.Verify(oldPassword, userResult.Password))
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Wrong password!");
        }
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
        return await _unitOfWork.UserDetailRepository.UpdatePassword(userId,hashedPassword);
    }
    public async Task<ResponseDto> UpdateNewUser()
    {
        var userId = _claimsService.GetCurrentUserId;
        return await _unitOfWork.UserDetailRepository.UpdateNewUser(userId);
    }

    public Task<ResponseDto> GetSubcription()
    {
        throw new NotImplementedException();
    }

    public Task<Wallet> GetUserWallet()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto> GetTranstractionWallet(Guid walletId)
    {
        throw new NotImplementedException();
    }
}