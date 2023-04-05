namespace EventHub.EventManagement.Application.DTOs.ProducerDto
{
   [Serializable]
   public record ProducerDto
   {
      public Guid ProducerId { get; init; }
      public string? FirstName { get; set; }
      public string? LastName { get; set; }
      public string? Genre { get; set; }
      public int Age { get; set; }
      public string? LiveIn { get; set; }
      public string? PhoneNumber { get; set; }
      public string? Email { get; set; }
      public string? JobTitle { get; set; }
      public string? Bio { get; set; }
      public string? Facebook { get; set; }
      public string? Twitter { get; set; }
      public string? LinkedIn { get; set; }
   }
}
