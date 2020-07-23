using IUB.Commands.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace IUB.BoredApi
{
    public class ActivityModel //todo change name
    {
        public Guid? Id { get; set; }

        //todo: need have this name during using API
        public string Activity { get; set; }

        public string ActivityRu { get; set; }

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
