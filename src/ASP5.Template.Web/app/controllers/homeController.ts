class HomeController
{
    constructor(private scope: any, homeService: HomeService)
    {
        homeService.getData().then((data: any) => {
            console.log(data.data);
            scope.message = data.data;
            return null;
        });
}
}

app.controller("homeController", ["$scope", "homeService", HomeController]);