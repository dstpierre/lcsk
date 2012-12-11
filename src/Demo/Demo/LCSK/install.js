$(function () {
    $('#config').hide();

    $('#login-pass').focus();

    var myHub = $.connection.chatHub;

    $('#login-btn').click(function () {
        $('#login-alerts').text('');

        var agentPass = $('#login-pass').val();

        myHub.server.adminRequest(agentPass);
    });

    $('#save-button').click(function () {
        $('#save-alerts').text('');

        var adminPass = $('#main-pass').val();
        var agentPass = $('#agent-pass').val();

        if (adminPass == '') {
            $('#save-alerts').html('<div class="alert alert-warning">' +
                '<button type="button" class="close" data-dismiss="alert">×</button>' +
                '<strong>Oops!</strong> You have to specify an admin password.</div>');

            return;
        }

        if (agentPass == '') {
            $('#save-alerts').html('<div class="alert alert-warning">' +
                '<button type="button" class="close" data-dismiss="alert">×</button>' +
                '<strong>Oops!</strong> You have to specify an agent password.</div>');

            return;
        }

        myHub.server.setConfig($('#save-button').data('id'), adminPass, agentPass);
    });

    myHub.client.installState = function (state, data) {
        if (!state) {
            $('#config').show();
            $('#login').hide();
            
        }

        $('#save-button').data('id', data);
    }

    myHub.client.adminResult = function (state, data) {
        if (state) {
            $('#login').hide();
            $('#config').show();

            $('#save-button').data('id', data);
        } else {
            $('#login-alerts').html('<div class="alert alert-error">' +
                '<button type="button" class="close" data-dismiss="alert">×</button>' +
                '<strong>Oops!</strong> This is not the right password. If you do not remember your admin password, simply remove the LCSK.dat file on your App_Data folder.</div>');
        }
    };

    myHub.client.setConfigResult = function (state, msg) {
        var boxType = state ? 'alert-success' : 'alert-error';
        var boxPrompt = state ? 'Save successful' : 'Save failed';

        $('#save-alerts').html('<div class="alert ' + boxType + '">' +
                '<button type="button" class="close" data-dismiss="alert">×</button>' +
                '<strong>' + boxPrompt + '</strong> ' + msg);
    };

    $.connection.hub.start()
            .done(function () {
                myHub.server.getInstallState();
            })
            .fail(function () { alert('unable to connect'); });
});