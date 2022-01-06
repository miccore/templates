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
using Microsoft.Extensions.DependencyInjection;
using Miccore.Net.webapi_template.User.Api.Entities;

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
        [HttpPost(Name = nameof(CreateRole))]
        public async Task<ActionResult<RoleViewModel>> CreateRole([FromBody] CreateRoleViewModel viewModel)
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
        [HttpGet(Name = nameof(GetAllRoles))]
        public async Task<ActionResult<PaginationEntity<RoleViewModel>>> GetAllRoles([FromQuery] UrlQueryParameters urlQueryParameters)
        {
            try
            {
                // get all roles
                var roles = await _roleService.GetAllRolesAsync(urlQueryParameters.Page, urlQueryParameters.Limit);
                // mapp response to view model
                var response = _mapper.Map<PaginationEntity<RoleViewModel>>(roles);
                // add previous route link if exist
                if (response.CurrentPage > 1)
                {
                    // generate previous route
                    var prevRoute = Url.RouteUrl(nameof(GetAllRoles), new { limit = urlQueryParameters.Limit, page = urlQueryParameters.Page - 1 });
                    // add the route to dictionnary links
                    response.Prev= prevRoute;

                }
                //add next route link if exist
                if (response.CurrentPage < response.TotalPages)
                {
                    // generate next route
                    var nextRoute = Url.RouteUrl(nameof(GetAllRoles), new { limit = urlQueryParameters.Limit, page = urlQueryParameters.Page + 1 });
                    // add the route to dictionnary links
                    response.Next = nextRoute;
                }
                // return success response
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
        [HttpGet(template:"{id}", Name = nameof(GetRoleById))]
        public async Task<ActionResult<RoleViewModel>> GetRoleById(int id)
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
        [HttpDelete(template:"{id}", Name = nameof(DeleteRole))]
        public async Task<IActionResult> DeleteRole(int id)
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
        [HttpPut(template:"{id}", Name = nameof(UpdateRole))]
        public async Task<ActionResult<RoleViewModel>> UpdateRole(int id, [FromBody] RoleViewModel viewModel)
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