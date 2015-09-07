class HomeService
{
    constructor(private $http: ng.IHttpService)
    {

    }

    public getData(): ng.IPromise<any>
    {
        return this.$http.get("/api/values/GetManufacturers");
    }
}

app.service("homeService", ["$http", HomeService]);