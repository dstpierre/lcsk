var myHub = null;

var agent = {
    id: '',
    name: '',
    isOnline: false
};

var chatMessages = [];

$(function () {
    $('#agent-chat').hide();

    myHub = $.connection.chatHub;

    $('#login-btn').click(function () {
        var agentName = $('#login-name').val();
        var agentPass = $('#login-pass').val();
        
        $.connection.hub.start()
            .done(function () {
                myHub.agentConnect(agentName, agentPass);
            })
            .fail(function () { alert('unable to connect'); });
    });

    $('#show-real-time-visits').click(function () {
        $('.chat-session').removeClass('active');
        showChat('rt');
    });

    $('#change-status').click(function () {
        agent.isOnline = !agent.isOnline;
        myHub.changeStatus(agent.isOnline);

        showStatus();
    });

    $('#chat-sessions').on({
        click: function () {
            var chatId = $(this).data('id');

            $('.chat-session').removeClass('active');
            $(this).addClass('active');

            for (var i = 0; i < chatMessages[chatId].length; i++) {
                $('#chatmsgs' + chatId).append('<p>' + chatMessages[chatId][i] + '</p>');
            }

            $('#chatmsgs' + chatId).attr({ scrollTop: $('#chatmsgs' + chatId).attr("scrollHeight") });

            showChat(chatId);

            chatMessages[chatId] = [];

            $(this).find('.badge').text('0');
        }
    }, '.chat-session');

    $('#post-msg').keypress(function (e) {
        if (e.keyCode == 13) {
            postMsg();
        }
    });

    $('#post-btn').click(function () {
        postMsg();
    });

    myHub.loginResult = function (status, id, name) {
        if (status) {
            agent.id = id;
            agent.name = name;
            agent.isOnline = true;

            showStatus();
            $('#login').remove();
            $('#agent-chat').show();

            // Let's display the real time visitor
            showChat('rt');

        } else {
            $('#login-alerts').html('<div class="alert">' +
                '<button type="button" class="close" data-dismiss="alert">×</button>' + 
                '<strong>Oops!</strong> No agent matches those credentials.</div>');
        }
    };

    myHub.newChat = function (id) {
        var d = new Date();

        var session = [];
        session.push('Chat started at ' + d.getHours() + ':' + d.getMinutes());
        
        chatMessages.push(id);
        chatMessages[id] = session;

        $('#chat-sessions').prepend(
            '<div id="chat' + id + '" class="chat-session" data-id="' + id + '">' +
            '<p>Started at: ' + d.getHours() + ':' + d.getMinutes() +
            '<a class="close-chat btn btn-mini pull-right" href="#"><i class="icon-remove"></i></a></p>' +
            '<p class="pull-right">New message(s) <span class="badge badge-warning">0</span></p>' +
            '</div>');

        $('#all-chatbox').append(
            '<div id="chatmsgs' + id + '" class="chat-msgs"></div>'
        );

        $('#chatmsgs' + id).hide();
    };

    myHub.addMessage = function (id, from, value) {
        // are we currently viewing that chat session?
        if (id == getCurrentChatId()) {
            $('#chatmsgs' + id).append('<p><strong>' + from + '</strong> ' + value + '</p>');

            $('#chatmsgs' + id).attr({ scrollTop: $('#chatmsgs' + id).attr("scrollHeight") });
        } else {
            chatMessages[id].push('<strong>' + from + '</strong> ' + value);

            $('#chat' + id).find('.badge').text(chatMessages[id].length);
        }
    };
});

function showStatus() {
    $('#change-status').text(agent.name + ' ' + (agent.isOnline ? 'set offline' : 'set online'));
}

function getCurrentChatId() {
    if ($('.chat-session.active').length > 0) {
        return $('.chat-session.active').data('id');
    } else {
        return '';
    }
}

function showChat(windowToShow) {
    $('#real-time-visits').hide();
    $('#all-chatbox').hide();
    $('.chat-msgs').hide();
    $('#chat-controls').hide();

    if (windowToShow == 'rt') {
        $('#real-time-visits').show();
    } else {
        $('#all-chatbox').show();
        $('#chatmsgs' + windowToShow).show();
        $('#chat-controls').show().find('#post-msg').focus();
    }
}

function postMsg() {
    var chatId = getCurrentChatId();

    if (chatId != '') {
        myHub.opSend(chatId, $('#post-msg').val());

        $('#post-msg').val('').focus();
    }
}