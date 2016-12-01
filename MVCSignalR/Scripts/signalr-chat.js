function addErr($doc) {
    $doc.addClass("err-tip");
}

function removeErr($doc) {
    $doc.removeClass("err-tip");
}

function getInitLi(initMsg) {
    return '<li class="msg-item"><div class="init-msg">...' + initMsg + '...</div></li>';
}

function getChatItem(img, nick, time, content) {
    var imgUrl = "/Content/Imgs/" + img + ".jpg";
    return '<li class="msg-item"><div class="img-box"><img src=' + imgUrl + ' /></div><div class="nick-msg"><div><span class="nick">' + nick + '</span><span class="time">' + time + '</span></div><div class="msg">' + content + '</div></div></li>';
}

$(function () {
    var connection = $.connection('/myConnection');
    var $nick = $("#chat-nick");
    var $msg;
    var nick = "";
    var headImgName = "";
    var sendMsg = "";
    connection.received(function (data) {
        var $li = "";
        if (data == "连接已经建立") {
            $li = getInitLi(data);
        } else {
            $nick.attr("disabled", "disabled");
            $li = getChatItem(data.headFileName, data.nick, data.time, data.msg);
        }
        $('#messages-connection').append($li);
        $('#msg').val("");
    });

    connection.disconnected(function (data) {
        $('#messages-connection').append('<li style="text-align:left;">' + data == undefined ? "掉线啦..." : data + '</li>');
    });

    connection.start();

    $("#broadcast").click(function () {
        nick = $nick.val();
        $msg = $('#msg');
        sendMsg = $msg.val();
        if (nick == "") {
            addErr($nick); return;
        }
        removeErr($nick);
        if (sendMsg == "") {
            addErr($msg); return;
        }
        removeErr($msg);

        var info = headImgName == "" ? nick + "|" + sendMsg : nick + "|" + sendMsg + "|" + headImgName;

        connection.send(info);
    });
    $("#btnStop").click(function () {
        connection.stop();
    });

    // 建立对应server端Hub class的对象，请注意myHub的第一个字母要改成小写 
    var chat = $.connection.myHub;

    // 定义client端的javascript function，供server端hub，通过dynamic的方式，调用所有Clients的javascript function 
    chat.client.sendMessage = function (nick, img, time, message) {
        //当server端调用sendMessage时，将server push的message数据，呈现在wholeMessage中 
        headImgName = img;
        $('#messages-hub').append(getChatItem(img, nick, time, message));
    };

    $("#sentBtn").click(function () {
        //调用叫server端的Hub对象，将#message数据传给server 
        nick = $nick.val();
        $msg = $('#message');
        sendMsg = $msg.val();
        if (nick == "") {
            addErr($nick); return;
        }
        removeErr($nick);
        if (sendMsg == "") {
            addErr($msg); return;
        }
        removeErr($msg);


        chat.server.send(nick, sendMsg, headImgName);
        $('#message').val("");
    });

    //把connection打开 
    $.connection.hub.start().done(function () {
        $('#messages-hub').append(getInitLi("连接已经建立"));
    });

});