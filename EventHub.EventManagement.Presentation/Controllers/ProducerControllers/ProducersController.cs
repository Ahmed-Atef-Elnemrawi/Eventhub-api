using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs.ProducerDto;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Application.Validation;
using EventHub.EventManagement.Presentation.ActionFilter;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EventHub.EventManagement.Presentation.Controllers.ProducerControllers
{
   [ApiVersion("1.0")]
   [Route("api/v{v:apiversion}/producers")]
   [ApiController]
   public class ProducersController : ControllerBase
   {
      private readonly IServiceManager _service;

      public ProducersController(IServiceManager service)
      {
         _service = service;
      }

      [HttpGet(Name = "GetProducers")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      public async Task<IActionResult> GetAllProducers
         ([FromQuery] ProducerParams producerParams)
      {

         var producerLinkParams = new ProducerLinkParams(producerParams, HttpContext);

         var (linkResponse, metaData) = await _service.
            ProducerService.
            GetAllProducersAsync(producerLinkParams, trackChanges: false);

         Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(metaData));

         return linkResponse.HasLinks ? Ok(linkResponse.LinkedEntities)
            : Ok(linkResponse.ShapedEntities);
      }


      [HttpGet("{id:guid}", Name = "GetProducer")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      public async Task<IActionResult> GetProducer(Guid id, [FromQuery] ProducerParams producerParams)
      {
         var linkParams = new ProducerLinkParams(producerParams, HttpContext);

         var linkResponse = await _service
            .ProducerService
            .GetProducerAsync(id, linkParams, trackChanges: false);

         return linkResponse.HasLinks ?
           Ok(linkResponse.LinkedEntity) : Ok(linkResponse.ShapedEntity);
      }


      [HttpPost(Name = "CreateProducer")]
      public async Task<IActionResult> CreateProducer
         ([FromBody] ProducerForCreationDto producerForCreationDto)
      {

         var result = await new ProducerValidator().ValidateAsync(producerForCreationDto);
         if (!result.IsValid)
         {
            result.AddToModelState(ModelState);
            return BadRequest(ModelState);
         }

         var producerDto = await _service
            .ProducerService
            .CreateProducerAsync(producerForCreationDto);

         return CreatedAtRoute("GetProducer",
            new { Id = producerDto.ProducerId },
         producerDto);
      }


      [HttpDelete("{id:guid}", Name = "RemoveProducer")]
      [Authorize(Roles = "Administrator,Producer")]
      public async Task<IActionResult> RemoveProducer(Guid id)
      {
         await _service
            .ProducerService
            .RemoveProducerAsync(id, trackChanges: false);

         return NoContent();
      }


      [HttpPut("{id:guid}")]
      [Authorize(Roles = "Producer")]
      public async Task<IActionResult> UpdateProducer
         (Guid id, [FromBody] ProducerForUpdateDto producerForUpdateDto)
      {
         var result = await new ProducerValidator().ValidateAsync(producerForUpdateDto);
         if (!result.IsValid)
         {
            result.AddToModelState(ModelState);
            return BadRequest(ModelState);
         }

         await _service
            .ProducerService
            .UpdateProducerAsync(id, producerForUpdateDto, trackChanges: true);

         return NoContent();
      }
   }
}

