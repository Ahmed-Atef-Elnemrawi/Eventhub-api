namespace EventHub.EventManagement.Application.DTOs.FollowerDto
{
   public record FollowerDto
   {
      public Guid Id { get; init; }
      public string? FirstName { get; init; }
      public string? LastName { get; init; }
      public string? FullName { get; set; }
      public int Age { get; set; }
      public string? Genre { get; init; }
      public string? LiveIn { get; init; }
   }
}
