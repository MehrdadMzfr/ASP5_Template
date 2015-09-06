class HomeService
{
    constructor(private $http: ng.IHttpService)
    {

    }

    public getData(): ng.IPromise<any>
    {
        return this.$http.get("/api/values/getdata");
    }
}

app.service("homeService", ["$http", HomeService]);