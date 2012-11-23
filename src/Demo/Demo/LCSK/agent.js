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

            scrollDiv($('#chatmsgs' + chatId));

            showChat(chatId);

            chatMessages[chatId] = [];

            var badge = $(this).find('.badge');
            if (badge != null && badge != undefined) {
                badge.removeClass('badge-warning');
                badge.text('0');
            }
        }
    }, '.chat-session');

    $('#chat-sessions').on({
        click: function (event) {
            event.preventDefault();
            event.stopPropagation();

            var div = $(this).parent().parent();
            var chatId = div.data('id');

            myHub.closeChat(chatId);

            div.remove();
            $('chatmsgs' + chatId).remove();
            return false;
        }
    }, '.close-chat');

    $('#post-msg').keypress(function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            e.stopPropagation();
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

    myHub.newVisit = function (page, referrer, chatWith) {
        var d = new Date();
        $('#current-visits > tbody').prepend(
            '<tr><td><abbr class="timeago" title="' + d.toISOString() + '">' + d.toISOString() + '</abbr></td>' +
            '<td>' + page + '</td>' +
            '<td>' + referrer + '</td>' +
            '<td>' + (chatWith != null ? chatWith : 'not in chat') + '</td>' +
            '<td><a href="#" class="engage-visitor">engage in chat</a></td></tr>');

        $('#current-visits > tbody:first').find('abbr.timeago').timeago();
    };

    myHub.newChat = function (id) {
        var snd = new Audio('assets/sounds/newchat.mp3');
        snd.play();

        var d = new Date();

        var session = [];
        session.push('Chat started at ' + d.getHours() + ':' + d.getMinutes());
        
        chatMessages.push(id);
        chatMessages[id] = session;

        $('#chat-sessions').prepend(
            '<div id="chat' + id + '" class="chat-session" data-id="' + id + '">' +
            '<p><abbr class="timeago" title="' + d.toISOString() + '">' + d.toISOString() + '</abbr>' +
            '<a class="close-chat btn btn-mini pull-right" href="#"><i class="icon-remove"></i></a></p>' +
            '<p class="pull-right">New message(s) <span class="badge badge-warning">0</span></p>' +
            '</div>');

        $('#all-chatbox').append(
            '<div id="chatmsgs' + id + '" class="chat-msgs"></div>'
        );

        $('#chatmsgs' + id).hide();

        $('#chat' + id).find('abbr.timeago').timeago();
    };

    myHub.visitorSwitchPage = function (lastId, newId, newPage) {
        chatMessages.splice(lastId);

        var session = [];
        session.push('<strong>system</strong> The visitor is now on: ' + newPage);

        chatMessages.push(newId);
        chatMessages[newId] = session;
        

        // are we currently viewing that chat session?
        if (lastId == getCurrentChatId()) {
            $('#chatmsgs' + lastId).append('<p><strong>system</strong> The visitor is now on: ' + newPage + '</p>');

            scrollDiv($('#chatmsgs' + lastId));

            chatMessages[newId] = [];
        } else {
            var badge = $('#chat' + lastId).find('.badge');
            if (badge != null && badge != undefined) {
                if (!badge.hasClass('badge-warning')) {
                    badge.addClass('badge-warning');
                }
                badge.text(1);
            }
        }

        $('#chat' + lastId).attr('id', 'chat' + newId);
        $('#chatmsgs' + lastId).attr('id', 'chatmsgs' + newId);

        $('#chat' + newId).data('id', newId);
    };

    myHub.addMessage = function (id, from, value) {
        // are we currently viewing that chat session?
        if (id == getCurrentChatId()) {
            $('#chatmsgs' + id).append('<p><strong>' + from + '</strong> ' + value + '</p>');

            scrollDiv($('#chatmsgs' + id));
        } else {
            var snd = new Audio('assets/sounds/newmsg.mp3');
            snd.play();

            chatMessages[id].push('<strong>' + from + '</strong> ' + value);

            var badge = $('#chat' + id).find('.badge');
            if (badge != null && badge != undefined) {
                if (!badge.hasClass('badge-warning')) {
                    badge.addClass('badge-warning');
                }
                badge.text(chatMessages[id].length);
            }
        }
    };

    myHub.leave = function (id) {
        myHub.leaveChat(id);
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
    var msg = $('#post-msg').val();

    if (chatId != '' && msg != '') {
        myHub.opSend(chatId, msg);

        $('#post-msg').val('').focus();
    }
}

function scrollDiv(div) {
    div.scrollTop(div[0].scrollHeight);
}