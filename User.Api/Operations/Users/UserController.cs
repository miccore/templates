using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using  Miccore.Net.webapi_template.User.Api.Services.User;
using  Miccore.Net.webapi_template.User.Api.Services.User.DomainModels;
using  Miccore.Net.webapi_template.User.Api.Operations.User.ViewModels;
using  Miccore.Net.webapi_template.User.Api.Operations.User.Validator;
using AutoMapper;
using System.Diagnostics.Contracts;
using  Miccore.Net.webapi_template.User.Api.Services.Role;
using Microsoft.AspNetCore.Authorization;
using JWTAuthentication.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Miccore.Net.webapi_template.User.Api.Entities;

namespace  Miccore.Net.webapi_template.User.Api.Operations
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Policy = Policies.Admin)]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        
        public UserController(
            IMapper mapper,
            IUserService userService,
            IRoleService roleService
        )
        {
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
        }

       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]    
        [ProducesResponseType(StatusCodes.Status400BadRequest)]  
        [HttpPost(template: "login", Name = nameof(LoginUser))]
        [AllowAnonymous]
        public async Task<ActionResult<UserViewModel>> LoginUser([FromBody] LoginUserViewModel loginUser){
            try
            {
               var validator = new LoginUserValidator();

                var validate = validator.Validate(loginUser);

                if(!validate.IsValid){
                    return HandleErrorResponse(HttpStatusCode.BadRequest, validate.ToString());
                }

                var user = _mapper.Map<UserDomainModel>(loginUser);

                var userLogin = await _userService.LoginUserAsync(user);

                var response = _mapper.Map<UserViewModel>(userLogin);

                if(response == null){
                    return HandleErrorResponse(HttpStatusCode.Unauthorized, "PHONE_OR_PASSWORD_INCORRECT");
                }

                LoginUserResponseViewModel userLogged = new LoginUserResponseViewModel();

                Response.Cookies.Append("X-Refresh-Token", userLogin.RefreshToken, new CookieOptions(){HttpOnly = true, SameSite = SameSiteMode.Strict});
                Response.Cookies.Append("X-Access-Token", userLogin.Token, new CookieOptions(){HttpOnly = true, SameSite = SameSiteMode.Strict});
                userLogged.User = response;
                userLogged.Token = userLogin.Token;

                return HandleCreatedResponse(userLogged);
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
        [HttpPost(template:"refresh/token", Name = nameof(UserRefreshToken))]
        [AllowAnonymous]
        public async Task<ActionResult<UserViewModel>> UserRefreshToken(){
            try
            {
                if (!(Request.Cookies.TryGetValue("X-Refresh-Token", out var refreshToken)))
                    return BadRequest();

            
                var User = await _userService.GetSingleByRefreshTokenAsync(refreshToken);

                if(User == null){
                    return HandleErrorResponse(HttpStatusCode.NotFound, "USER_NOT_FOUND");
                }


                var token = _userService.GenerateToken(User);
                var refresh = _userService.GenerateRefreshToken();

                User.RefreshToken = refresh;
                var updated = await _userService.UpdateRefreshTokenAsync(User);

                var response = _mapper.Map<UserViewModel>(updated);
                LoginUserResponseViewModel userLogged = new LoginUserResponseViewModel();

                Response.Cookies.Append("X-Refresh-Token", refresh, new CookieOptions(){HttpOnly = true, SameSite = SameSiteMode.Strict});
                Response.Cookies.Append("X-Access-Token", token, new CookieOptions(){HttpOnly = true, SameSite = SameSiteMode.Strict});
                userLogged.User = response;
                userLogged.Token = token;

                return HandleCreatedResponse(userLogged);
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
        [HttpGet(template:"authenticated", Name = nameof(UserAuthenticated))]
        [AllowAnonymous]
        public async Task<ActionResult<UserViewModel>> UserAuthenticated(){
            try
            {
                 if (!(Request.Cookies.TryGetValue("X-Access-Token", out var accessToken)))
                    return BadRequest();

                var access = Request.Cookies.Where(x => x.Key == "X-Access-Token").FirstOrDefault();
                accessToken = access.Value;
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(accessToken);
                var mobile = token.Claims.Where(x => x.Type == ClaimTypes.MobilePhone).FirstOrDefault();
                
                var userPhone = mobile.Value;
                var User = await _userService.GetUserByPhoneAsync(userPhone);

                if(User == null){
                    return HandleErrorResponse(HttpStatusCode.NotFound, "USER_NOT_FOUND");
                }
                
                return HandleCreatedResponse(User);
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
        [HttpPost(Name = nameof(CreateUser))]
        [AllowAnonymous]
        public async Task<ActionResult<UserViewModel>> CreateUser([FromBody] CreateUserViewModel viewModel)
        {

            try
            {
                var validator = new CreateUserValidator();

                var validate = validator.Validate(viewModel);

                if(!validate.IsValid){
                    return HandleErrorResponse(HttpStatusCode.BadRequest, validate.ToString());
                }

                var role = await _roleService.GetRoleAsync(viewModel.RoleId);

                if(role == null){
                    return HandleErrorResponse(HttpStatusCode.NotFound, "role doesn't exist");
                }

                var userd = await _userService.GetUserByPhoneAsync(viewModel.Phone);

                if(userd != null){
                    return HandleErrorResponse(HttpStatusCode.NotFound, "user phone already used");
                }

                var user = _mapper.Map<UserDomainModel>(viewModel);
                user.RoleId = (int)role.Id;
                // create new user
                var createdUser = await _userService.CreateUserAsync(user);

                // prepare response
                var response = _mapper.Map<UserViewModel>(createdUser);

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
        [HttpGet(Name = nameof(GetAllUsers))]
        public async Task<ActionResult<PaginationEntity<UserViewModel>>> GetAllUsers([FromQuery] UrlQueryParameters urlQueryParameters)
        {
            try
            {
                // get all paginated users
                var users = await _userService.GetAllUsersAsync(urlQueryParameters.Page, urlQueryParameters.Limit);
                // mapping the response
                var response = _mapper.Map<PaginationEntity<UserViewModel>>(users);
                // add previous route link if exist
                if (response.CurrentPage > 1)
                {
                    // generate previous route
                    var prevRoute = Url.RouteUrl(nameof(GetAllUsers), new { limit = urlQueryParameters.Limit, page = urlQueryParameters.Page - 1 });
                    response.Prev = prevRoute;
                    // add the route to dictionnary links
                    // response.AddResourceLink(LinkedResourceType.Prev, prevRoute);

                }
                //add next route link if exist
                if (response.CurrentPage < response.TotalPages)
                {
                    // generate next route
                    var nextRoute = Url.RouteUrl(nameof(GetAllUsers), new { limit = urlQueryParameters.Limit, page = urlQueryParameters.Page + 1 });
                    response.Next = nextRoute;
                    // add the route to dictionnary links
                    // response.AddResourceLink(LinkedResourceType.Next, nextRoute);
                }
                // handle success response
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
        [HttpGet(template:"{id}", Name = nameof(GetUserById))]
        [AllowAnonymous]
        public async Task<ActionResult<UserViewModel>> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserAsync(id);
                if(user == null){
                    return HandleErrorResponse(HttpStatusCode.NotFound, "user doesn't exist");
                }
                var response = _mapper.Map<UserViewModel>(user);

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
        [HttpDelete(template:"{id}", Name = nameof(DeleteUser))]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                // delete existing movie
                await _userService.DeleteUserAsync(id);

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
        [HttpPut(template:"{id}", Name = nameof(UpdateUser))]
        public async Task<ActionResult<UserViewModel>> UpdateUser(int id, [FromBody] UserViewModel viewModel)
        {
           try
            {
                Contract.Requires(viewModel != null);
            
                // id can be in URL, body, or both
                viewModel.Id = id;

                // map view model to domain model
                var user = _mapper.Map<UserDomainModel>(viewModel);

                // update existing user
                var updateduser = await _userService.UpdateUserAsync(user);

                // prepare response
                var response = _mapper.Map<UserViewModel>(updateduser);

                // 200 response
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
        [HttpPut(template:"{id}/update/password", Name = nameof(UpdateUserPassword))]
        public async Task<ActionResult<UserViewModel>> UpdateUserPassword(int id, [FromBody] UserPasswordViewModel viewModel)
        {
           try
            {
                Contract.Requires(viewModel != null);
            
                var validator = new UserPasswordValidator();

                var validate = validator.Validate(viewModel);

                if(!validate.IsValid){
                    return HandleErrorResponse(HttpStatusCode.BadRequest, validate.ToString());
                }

                // update existing User
                var updatedUser = await _userService.UpdatePasswordAsync(id, viewModel.odlpassword, viewModel.newpassword);

                // prepare response
                var response = _mapper.Map<UserViewModel>(updatedUser);

                if(response == null){
                    return HandleErrorResponse(HttpStatusCode.NotModified, "User doesn't exit or password doesn't match");
                }

                // 200 response
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
        [HttpPut(template: "{email}/reinitialize/password", Name = nameof(ReinitializeUserPassword))]
        [AllowAnonymous]
        public async Task<ActionResult<UserViewModel>> ReinitializeUserPassword(string email)
        {
            
             try{
                
                var user = await _userService.GetUserByEmailAsync(email);
                if(user == null){
                    return HandleErrorResponse(HttpStatusCode.NotFound, "user not found");
                }

                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var pw = new string(Enumerable.Repeat(chars, 6)
                                .Select(s => s[random.Next(s.Length)]).ToArray());

                // update existing client
                var updateduser = await _userService.UpdatePasswordAsync((int)user.Id, "azerty", pw);
                // prepare response
                var response = _mapper.Map<UserViewModel>(updateduser);

                if(response == null){
                    return HandleErrorResponse(HttpStatusCode.NotFound, "Client doesn't exit or password doesn't match");
                }

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
