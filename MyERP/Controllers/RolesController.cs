using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyERP.API.Filters;
using MyERP.Core.DTOs;
using MyERP.Core.DTOs.UpdateDTOs;
using MyERP.Core.Models;
using MyERP.Core.Services;

namespace MyERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : CustomBaseController
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RolesController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            // url/api/roles

            var roles = _roleService.GetAll();
            var dtos = _mapper.Map<List<RoleDto>>(roles).OrderBy(x => x.Name).ToList();

            return CreateActionResult(CustomResponseDto<List<RoleDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Role>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // url/api/roles/{id}
            var roles = await _roleService.GetByIdAsync(id);
            var rolesDto = _mapper.Map<RoleDto>(roles);

            return CreateActionResult(CustomResponseDto<RoleDto>.Success(200, rolesDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Role>))]
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            // get user from token
            int userId = 1;

            var roles = await _roleService.GetByIdAsync(id);
            roles.UpdatedBy = userId;

            _roleService.ChangeStatus(roles);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Not Found :("));
        }

        [HttpPost]
        public async Task<IActionResult> Save(RoleDto roleDto)
        {
            // get user from token
            int userId = 1;
            
            var processedEntity = _mapper.Map<Role>(roleDto);
            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

            var role = await _roleService.AddAsync(processedEntity);
            var roleResponseDto = _mapper.Map<RoleDto>(role);

            return CreateActionResult(CustomResponseDto<RoleDto>.Success(201, roleDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(RoleUpdateDto roleDto)
        {
            // get user from token
            var userId = 1;

            var currentRole = await _roleService.GetByIdAsync(userId);

            currentRole.UpdatedBy = userId;
            currentRole.Name = roleDto.Name;

            _roleService.Update(currentRole);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
