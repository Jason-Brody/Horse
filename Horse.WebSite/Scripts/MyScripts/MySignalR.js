function agentHub(url) {
    var connection;
    var proxy;

    this.url = url;
    
    this.connect = function (Success,Fail) {

        connection = $.hubConnection(this.url, { useDefaultPath: false });
        proxy = connection.createHubProxy('agentHub');
        proxy.on('sayHello', function () {
            alert("hello");
        });
        connection.start().done(function () {
            Success();
        }).fail(function () {
            Fail();
        });
    }

    this.disConnect = function () {
        connection.stop();
    }

    this.runTask = function () {
        proxy.invoke('runTask');
    }

}