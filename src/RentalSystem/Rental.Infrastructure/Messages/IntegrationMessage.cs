﻿using System.Text.Json.Serialization;

namespace Rental.Infrastructure.Messages
{
    public abstract class IntegrationMessage
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
