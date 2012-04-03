var requestChat = false;
var pingCount = 0;
var pingIntervalId;
var chatId = '';
var lastMsgId = 0;
var chatStatus = 'offline';
var chatEditing = false;
var chatboxWidth = 350;

$(function () {

    $('body').append(
        '<div id="chat-box-header" style="display: block;position:fixed;bottom:0;right:0;width:' + chatboxWidth + 'px;padding: 3px 10px 3px 10px;color:white;font-size: small;cursor:pointer;background: #0376ee;filter: progid:DXImageTransform.Microsoft.gradient(startColorstr=\'#058bf5\', endColorstr=\'#015ee6\');background: -webkit-gradient(linear, left top, left bottom, from(#058bf5), to(#015ee6));background: -moz-linear-gradient(top,  #058bf5,  #015ee6);border:1px solid #0354cb;box-shadow:inset 0 0 7px #0354cb;-webkit-box-shadow:inset 0 0 7px #0354cb;border-radius: 5px;">Contact us</div>' +
        '<div id="chat-box" style="display:none;position:fixed;bottom:0px;right:0px;width:350px;height:300px;padding: 10px 10px 10px 10px;border: 1px solid #0376ee;background-color: white;font-size:small;"></div>'
    );

    $.post('/chat/logvisit', { page: document.location.href, referrer: document.referrer }, function (data) {
        lastChatStatus = data;
        if (data == 'failed') {
            $('#chat-box-header').hide();
            $('#chat-box').hide();
        } else {
            var state = false;
            if (data != 'online' && data != 'offline') {
                chatId = data;
                state = true;
            } else {
                state = data == 'online';
            }
            chatStatus = state ? 'online' : 'offline';
            chatRefreshState(state);
        }
    });

    pingIntervalId = window.setInterval(function () {
        if (pingCount >= 3) {
            pingCount = 0;
            $.post('/chat/ping', { page: document.location.href }, function (data) {
                if (data == 'failed') {
                    clearInterval(pingIntervalId);
                    $('#chat-box-header').hide();
                    $('#chat-box').hide();
                } else if (data != chatStatus) {
                    if (data != chatId) {
                        var state = false;
                        if (data != 'online' && data != 'offline') {
                            chatId = data;
                            state = true;
                        } else {
                            state = data == 'online';
                        }
                        chatStatus = state ? 'online' : 'offline';
                        chatRefreshState(state);
                    }
                }
            });
        } else {
            pingCount++;
        }

        getMsgs();
    },
    3210);


    $('#chat-box-header').click(function () {
        toggleChatBox();
    });

    $('#chat-box').on({
        keydown: function (e) {
            var msg = $(this).val();
            if (e.keyCode == 13) {
                if (chatId == null || chatId == '') {
                    $.post('/chat/requestchat', {}, function (data) {
                        if (data.status) {
                            requestChat = true;
                            chatId = data.chatId;
                            lastChatStatus = chatId;
                            $('#chat-box-msg').html('Thanks, give us 1min to accept the chat...');
                            $.post('/chat/addmsg', { id: chatId, from: 'me', msg: msg }, function (data) {
                                $('#chat-box-textinput').val('');
                                getMsgs();
                            });
                        }
                    });
                } else {
                    $.post('/chat/addmsg', { id: chatId, from: 'me', msg: msg }, function (data) {
                        $('#chat-box-textinput').val('');
                        getMsgs();
                    });
                }
            }
        }
    }, '#chat-box-textinput');

    $('#chat-box').on({
        keydown: function () {
            chatEditing = true;
        }
    }, '.chat-editing');

    $('#chat-box').on({
        click: function () {
            $.post('/chat/sendemail', { email: $('#chat-box-email').val(), comment: $('#chat-box-cmt').val() }, function (data) {
                if (data == 'ok') {
                    $('#chat-box').html('Your email was sent, thanks we\'ll get back to you asap.');
                    chatEditing = false;
                } else {
                    $('#chat-box').html('Doh! The email could not be sent at the moment.<br /><br />Sorry about that.');
                }
            });
        }
    }, '#chat-box-send');


});

function chatRefreshState(state) {
    if (state) {
        $('#chat-box-header').text('Chat with us, we\'re online');
        if (!requestChat) {
            $('#chat-box').html('<div id="chat-box-msg" style="height:265px;overflow:auto;">' +
                '<p>Have a question? Let\'s chat!</p><p>Add your question on the field below and press ENTER.</p></div>' +
                '<div id="chat-box-input" style="height:35px;"><textarea id="chat-box-textinput" style="width:100%;height: 32px;border:1px solid #0354cb;border-radius: 3px;" /></div>'
            );
        }
    } else {
        if (!chatEditing) {
            $('#chat-box-header').text('Chat is offline atm, contact us.');
            $('#chat-box-input').hide();
            $('#chat-box').html('<p>Your email</p><input type="text" id="chat-box-email" style="border:1px solid #0354cb;border-radius: 3px;width: 100%;" class="chat-editing" />' +
                '<p>Your message</p><textarea id="chat-box-cmt" cols="40" rows="7" class="chat-editing" style="border:1px solid #0354cb;border-radius: 3px;"></textarea>' +
                '<p><input type="button" id="chat-box-send" value="Contact us" />'
           );
        }
    }
}

function getMsgs() {
    if (chatId != null && chatId != '') {
        if (!requestChat) {
            if(!$('#chat-box').hasClass('chat-open')) {
                toggleChatBox();
            }
            requestChat = true;
        }

        $.post('/chat/pollmsgs', { id: chatId, lastId: lastMsgId }, function (data) {
            if (data.lastId > lastMsgId) {
                lastMsgId = data.lastId;

                for (i = 0; i < data.msgs.length; i++) {
                    $('#chat-box-msg').append('<p><strong>' + data.msgs[i].FromName + '</strong>: ' + data.msgs[i].Message + '</p>');
                    if (data.msgs[i].FromName == 'system') {//) {
                        chatId = '';
                        chatStatus = '';
                        requestChat = false;
                    }
                }

                $("#chat-box-msg").scrollTop($("#chat-box-msg")[0].scrollHeight);
            }
        });
    }
}
function toggleChatBox() {
    var elm = $('#chat-box-header');
    if ($('#chat-box').hasClass('chat-open')) {
        $('#chat-box').removeClass('chat-open');
        elm.css('bottom', '0px');
    } else {
        var y = 308 + elm.height();
        $('#chat-box').addClass('chat-open');
        elm.css('bottom', y);
    }
    $('#chat-box').slideToggle();
}