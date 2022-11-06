﻿namespace EventHub.EventManagement.Application.Models
{
   public class ShapedEntity
   {
      public Guid Id { get; set; }
      public Entity Entity { get; set; }

      public ShapedEntity()
      {
         Entity = new Entity();
      }
   }
}
