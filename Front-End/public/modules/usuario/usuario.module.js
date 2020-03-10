(function ()
{
  'use strict';

  angular
    angular.module('app.usuario', [])
    .config(config);
  
  function config($stateProvider)
  {
    // State
    $stateProvider
      
    .state('usuario', {
        url: '/usuario',
        templateUrl: './modules/usuario/views/usuario-lista.html',
        controller: 'UsuarioListaController',
        controllerAs: 'vm',
        params: {
            title: "Usuarios"
        }
    })
    .state('usuario-novo', {
        url: '/usuario/novo',
        templateUrl: './modules/usuario/views/usuario-novo.html',
        controller: 'UsuarioController',
        controllerAs: 'vm',
        params: {
            title: "Cadastro de Usuario"
        },resolve : {
            usuarioId : function($stateParams){
                console.log('Modulo: ' + $stateParams.id)
                return $stateParams.id;
            }  
        }      
    })

    .state('usuario-editar', {
        url: '/usuario/usuario-editar/:id',
        templateUrl: '/modules/usuario/views/usuario-editar.html',
        controller: 'UsuarioController',
        controllerAs: 'vm',
        params: {
            title: "Editar Usuario"
        },
        resolve : {
            usuarioId : function($stateParams){
                console.log('Modulo: ' + $stateParams.id)
                return $stateParams.id;
            }    
        }
    })
  }
})()