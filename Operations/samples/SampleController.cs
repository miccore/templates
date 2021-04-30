using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.Microservice.Data;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sample.Microservice.Services.Sample;
using Sample.Microservice.Services.Sample.DomainModels;
using Sample.Microservice.Operations.Sample.ViewModels;
using Sample.Microservice.Operations.Sample.Validator;
using AutoMapper;
using System.Diagnostics.Contracts;

namespace Sample.Microservice.Operations
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
        [HttpPost]
        public async Task<ActionResult<SampleViewModel>> Create([FromBody] CreateSampleViewModel viewModel)
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
        [HttpGet]
        public async Task<ActionResult<List<SampleViewModel>>> GetAll()
        {
           try
            {
                var samples = await _sampleService.GetAllSamplesAsync();

                var response = _mapper.Map<List<SampleViewModel>>(samples);

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
        public async Task<ActionResult<SampleViewModel>> GetById(int id)
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
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
        [HttpPut("{id}")]
        public async Task<ActionResult<SampleViewModel>> Update(int id, [FromBody] SampleViewModel viewModel)
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