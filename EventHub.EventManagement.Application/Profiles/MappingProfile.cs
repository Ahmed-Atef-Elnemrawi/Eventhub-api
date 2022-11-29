using AutoMapper;
using EventHub.EventManagement.Application.DTOs.AttendantDto;
using EventHub.EventManagement.Application.DTOs.CategoryDto;
using EventHub.EventManagement.Application.DTOs.EventDto;
using EventHub.EventManagement.Application.DTOs.FollowerDto;
using EventHub.EventManagement.Application.DTOs.MediumDto;
using EventHub.EventManagement.Application.DTOs.OrganizationDto;
using EventHub.EventManagement.Application.DTOs.ProducerDto;
using EventHub.EventManagement.Application.DTOs.SpeakerDto;
using EventHub.EventManagement.Application.DTOs.UserDto;
using EventHub.EventManagement.Domain.Entities;
using EventHub.EventManagement.Domain.Entities.EventEntities;
using EventHub.EventManagement.Domain.Entities.OrganizationEntities;
using EventHub.EventManagement.Domain.Entities.ProducerEntities;

namespace EventHub.EventManagement.Application.Profiles
{
   public class MappingProfile : Profile
   {
      public MappingProfile()
      {
         CreateMap<User, UserForRegistrationDto>().ReverseMap();

         CreateMap<Attendant, AttendantDto>();
         CreateMap<Attendant, AttendantForCreationDto>().ReverseMap();

         CreateMap<Category, CategoryDto>();
         CreateMap<Category, CategoryForCreationDto>().ReverseMap();
         CreateMap<Category, CategoryForUpdateDto>().ReverseMap();

         CreateMap<Event, EventDto>();
         CreateMap<OrganizationEvent, OrganizationEventDto>();
         CreateMap<ProducerEvent, ProducerEventDto>();

         CreateMap<Follower, FollowerDto>();
         CreateMap<Follower, FollowerForCreationDto>().ReverseMap();

         CreateMap<Medium, MediumDto>();

         CreateMap<Organization, OrganizationDto>();
         CreateMap<Organization, OrganizationForCreationDto>().ReverseMap();
         CreateMap<Organization, OrganizationForUpdateDto>().ReverseMap();

         CreateMap<OrganizationEvent, EventForCreationDto>().ReverseMap();
         CreateMap<OrganizationEvent, OrganizationEventForCreationDto>().ReverseMap();
         CreateMap<OrganizationEvent, EventForUpdateDto>().ReverseMap();
         CreateMap<OrganizationEvent, EventDto>();

         CreateMap<Producer, ProducerDto>();
         CreateMap<Producer, ProducerForCreationDto>().ReverseMap();
         CreateMap<Producer, ProducerForUpdateDto>().ReverseMap();

         CreateMap<ProducerEvent, EventForCreationDto>().ReverseMap();
         CreateMap<ProducerEvent, EventForUpdateDto>().ReverseMap();
         CreateMap<ProducerEvent, EventDto>();

         CreateMap<Speaker, SpeakerDto>().ReverseMap();
         CreateMap<Speaker, SpeakerForCreationDto>().ReverseMap();
         CreateMap<Speaker, SpeakerForUpdateDto>().ReverseMap();

      }
   }
}
