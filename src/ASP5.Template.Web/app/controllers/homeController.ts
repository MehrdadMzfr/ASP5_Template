class HomeController
{
    constructor(private scope: any, private homeService: HomeService)
    {
        scope.vm = this;
        //homeService.getData().then((data: any) =>
        //{
        //    console.log(data.data);
        //    scope.message = data.data;
        //    return null;
        //});
        scope.userName = "sharpiro";
        scope.password = "password";
    }

    public login(userName: string, password: string): void
    {
        console.log(userName);
        console.log(password);
        this.homeService.login(userName, password).then((data) =>
        {
            this.scope.accessToken = data.data.access_token;
            console.log(data.data);
            return null;
        });
    }

    public getProtectedData(): void
    {
        //if (!this.scope.accessToken) return;
        this.homeService.getProtectedData(this.scope.accessToken).then((data) =>
        {
            console.log(data.data);
            return null;
        });
    }
}

app.controller("homeController", ["$scope", "homeService", HomeController]);