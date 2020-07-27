using IUB.Commands.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace IUB.Activity
{
    public class Activity
    {
        public Guid? Id { get; set; }

        public string TextEn { get; set; }

        public string TextRu { get; set; }

        public double? Accessibility { get; set; }

#nullable enable
        public string? Type { get; set; } //todo change to enum

        public int? Participants { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PriceEnum Price { get; set; }

#nullable enable
        public string? Link { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
