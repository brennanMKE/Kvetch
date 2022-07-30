using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Kvetch.Domain
{
    [Serializable]
    [DataContract]
    public class Comment : DomainObject<Comment, int>
    {

        [DataMember]
        public int TopicID { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public string Author { get; set; }

    }

    [Serializable]
    [CollectionDataContract]
    public class CommentCollection : DomainCollection<Comment>
    {
    }
}
