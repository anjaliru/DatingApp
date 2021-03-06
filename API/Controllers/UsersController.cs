using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using API.DTOs;
using System.Security.Claims;

namespace API.Controllers
{
[Authorize]
    public class UsersController : BaseApiController{

        private readonly IUserRepository _userRepository;

       private readonly IMapper _mapper;

       private readonly IPhotoService _photoService;

        public UsersController(IUserRepository userRepository,IMapper mapper, IPhotoService photoService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }   


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);

            return Ok(usersToReturn);
        }   

[HttpPut]
public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
{
var username = User.GetUsername();
var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
_mapper.Map(memberUpdateDto, user);

_userRepository.Update(user);

if (await _userRepository.SaveAllAsync()) return NoContent();

return BadRequest("Failed to update user");
}


    [HttpPost("add-photo")]
        public async Task<ActionResul<PhotoDto>> AddPhoto(IFormFile file){
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            var result = await _photoService.AddPhotoAsync(file);

            if(result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo{
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if(user.Photos.Count == 0 ){
                photo.IsMain = true;
            }

            user.Photos.Add(photo);

            if(await _userRepository.SaveAllAsync())
            return _mapper.Map<PhotoDto>(photo);


            return BadRequest("Problem adding photo");
        }
    
    
    [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username) => await _userRepository.GetMemberAsync(username);
    }
}
