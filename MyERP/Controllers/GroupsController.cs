using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class GroupsController : CustomBaseController
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupsController(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var groups = _groupService.GetAll();
            var dtos = _mapper.Map<List<GroupDto>>(groups).OrderBy(x => x.Id).ToList();

            return Ok(CustomResponseDto<List<GroupDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Group>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // url/api/groups/{id}

            var group = await _groupService.GetByIdAsync(id);
            var groupDto = _mapper.Map<GroupDto>(group);

            return Ok(CustomResponseDto<GroupDto>.Success(200, groupDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Group>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            // get user from token
            int userId = 1;
            var group = await _groupService.GetByIdAsync(id);

            group.UpdatedBy = userId; 
            _groupService.ChangeStatus(group);

            return NoContent(); 
        }

        [HttpPost]
        public async Task<IActionResult> Save(GroupDto groupDto)
        {
            // get user from token
            int userId = 1;
            var group = _mapper.Map<Group>(groupDto); // When GroupDto mapping Group, GroupDto was came on UI
            group.CreatedBy = userId;

            var savedGroup = await _groupService.AddAsync(group);
            var groupResponseDto = _mapper.Map<GroupDto>(savedGroup);

            return CreatedAtAction(nameof(GetById), new { id = groupResponseDto.Id }, CustomResponseDto<GroupDto>.Success(201, groupResponseDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(GroupUpdateDto groupDto)
        {
            // get user from token
            int userId = 1; 
            var currentGroup = await _groupService.GetByIdAsync(groupDto.Id);

            currentGroup.UpdatedBy = userId;
            currentGroup.Name = groupDto.Name;

            _groupService.Update(currentGroup);

            return NoContent();
        }
    }
}
