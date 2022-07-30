using System;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web.Caching;
using Microsoft.Practices.EnterpriseLibrary.Data;
using KvetchLinq = Kvetch.Linq;
using System.Web;

namespace Kvetch.Domain
{
    [DataObject]
    public class TopicManager
    {

        private Database db;
        KvetchLinq.KvetchDataContext dc;

        public TopicManager()
        {
            db = DomainConfiguration.CreateDatabase();
            dc = DomainConfiguration.CreateDataContext();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public TopicCollection GetTopics()
        {
            string cacheKey = "Topics";
            Cache cache = HttpRuntime.Cache;
            if (cache[cacheKey] != null)
            {
                return (TopicCollection)cache[cacheKey];
            }
            TopicCollection topics = new TopicCollection();
            var query = from i in dc.Topics
                        select Convert(i);
            topics.AddRange(query.ToArray());

            cache.Add(cacheKey, topics, null, DateTime.Now.AddMinutes(5), 
                Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            return topics;
        }

        public Topic GetTopic(int topicId)
        {
            foreach (Topic topic in GetTopics())
            {
                if (topic.ID.Value.Equals(topicId))
                {
                    return topic;
                }
            }
            return null;
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void InsertTopic(Topic topic)
        {
            dc.Topics.InsertOnSubmit(Convert(topic));
            dc.SubmitChanges();
        }

        private KvetchLinq.Topic Convert(Topic topic)
        {
            return new KvetchLinq.Topic()
            {
                ID = topic.ID.Value,
                Title = topic.Title,
                Created = topic.Created,
                Modified = topic.Modified
            };
        }

        private Topic Convert(KvetchLinq.Topic topic)
        {
            return new Topic()
            {
                ID = new DomainKey<int>(topic.ID),
                Title = topic.Title,
                Created = topic.Created,
                Modified = topic.Modified
            };
        }

    }
}
