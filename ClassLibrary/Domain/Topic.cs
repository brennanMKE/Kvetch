using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Kvetch.Domain
{
    [Serializable]
    [DataContract]
    public class Topic : DomainObject<Topic, int>
    {

        [DataMember]
        public string Title { get; set; }

    }

    [Serializable]
    [CollectionDataContract]
    public class TopicCollection : DomainCollection<Topic>
    {
    }
}
