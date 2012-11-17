var agent = {
    id: '',
    name: '',
    isOnline: false
};

var chatMessages = [];

$(function () {
    var myHub = $.connection.chatHub;

    $('#login-btn').click(function () {
        var agentName = $('#login-name').val();
        var agentPass = $('#login-pass').val();
        
        $.connection.hub.start()
            .done(function () {
                myHub.agentConnect(agentName, agentPass);
            })
            .fail(function () { alert('unable to connect'); });
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

            $('.chat-msgs').hide();

            $('#chatmsgs' + chatId).show();

            for (var i = 0; i < chatMessages[chatId].length; i++) {
                $('#chatmsgs' + chatId).append('<p>' + chatMessages[chatId][i] + '</p>');
            }

            chatMessages[chatId] = [];
        }
    }, '.chat-session');

    myHub.loginResult = function (status, id, name) {
        if (status) {
            agent.id = id;
            agent.name = name;
            agent.isOnline = true;

            showStatus();
            $('#chat-content').html('<h3>Yay! You are logged in.</h3>');
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
            '<p>Started at: ' + d.getHours() + ':' + d.getMinutes() + '</p>' +
            '<p class="pull-right">New message <span class="badge badge-warning">0</span></p>' +
            '</div>');

        $('#chat-content').append(
            '<div id="chatmsgs' + id + '" class="chat-msgs"></div>'
        );

        $('#chatmsgs' + id).hide();
    };

    myHub.addMessage = function (id, from, value) {
        chatMessages[id].push('<strong>' + from + '</strong> ' + value);

        $('#chat' + id).find('.badge').text(chatMessages[id].length);
    };
});

function showStatus() {
    $('#change-status').text(agent.name + ' ' + (agent.isOnline ? 'set offline' : 'set online'));
}