using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Kvetch.Domain;

namespace Kvetch.Services
{
    [ServiceContract(Namespace = "http://services.smallsharptools.com/kvetch/")]
    public interface ICommentService
    {

        [OperationContract]
        CommentCollection GetComments(int topicId);

        [OperationContract]
        Comment PostComment(string text, string author, int topicId);

    }

}