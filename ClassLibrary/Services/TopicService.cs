using System;
using System.Web.Services;
using System.Web.Script.Services;
using System.ServiceModel.Activation;
using Kvetch.Domain;

namespace Kvetch.Services
{
    [WebService(Namespace = "http://services.smallsharptools.com/kvetch/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class TopicService : ITopicService
    {

        [WebMethod]
        public TopicCollection GetTopics()
        {
            TopicManager manager = new TopicManager();
            return manager.GetTopics();
        }

        [WebMethod]
        public void InsertTopic(string title)
        {
            TopicManager manager = new TopicManager();
            Topic topic = new Topic() { Title = title };
            manager.InsertTopic(topic);
        }

    }
}
