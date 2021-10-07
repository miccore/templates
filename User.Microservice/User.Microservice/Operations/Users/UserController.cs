using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Microservice.Data;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User.Microservice.Services.User;
using User.Microservice.Services.User.DomainModels;
using User.Microservice.Operations.User.ViewModels;
using User.Microservice.Operations.User.Validator;
using AutoMapper;
using System.Diagnostics.Contracts;
using User.Microservice.Services.Role;
using Microsoft.AspNetCore.Authorization;
using JWTAuthentication.Models;
using User.Microservice.Operations.ApiResponses;
using System.Net.Http;
using User.Microservice.Entities;
using Newtonsoft.Json;

namespace User.Microservice.Operations
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
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserViewModel>> Login([FromBody] LoginUserViewModel loginUser){
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
        [HttpPost("refresh/token")]
        [AllowAnonymous]
        public async Task<ActionResult<UserViewModel>> RefreshToken(){
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
                var updated = await _userService.UpdateUserAsync(User);

                var response = _mapper.Map<UserViewModel>(updated);
                LoginUserResponseViewModel userLogged = new LoginUserResponseViewModel();

                Response.Cookies.Append("X-Refresh-Token", refresh, new CookieOptions(){HttpOnly = true, SameSite = SameSiteMode.Strict});
                Response.Cookies.Append("X-Access-Token", token, new CookieOptions(){HttpOnly = true, SameSite = SameSiteMode.Strict});
                userLogged.User = response;
                userLogged.Token = token;

                return HandleCreatedResponse(updated);
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
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserViewModel>> Create([FromBody] CreateUserViewModel viewModel)
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
        [HttpGet]
        public async Task<ActionResult<List<UserViewModel>>> GetAll()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();

                var response = _mapper.Map<List<UserViewModel>>(users);

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
        [AllowAnonymous]
        public async Task<ActionResult<UserViewModel>> GetById(int id)
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
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
        [HttpPut("{id}")]
        public async Task<ActionResult<UserViewModel>> Update(int id, [FromBody] UserViewModel viewModel)
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
        [HttpPut("{id}/update/password")]
        public async Task<ActionResult<UserViewModel>> UpdatePassword(int id, [FromBody] UserPasswordViewModel viewModel)
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
        [HttpPut("{email}/reinitialize/password")]
        [AllowAnonymous]
        public async Task<ActionResult<UserViewModel>> ReinitializePassword(string email)
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

                EmailService emailSend = new EmailService();
                emailSend.SendText(null, email, "Reset Password", $"New Password : {pw}");

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
