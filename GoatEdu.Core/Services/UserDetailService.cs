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
        if (!BCrypt.Net.BCrypt.Verify(user.password, userResult.Password))
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Wrong password!");
        }

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
            imageUrl = "https://camo.githubusercontent.com/65eac2eb9bb41fefaa3800d8ee40a7139fc25d9372037550944b5c5fb1e6a6fd/68747470733a2f2f7261772e6769746875622e636f6d2f68617368646f672f6e6f64652d6964656e7469636f6e2d6769746875622f6d61737465722f6578616d706c65732f696d616765732f6769746875622e706e67";
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