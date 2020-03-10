(function ()
{
  'use strict';

  angular
    angular.module('app.login', [])
    .config(config);
  
  function config($stateProvider)
  {
    // State
    $stateProvider
      
    .state('login', {
        url: '/login',
        templateUrl: './modules/login/views/sign-up.html',
        controller: 'LoginController',
        controllerAs: 'vm',
        params: {
            title: "Faça seu Login"
        },
        views : {
          'login' : {
            templateUrl: './modules/login/views/index.html',
            controller: 'LoginController',
            controllerAs: 'vm',
          }
        }
    })
    
  }
})()