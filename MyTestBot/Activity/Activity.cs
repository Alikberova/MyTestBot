﻿using MyTestBot.Commands.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace MyTestBot.BoredApi
{
    public class ActivityModel //todo change name
    {
        public Guid? Id { get; set; }

        //need have this name during using API
        public string Activity { get; set; }

        public string ActivityRu { get; set; }

        public double? Accessibility { get; set; }

#nullable enable
        public string? Type { get; set; }

        public int? Participants { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PriceEnum Price { get; set; }

#nullable enable
        public string? Link { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
