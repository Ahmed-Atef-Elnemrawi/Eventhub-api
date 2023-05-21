namespace EventHub.EventManagement.Application.DTOs.ProducerDto
{
   [Serializable]
   public record ProducerDto
   {
      public Guid ProducerId { get; init; }
      public string? FirstName { get; init; }
      public string? LastName { get; init; }
      public string? Genre { get; init; }
      public int Age { get; init; }
      public string? LiveIn { get; init; }
      public string? PhoneNumber { get; init; }
      public string? Email { get; init; }
      public string? JobTitle { get; init; }
      public string? Bio { get; init; }
      public string? Facebook { get; init; }
      public string? Twitter { get; init; }
      public string? LinkedIn { get; init; }
      public int FollowersCount { get; init; }
   }
}
