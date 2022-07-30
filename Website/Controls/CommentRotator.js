/// <reference name="MicrosoftAjax.debug.js" />
/// <reference name="MicrosoftAjaxTimer.debug.js" />
/// <reference name="MicrosoftAjaxWebForms.debug.js" />

/// <reference name="..\Scripts\Kvetch.js" />

/// <reference path="..\Scripts\jquery.intellisense.js"/>

Kvetch.CommentRotator = function() {};

Kvetch.CommentRotator.prototype = 
{
    Name : 'Kvetch.CommentRotator',
    IntervalDelay : 5000,
    Interval : null,
    CommentIndex : 0,
    CommentCounter : 0,
    CommentCounterMax : 50,
    ShowNextComment : function()
    {
        var __app = this;
        if (this.Comments !== undefined)
        {
            if (this.CommentCounter >= this.CommentCounterMax)
            {
                this.StopInterval();
                this.GetComments();
                return;
            }
            var nextIndex = Math.floor(Math.random() * (this.Comments.length));
            if (nextIndex == this.CommentIndex)
            {
                // try again 1 time
                nextIndex = Math.floor(Math.random() * (this.Comments.length));
            }
            this.CommentIndex = nextIndex;
            var comment = this.Comments[nextIndex];
            $(this.Selector + ' div.Comment').fadeOut(750);
            setTimeout(function()
            {
                var content = 
                    '<div class="Text">' + comment.Text + '</div>' +
                    '<div class="Author"> - ' + comment.Author + '</div>';
                $(__app.Selector + ' div.Comment').empty().append(content).fadeIn();
            }, 750);
            this.CommentCounter += 1;
        }
    },
    StartInterval : function()
    {
        var __app = this;
        this.StopInterval();
        this.CommentCounter = 0;
        if (this.Comments !== undefined && this.Comments.length > 0)
        {
            var showNextComment = function()
            {
                __app.ShowNextComment();
            };
            showNextComment();
            this.Interval = setInterval(showNextComment, this.IntervalDelay);
        }
        else
        {
            $(this.Selector).empty().append('No comments to display');
        }
    },
    StopInterval : function()
    {
        if (this.Interval !== undefined)
        {
            clearInterval(this.Interval);
        }
    },
    OnTopicChanged : function(topic)
    {
        this.StopInterval();
        this.GetComments();
    },
    GetComments : function()
    {
        var __app = this;
        var handleResponse = function(data, context)
        {
            __app.Comments = data;
            __app.StartInterval();
        };
        this.CurrentTopic = this.KvetchApplication.CurrentTopic;
        this.CommentService.GetComments(this.CurrentTopic.ID.Value, handleResponse);
    },
    UpdateDisplay : function()
    {
        /// <summary></summary>
        $('#' + this.controlId).empty().append('<div>CommentRotator</div>');
    },
    PostInitialize : function() 
    {
        this.KvetchApplication.RegisterTopicChangeListener(this);
        this.GetComments();
        //this.UpdateDisplay();
        //this.AttachEvents();
    }
};

Kvetch.Extend(Kvetch.CommentRotator, Kvetch.UserControl);
