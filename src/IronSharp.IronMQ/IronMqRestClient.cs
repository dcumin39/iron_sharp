﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IronSharp.Core;
using IronSharp.Core.Attributes;

namespace IronSharp.IronMQ
{
    public class IronMqRestClient
    {
        private readonly IronClientConfig _config;

        internal IronMqRestClient(IronClientConfig config)
        {
            _config = LazyInitializer.EnsureInitialized(ref config);

            if (string.IsNullOrEmpty(Config.Host))
            {
                Config.Host = IronMqCloudHosts.DEFAULT;
            }

            Config.ApiVersion = config.ApiVersion.GetValueOrDefault(1);
        }

        public IronClientConfig Config
        {
            get { return _config; }
        }

        public string EndPoint
        {
            get { return "/projects/{Project ID}/queues"; }
        }

        public QueueClient<T> Queue<T>()
        {
            return Queue<T>(QueueNameAttribute.GetName<T>());
        }

        public QueueClient<T> Queue<T>(string name)
        {
            return new QueueClient<T>(this, name);
        }

        public QueueClient Queue(string name)
        {
            return new QueueClient(this, name);
        }

        /// <summary>
        /// Get a list of all queues in a project.
        /// By default, 30 queues are listed at a time.
        /// To see more, use the page parameter or the per_page parameter.
        /// Up to 100 queues may be listed on a single page.
        /// </summary>
        /// <param name="filter"> </param>
        /// <returns> </returns>
        public async Task<IEnumerable<QueueInfo>> Queues(PagingFilter filter = null)
        {
            return await RestClient.Get<IEnumerable<QueueInfo>>(_config, EndPoint, filter).
                ContinueWith(x => x.Result.ReadResultAsync()).
                Unwrap();
        }
    }
}