using AutoMapper;
using Friendbook.Api.Helpers;
using Friendbook.Api.Models;
using Friendbook.Domain;
using Friendbook.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Friendbook.Api.Controllers;

[ApiController]
[Route("auth/[action]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserProfileService _userProfileService;
    private readonly IMapper _mapper;
    private readonly PasswordHasher<UserProfile> _passwordHasher;
        
    public AuthController(IConfiguration configuration, IUserProfileService userProfileService, IMapper mapper)
    {
        _configuration = configuration;
        _userProfileService = userProfileService;
        _mapper = mapper;
        _passwordHasher = new PasswordHasher<UserProfile>();
    }
        
    [HttpPost]
    public ActionResult<AuthenticateUserResponseDto> Login(AuthenticateUserRequestDto dto)
    {
        UserProfile? userProfile = _userProfileService.GetByNickname(dto.Nickname);

        if (_passwordHasher.VerifyHashedPassword(userProfile, userProfile.PasswordHash, dto.Password) 
            != PasswordVerificationResult.Success)
        {
            return BadRequest("Incorrect nickname or password");
        }

        string token = _configuration.GenerateJwtToken(userProfile);

        AuthenticateUserResponseDto responseDto = _mapper.Map<AuthenticateUserResponseDto>(userProfile);
        responseDto.Token = token;

        return responseDto;
    }
    
    [HttpPost]
    public ActionResult<bool> Register(CreateUserProfileDto dto)
    {
        UserProfile? userProfile = _mapper.Map<UserProfile>(dto);
        userProfile.PasswordHash = _passwordHasher.HashPassword(userProfile, dto.Password);
        
        return _userProfileService.Create(userProfile);
    }
}
