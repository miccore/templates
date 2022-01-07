using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Miccore.Net.webapi_template.Sample.Api.Data;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miccore.Net.webapi_template.Sample.Api.Services.Sample;
using Miccore.Net.webapi_template.Sample.Api.Services.Sample.DomainModels;
using Miccore.Net.webapi_template.Sample.Api.Operations.Sample.ViewModels;
using Miccore.Net.webapi_template.Sample.Api.Operations.Sample.Validator;
using AutoMapper;
using System.Diagnostics.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Miccore.Net.webapi_template.User.Api.Entities;

namespace Miccore.Net.webapi_template.Sample.Api.Operations
{
    [Route("[controller]")]
    [ApiController]
    public class SampleController : BaseController
    {
        private readonly ISampleService _sampleService;
        private readonly IMapper _mapper;
        public SampleController(
            IMapper mapper,
            ISampleService sampleService
        )
        {
            _sampleService = sampleService;
            _mapper = mapper;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]    
        [ProducesResponseType(StatusCodes.Status400BadRequest)]   
        [HttpPost(Name = nameof(CreateSample))]
        public async Task<ActionResult<SampleViewModel>> CreateSample([FromBody] CreateSampleViewModel viewModel)
        {

           try
            {
                var validator = new CreateSampleValidator();

                var validate = validator.Validate(viewModel);

                if(!validate.IsValid){
                    return HandleErrorResponse(HttpStatusCode.BadRequest, validate.ToString());
                }

                var sample = _mapper.Map<SampleDomainModel>(viewModel);

                // create new sample
                var createdSample = await _sampleService.CreateSampleAsync(sample);

                // prepare response
                var response = _mapper.Map<SampleViewModel>(createdSample);

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
        [HttpGet(nameof = nameof(GetAllSamples))]
        public async Task<ActionResult<PaginationEntity<SampleViewModel>>> GetAllSamples([FromQuery] UrlQueryParameters urlQueryParameters)
        {
           try
            {
                var samples = await _sampleService.GetAllSamplesAsync(urlQueryParameters.Page, urlQueryParameters.Limit);

                var response = _mapper.Map<PaginationEntity<SampleViewModel>>(samples);
                 // add previous route link if exist
                if (response.CurrentPage > 1)
                {
                    // generate previous route
                    var prevRoute = Url.RouteUrl(nameof(GetAllSamples), new { limit = urlQueryParameters.Limit, page = urlQueryParameters.Page - 1 });
                    // add the route to dictionnary links
                    response.Prev= prevRoute;

                }
                //add next route link if exist
                if (response.CurrentPage < response.TotalPages)
                {
                    // generate next route
                    var nextRoute = Url.RouteUrl(nameof(GetAllSamples), new { limit = urlQueryParameters.Limit, page = urlQueryParameters.Page + 1 });
                    // add the route to dictionnary links
                    response.Next = nextRoute;
                }
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
        [HttpGet(template: "{id}", Name = nameof(GetSampleById))]
        public async Task<ActionResult<SampleViewModel>> GetSampleById(int id)
        {
           try
            {
                var sample = await _sampleService.GetSampleAsync(id);

                var response = _mapper.Map<SampleViewModel>(sample);

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
        [HttpDelete(template: "{id}", Name = nameof(DeleteSample))]
        public async Task<IActionResult> DeleteSample(int id)
        {
            // delete existing movie
            try
            {
                await _sampleService.DeleteSampleAsync(id);

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
        [HttpPut(template: "{id}", Name = nameof(UpdateSample))]
        public async Task<ActionResult<SampleViewModel>> UpdateSample(int id, [FromBody] SampleViewModel viewModel)
        {
            try
            {
                Contract.Requires(viewModel != null);
            
                // id can be in URL, body, or both
                viewModel.Id = id;

                // map view model to domain model
                var sample = _mapper.Map<SampleDomainModel>(viewModel);

                // update existing sample
                var updatedsample = await _sampleService.UpdateSampleAsync(sample);

                // prepare response
                var response = _mapper.Map<SampleViewModel>(updatedsample);

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