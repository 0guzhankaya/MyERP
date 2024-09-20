using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyERP.API.Filters;
using MyERP.Core.DTOs;
using MyERP.Core.DTOs.UpdateDTOs;
using MyERP.Core.Models;
using MyERP.Core.Services;
using MyERP.Service.Hashing;

namespace MyERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : CustomBaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            // url/api/users

            var users = _userService.GetAll();
            var dtos = _mapper.Map<List<UserDto>>(users).OrderBy(x => x.Name).ToList();

            return CreateActionResult(CustomResponseDto<List<UserDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<User>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // url/api/users/{id}

            var user = await _userService.GetByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(user);

            return CreateActionResult(CustomResponseDto<UserDto>.Success(200, userDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<User>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            // get user from token
            int userId = 1;

            var user = await _userService.GetByIdAsync(id);
            user.UpdatedBy = userId;

            _userService.ChangeStatus(user);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Not Found :("));
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserDto userDto)
        {
            // get user from token
            int userId = 1;

            var processedEntity = _mapper.Map<User>(userDto);

            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

            // password
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(userDto.Password, out passwordHash, out passwordSalt);
            processedEntity.PasswordHash = passwordHash;
            processedEntity.PasswordSalt = passwordSalt;

            var user = await _userService.AddAsync(processedEntity);
            var userResponseDto = _mapper.Map<UserDto>(user);

            return CreateActionResult(CustomResponseDto<UserDto>.Success(201, userResponseDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateDto userDto)
        {
            // get user from token
            var userId = 1;

            var currentUser = await _userService.GetByIdAsync(userId);
            currentUser.UpdatedBy = userId;
            currentUser.Name = userDto.Name;

            _userService.ChangeStatus(currentUser);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            Token token = await _userService.Login(userLoginDto);

            if (token == null)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(401, "Error :("));
            }

            return CreateActionResult(CustomResponseDto<Token>.Success(200, token));
        }
    }
}
