/// <reference name="MicrosoftAjax.debug.js" />
/// <reference name="MicrosoftAjaxTimer.debug.js" />
/// <reference name="MicrosoftAjaxWebForms.debug.js" />

/// <reference path="jquery.intellisense.js"/>

// Note: Currently the jQuery code is not working with VS 2008 Intellisense
// <reference path="jquery-1.1.3.1.js"/>

Type.registerNamespace('Kvetch');

// Inheritance function
Kvetch.Extend = function(destination, source)
{
    /// <summary>Extends one object with the properties from a source object</summary>
    /// <param name="destination">destination</param>
    /// <param name="source">source</param>
    /// <returns>object</returns>
    for (var property in source.prototype) 
    {
        if (destination.prototype[property] === undefined)
        {
            destination.prototype[property] = source.prototype[property];
        }
    }
    return destination;
};

Kvetch.Core = function() {};
Kvetch.Core.prototype =
{
    Name : 'Kvetch.Core',
    ShowError : function(message, stackTrace)
    {
        $('#Error').empty().append(message).css('background', '#d00').fadeIn(250);
        setTimeout(function() {$('#Error').fadeOut(250);}, 250);
        setTimeout(function() {$('#Error').css('background', '#900').fadeIn(250);}, 500);
        setTimeout(function() {$('#Error').fadeOut(500);}, 2000);
    },
    ErrorHandler : function(exception, context)
    {
        //exception._exceptionType;
        //exception._message;
        //exception._stackTrace;
        //exception._statusCode;
        //exception._timedOut;
        //alert('Error: ' + exception._message);
        context.ShowError(exception._message, exception._stackTrace);
    },
    InitializeServices : function()
    {
        this.CommentService = Kvetch.Services.CommentService;
        this.CommentService.set_defaultUserContext(this);
        this.CommentService.set_defaultFailedCallback(this.ErrorHandler);
        this.TopicService = Kvetch.Services.TopicService;
        this.TopicService.set_defaultUserContext(this);
        this.TopicService.set_defaultFailedCallback(this.ErrorHandler);
    }
};

Kvetch.Application = function() {};
Kvetch.Application.prototype =
{
    Name : 'Kvetch.Application',
    Instances : [],
    TopicChangeListeners : [],
    GetInstance : function(id)
    {
        return this.Instances[id];
    },
    RegisterUserControls : function()
    {
        /// <summary>Registers the user controls</summary>
        var __app = this;
        $('script').each(function()
        {
            var script = this;
            if (script.src !== undefined)
            {
                if (script.src.indexOf('UserControl.js?') != -1)
                {
                    var className = __app.GetQueryStringValue('className=', script.src);
                    var clientId = __app.GetQueryStringValue('clientId=', script.src);
                    
                    eval('__app.Instances["' + clientId + '"] = new ' + className + '();');
                    eval('__app.Instances["' + clientId + '"].KvetchApplication = __app;');
                    eval('__app.Instances["' + clientId + '"].Initialize("' + clientId + '");');
                }
            }
        });
    },
    ChangeTopic : function(topic)
    {
        this.CurrentTopic = topic;
        // TODO notify registered listened
        for (var index in this.TopicChangeListeners)
        {
            this.TopicChangeListeners[index].OnTopicChanged(topic);
        }
    },
    RegisterTopicChangeListener : function(listener)
    {
        var index = this.TopicChangeListeners.length;
        this.TopicChangeListeners[index] = listener;
    },
    GetQueryStringValue : function(key, url)
    {
        /// <summary>Extracts a query string value</summary>
        /// <param name="key">key</param>
        /// <param name="url">url</param>
        /// <returns>string</returns>
        var str = url.substr(url.indexOf(key) + key.length);
        if (str.indexOf('&') == -1)
        {
            return str;
        }
        else
        {
            return str.substr(0, str.indexOf('&'));
        }
    },
    Initialize : function()
    {
        /// <summary>Initializes the website</summary>
        var __app = this;
        this.InitializeServices();
        var topicsLoadedHandler = function(data, context)
        {
            __app.Topics = data;
            __app.CurrentTopic = data[0];
            __app.RegisterUserControls();
        };
        this.TopicService.GetTopics(topicsLoadedHandler);
    }
};

Kvetch.Extend(Kvetch.Application, Kvetch.Core);

Kvetch.UserControl = function() {};
Kvetch.UserControl.prototype =
{
    Name : 'Kvetch.UserControl',
    AttachEvents : function() {},
    UpdateDisplay : function() {},
    GetElement : function()
    {
        return $(this.Selector);
    },
    PostInitialize : function() 
    {
        this.UpdateDisplay();
        this.AttachEvents();
    },
    Initialize : function(id)
    {
        /// <summary>Initialize</summary>
        this.ID = id;
        this.Selector = '#' + this.ID;
        this.InitializeServices();
        this.PostInitialize();
    }
};

Kvetch.Extend(Kvetch.UserControl, Kvetch.Core);

var kvetchApplication = new Kvetch.Application();
$(document).ready(function() { kvetchApplication.Initialize(); });
