using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Kvetch.Domain;

namespace Kvetch.Services
{
    [ServiceContract(Namespace = "http://services.smallsharptools.com/kvetch/")]
    public interface ITopicService
    {
        [OperationContract]
        TopicCollection GetTopics();

        [OperationContract]
        void InsertTopic(string title);

    }
}
