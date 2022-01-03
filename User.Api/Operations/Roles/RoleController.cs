using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  Miccore.Net.webapi_template.User.Api.Data;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using  Miccore.Net.webapi_template.User.Api.Services.Role;
using  Miccore.Net.webapi_template.User.Api.Services.Role.DomainModels;
using  Miccore.Net.webapi_template.User.Api.Operations.Role.ViewModels;
using  Miccore.Net.webapi_template.User.Api.Operations.Role.Validator;
using AutoMapper;
using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Authorization;
using JWTAuthentication.Models;

namespace  Miccore.Net.webapi_template.User.Api.Operations
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Policy = Policies.Admin)]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        public RoleController(
            IMapper mapper,
            IRoleService roleService
        )
        {
            _roleService = roleService;
            _mapper = mapper;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]    
        [ProducesResponseType(StatusCodes.Status400BadRequest)]   
        [HttpPost]
        public async Task<ActionResult<RoleViewModel>> Create([FromBody] CreateRoleViewModel viewModel)
        {

            try
            {
                var validator = new CreateRoleValidator();

                var validate = validator.Validate(viewModel);

                if(!validate.IsValid){
                    return HandleErrorResponse(HttpStatusCode.BadRequest, validate.ToString());
                }

                var role = _mapper.Map<RoleDomainModel>(viewModel);

                // create new role
                var createdRole = await _roleService.CreateRoleAsync(role);

                // prepare response
                var response = _mapper.Map<RoleViewModel>(createdRole);

                return HandleCreatedResponse(response);
            }
            catch (Exception ex)
            {
                return HandleErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]    
        [ProducesResponseType(StatusCodes.Status400BadRequest)]   
        [HttpGet]
        public async Task<ActionResult<List<RoleViewModel>>> GetAll()
        {
            try
            {
                var roles = await _roleService.GetAllRolesAsync();

                var response = _mapper.Map<List<RoleViewModel>>(roles);

                return HandleSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]    
        [ProducesResponseType(StatusCodes.Status400BadRequest)]   
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleViewModel>> GetById(int id)
        {
            try
            {
                var role = await _roleService.GetRoleAsync(id);
                if(role == null){
                    return HandleErrorResponse(HttpStatusCode.NotFound, "role doesn't exist");
                }
                var response = _mapper.Map<RoleViewModel>(role);

                return HandleSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]    
        [ProducesResponseType(StatusCodes.Status400BadRequest)]   
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // delete existing movie
                await _roleService.DeleteRoleAsync(id);

                return HandleDeletedResponse();
            }
            catch (Exception ex)
            {
                return HandleErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]    
        [ProducesResponseType(StatusCodes.Status400BadRequest)]   
        [HttpPut("{id}")]
        public async Task<ActionResult<RoleViewModel>> Update(int id, [FromBody] RoleViewModel viewModel)
        {
            try
            {
                Contract.Requires(viewModel != null);
            
                // id can be in URL, body, or both
                viewModel.Id = id;

                // map view model to domain model
                var role = _mapper.Map<RoleDomainModel>(viewModel);

                // update existing role
                var updatedrole = await _roleService.UpdateRoleAsync(role);

                // prepare response
                var response = _mapper.Map<RoleViewModel>(updatedrole);

                // 200 response
                return HandleSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}