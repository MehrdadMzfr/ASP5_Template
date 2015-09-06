///<reference path="../Lib/definitelyTyped/angular/angular.d.ts"/>

var app = angular.module("app", ["ui.router"]);

app.config(["$stateProvider", "$urlRouterProvider", ($stateProvider: any, $urlRouterProvider: any) =>
{
    $urlRouterProvider.otherwise("/home");

    $stateProvider
        .state("home", {
            url: "/home",
            templateUrl: "app/templates/homeTemplate.html",
            controller: "homeController"
        });
}]);