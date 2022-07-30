/// <reference name="MicrosoftAjax.debug.js" />
/// <reference name="MicrosoftAjaxTimer.debug.js" />
/// <reference name="MicrosoftAjaxWebForms.debug.js" />

/// <reference name="..\Scripts\Kvetch.js" />

/// <reference path="..\Scripts\jquery.intellisense.js"/>

Kvetch.TopicSelector = function() {};

Kvetch.TopicSelector.prototype = 
{
    Name : 'Kvetch.TopicSelector',
    GetTopic : function(elementId)
    {
        var id = elementId.substr('Topic-'.length);
        var topics = this.KvetchApplication.Topics;
        for (var i=0;i<topics.length;i++)
        {
            var topic = topics[i];
            if (topic.ID.Value == id)
            {
                return topic;
            }
        }
        return null;
    },
    HighlightTopic : function(topic)
    {
        $(this.Selector + ' div.Topics span.Selected').removeClass('Selected');
        $('#Topic-' + topic.ID.Value).addClass('Selected');
    },
    UpdateDisplay : function()
    {
        /// <summary>Update display</summary>
        var __app = this;
        var topics = this.KvetchApplication.Topics;
        var selector = this.Selector + ' div.Topics';
        $(selector).empty().hide();
        for (var i=0;i<topics.length;i++)
        {
            var topic = topics[i];
            var id = 'Topic-' + topic.ID.Value;
            $(selector).append('<span id="' + id + 
                '" class="Topic">' + topic.Title + '</span>');
            var clickHandler = function()
            {
                // TODO determine why this curried value is changing
                var currentTopic = __app.GetTopic(this.id);
                __app.KvetchApplication.ChangeTopic(currentTopic);
                __app.HighlightTopic(currentTopic);
                //__app.ShowError('You clicked ' + __app.KvetchApplication.CurrentTopic.Title, '');
            };
            $('#' + id).click(clickHandler);
        }
        $(selector).fadeIn(250);
    },
    PostInitialize : function()
    {
        this.UpdateDisplay();
        this.HighlightTopic(this.KvetchApplication.CurrentTopic);
    }
};

Kvetch.Extend(Kvetch.TopicSelector, Kvetch.UserControl);
