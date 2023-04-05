﻿namespace EventHub.EventManagement.Application.DTOs.EventDto
{
   public record EventDto
   {
      public Guid EventId { get; init; }
      public Guid CategoryId { get; init; }
      public string? Name { get; init; }
      public DateTime Date { get; init; }
      public string? Description { get; init; }
      public string? Discriminator { get; init; }
      public byte[]? Image { get; init; }
      public string? Url { get; init; }
      public string? City { get; init; }
      public string? Country { get; init; }
      public string? EventState { get; init; }

   }
}