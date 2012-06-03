$(function () {
    if (document.location.href.indexOf('/chat/go/') > -1) {
        window.setTimeout(function () { document.location.href = document.location.href.replace('-status', ''); }, 12345);
    }

    LCSKChat.config();
    LCSKChat.init();


});

var LCSKChat = function () {
    var requestChat = false;
    var pingCount = 0;
    var pingIntervalId;
    var chatId = '';
    var lastMsgId = 0;
    var chatStatus = 'offline';
    var chatEditing = false;

    var options = [];

    function setDefaults() {
        options.position = 'fixed';
        options.placement = 'bottom-right';

        options.headerPaddings = '3px 10px 3px 10px';
        options.headerBackgroundColor = '#0376ee';
        options.headerTextColor = 'white';
        options.headerBorderColor = '#0354cb';
        options.headerGradientStart = '#058bf5';
        options.headerGradientEnd = '#015ee6';
        options.headerFontSize = 'small';

        options.boxBorderColor = '#0376ee';
        options.boxBackgroundColor = 'white';

        options.width = 350;

        options.offlineTitle = 'Chat is offline, contact us';
        options.onlineTitle = 'Chat with us, we\'re online';

        options.waitingForOperator = 'Thanks, give us 1min to accept the chat...';
        options.emailSent = 'Your email was sent, thanks we\'ll get back to you asap.';
        options.emailFailed = 'Doh! The email could not be sent at the moment.<br /><br />Sorry about that.';

    }

    function config(args) {
        setDefaults();

        if (args != null) {
            for (key in options) {
                if (args[key]) {
                    options[key] = args[key];
                }
            }
        }
    }

    function getPlacement() {
        if (options.placement == 'bottom-right') {
            return 'bottom:0px;right:0px;';
        }
        return '';
    }

    function init() {
        $('body').append(
            '<div id="chat-box-header" style="display: block;position:' + options.position + ';' + getPlacement() + 'width:' + options.width + 'px;padding:' + options.headerPaddings + ';color:' + options.headerTextColor + ';font-size:' + options.headerFontSize + ';cursor:pointer;background:' + options.headerBackgroundColor + ';filter: progid:DXImageTransform.Microsoft.gradient(startColorstr=\'' + options.headerGradientStart + '\', endColorstr=\'' + options.headerGradientEnd + '\');background: -webkit-gradient(linear, left top, left bottom, from(' + options.headerGradientStart + '), to(' + options.headerGradientEnd + '));background: -moz-linear-gradient(top,  ' + options.headerGradientStart + ',  ' + options.headerGradientEnd + ');border:1px solid ' + options.headerBorderColor + ';box-shadow:inset 0 0 7px #0354cb;-webkit-box-shadow:inset 0 0 7px #0354cb;border-radius: 5px;">' + options.offlineTitle + '</div>' +
            '<div id="chat-box" style="display:none;position:' + options.position + ';' + getPlacement() + 'width:' + options.width + 'px;height:300px;padding: 10px 10px 10px 10px;border: 1px solid ' + options.boxBorderColor + ';background-color:' + options.boxBackgroundColor + ';font-size:small;"></div>'
        );

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

        logVisit();

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
                                $('#chat-box-msg').html(options.waitingForOperator);
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
                $('#chat-box').html(options.emailSent);
                $.post('/chat/sendemail', { email: $('#chat-box-email').val(), comment: $('#chat-box-cmt').val() }, function (data) {
                    if (data == 'ok') {
                        chatEditing = false;
                    } else {
                        $('#chat-box').html(options.emailFailed);
                    }
                });
            }
        }, '#chat-box-send');
    }

    function logVisit() {
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
    }

    function chatRefreshState(state) {
        if (state) {
            $('#chat-box-header').text(options.onlineTitle);
            if (!requestChat) {
                $('#chat-box').html('<div id="chat-box-msg" style="height:265px;overflow:auto;">' +
                '<p>Have a question? Let\'s chat!</p><p>Add your question on the field below and press ENTER.</p></div>' +
                '<div id="chat-box-input" style="height:35px;"><textarea id="chat-box-textinput" style="width:100%;height: 32px;border:1px solid #0354cb;border-radius: 3px;" /></div>'
            );
            }
        } else {
            if (!chatEditing) {
                $('#chat-box-header').text(options.offlineTitle);
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
                if (!$('#chat-box').hasClass('chat-open')) {
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

    return {
        config: config,
        init: init
    }
} ();