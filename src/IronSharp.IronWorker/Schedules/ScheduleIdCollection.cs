﻿using System.Collections.Generic;
using System.Threading;
using IronSharp.Core;
using Newtonsoft.Json;

namespace IronSharp.IronWorker
{
    public class ScheduleIdCollection : IMsg, IInspectable
    {
        private List<TaskId> _schedules;

        [JsonProperty("schedules")]
        public List<TaskId> Schedules
        {
            get { return LazyInitializer.EnsureInitialized(ref _schedules); }
            set { _schedules = value; }
        }

        [JsonIgnore]
        public bool Success
        {
            get { return this.HasExpectedMessage("Scheduled"); }
        }

        [JsonProperty("msg", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; set; }

        public static implicit operator bool(ScheduleIdCollection collection)
        {
            return collection.Success;
        }
    }
}