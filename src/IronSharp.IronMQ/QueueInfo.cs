﻿using System.Collections.Generic;
using System.Threading;
using IronSharp.Core;
using Newtonsoft.Json;

namespace IronSharp.IronMQ
{
    public class QueueInfo : IInspectable
    {
        [JsonProperty("alerts", DefaultValueHandling = DefaultValueHandling.Ignore)] 
        private List<Alert> _alerts;

        [JsonProperty("subscribers", DefaultValueHandling = DefaultValueHandling.Ignore)] 
        private List<SubscriberItem> _subscribers;

        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("project_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ProjectId { get; set; }

        /// <summary>
        ///     Either multicast to push to all subscribers or unicast to push to one and only one subscriber.
        ///     Default is multicast.
        ///     To revert push queue to reqular pull queue set pull.
        /// </summary>
        [JsonProperty("push_type", DefaultValueHandling = DefaultValueHandling.Include)]
        public PushType PushType { get; set; }

        /// <summary>
        ///     the name of another queue where information about messages that can’t be delivered after retrying retries number of
        ///     times will be placed.
        ///     Pass in an empty string to disable error queues. Default is disabled. See also
        ///     http://dev.iron.io/mq/reference/push_queues/#error_queues
        /// </summary>
        [JsonProperty("error_queue", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ErrorQueue { get; set; }

        /// <summary>
        ///     How many times to retry on failure.
        ///     Default is 3.
        ///     Maximum is 100.
        /// </summary>
        [JsonProperty("retries", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? Retries { get; set; }

        /// <summary>
        ///     Delay between each retry in seconds.
        ///     Default is 60.
        /// </summary>
        [JsonProperty("retries_delay", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? RetriesDelay { get; set; }

        [JsonProperty("size", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? Size { get; set; }

        [JsonProperty("total_messages", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? TotalMessages { get; set; }

        /// <summary>
        ///     An array of subscriber hashes containing a required "url" field and an optional "headers" map for custom headers.
        ///     This set of subscribers will replace the existing subscribers. See Push Queues to learn more about types of
        ///     subscribers.
        ///     To add or remove subscribers, see the add subscribers endpoint or the remove subscribers endpoint.
        ///     The maximum is 64kb for JSON array of subscribers' hashes. See below for example JSON.
        /// </summary>
        [JsonIgnore]
        public List<SubscriberItem> Subscribers
        {
            get { return LazyInitializer.EnsureInitialized(ref _subscribers); }
            set { _subscribers = value; }
        }

        [JsonIgnore]
        public List<Alert> Alerts
        {
            get { return LazyInitializer.EnsureInitialized(ref _alerts); }
            set { _alerts = value; }
        }
    }
}