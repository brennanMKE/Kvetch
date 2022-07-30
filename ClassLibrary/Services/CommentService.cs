using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ServiceModel.Activation;
using Kvetch.Domain;

namespace Kvetch.Services
{
    [WebService(Namespace = "http://services.smallsharptools.com/kvetch/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class CommentService : ICommentService
    {

        [WebMethod]
        public CommentCollection GetComments(int topicId)
        {
            CommentManager manager = new CommentManager();
            return manager.GetComments(GetTopic(topicId));
        }

        [WebMethod]
        public Comment PostComment(string text, string author, int topicId)
        {
            CommentManager manager = new CommentManager();
            Comment comment = new Comment()
            {
                Text = text,
                Author = author,
                TopicID = topicId
            };
            return manager.InsertComment(comment);
        }

        private Topic GetTopic(int topicId)
        {
            TopicManager manager = new TopicManager();
            return manager.GetTopic(topicId);
        }

    }
}