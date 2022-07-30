/// <reference name="MicrosoftAjax.debug.js" />
/// <reference name="MicrosoftAjaxTimer.debug.js" />
/// <reference name="MicrosoftAjaxWebForms.debug.js" />

/// <reference name="..\Scripts\Kvetch.js" />

/// <reference path="..\Scripts\jquery.intellisense.js"/>

Kvetch.CommentEntry = function() {};

Kvetch.CommentEntry.prototype = 
{
    Name : 'Kvetch.CommentEntry',
    AttachEvents : function()
    {
        var __app = this;
        var successHandler = function()
        {
            __app.DisplayStatus('Kvetch sent!');
            __app.ClearEntryForm();
        };
        var submitHandler = function()
        {
            this.blur();
            var text = __app.GetComment();
            if (text === '') {
                __app.ShowError("Comment cannot be empty");
                return; 
            }
            var author = __app.GetAuthorName();
            var topicId = __app.KvetchApplication.CurrentTopic.ID.Value;
            __app.CommentService.PostComment(
                text, author, topicId, successHandler);
        };
        $(this.Selector + ' td.Submit input').click(submitHandler);
    },
    ClearEntryForm : function()
    {
        $(this.Selector + ' td.Comment textarea').val('');
    },
    GetComment : function()
    {
        return $(this.Selector + ' td.Comment textarea').val();
    },
    GetAuthorName : function()
    {
        return $(this.Selector + ' td.Name input').val();
    },
    DisplayStatus : function(text)
    {
        var __app = this;
        $(this.Selector + ' table.EntryForm').hide();
        $(this.Selector + ' div.Status').empty().append(text).fadeIn(250);
        setTimeout(function()
        {
            $(__app.Selector + ' div.Status').fadeOut(250);
        }, 1250);
        setTimeout(function()
        {
            $(__app.Selector + ' table.EntryForm').fadeIn(500);
        }, 1500);
    },
    UpdateDisplay : function()
    {
        /// <summary></summary>
        //$('#' + this.controlId).empty().append('<div>CommentEntry</div>');
    }
};

Kvetch.Extend(Kvetch.CommentEntry, Kvetch.UserControl);
