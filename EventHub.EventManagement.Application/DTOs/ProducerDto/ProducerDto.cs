namespace EventHub.EventManagement.Application.DTOs.ProducerDto
{
   [Serializable]
   public record ProducerDto
   {
      public Guid ProducerId { get; init; }
      public string? FirstName { get; init; }
      public string? LastName { get; init; }
      public string? FullName { get; set; }
      public string? Genre { get; init; }
      public int Age { get; set; }
      public string? LiveIn { get; init; }
      public string? Email { get; init; }
      public string? PhoneNumber { get; init; }
      public string? JobTitle { get; init; }
      public string? Bio { get; init; }
   }
}
