using System;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using KvetchLinq = Kvetch.Linq;

namespace Kvetch.Domain
{
    [DataObject]
    public class CommentManager
    {
        private Database db;
        KvetchLinq.KvetchDataContext dc;

        public CommentManager()
        {
            db = DatabaseFactory.CreateDatabase("Kvetch.Properties.Settings.KvetchConnectionString");
            string connString = ConfigurationManager.ConnectionStrings["Kvetch.Properties.Settings.KvetchConnectionString"].ConnectionString;
            dc = new Kvetch.Linq.KvetchDataContext(connString);
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public CommentCollection GetComments(Topic topic)
        {
            CommentCollection comments = new CommentCollection();
            var query = (from i in dc.Comments
                        where i.TopicID == topic.ID.Value
                        orderby i.Created descending
                        select Convert(i)).Take(100);
            comments.AddRange(query.ToArray());
            return comments;
        }

        public Comment GetComment(int commentId)
        {
            var query = from i in dc.Comments
                        where i.ID == commentId
                        select Convert(i);
            Comment comment = query.Single<Comment>();
            return comment;
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public Comment InsertComment(Comment comment)
        {
            KvetchLinq.Comment newComment = Convert(comment);
            dc.Comments.InsertOnSubmit(newComment);
            dc.SubmitChanges();
            return Convert(newComment);
        }

        private KvetchLinq.Comment Convert(Comment comment)
        {
            return new KvetchLinq.Comment()
            {
                ID = comment.ID.Value,
                Text = comment.Text,
                Author = comment.Author,
                TopicID = comment.TopicID,
                Created = comment.Created,
                Modified = comment.Modified
            };
        }

        private Comment Convert(KvetchLinq.Comment comment)
        {
            return new Comment()
            {
                ID = new DomainKey<int>(comment.ID),
                Text = comment.Text,
                Author = comment.Author,
                TopicID = comment.TopicID,
                Created = comment.Created,
                Modified = comment.Modified
            };
        }

    }
}
