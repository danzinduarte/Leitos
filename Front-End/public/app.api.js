(function ()
{
    'use strict';

    angular
        .module('materialApp')
        .factory('api', apiService);
    
    function apiService($resource)
    {

      var api = {}      

      // Base Url
      api.baseUrl = 'https://192.168.0.17:5001/api/';

      
      /* Recursos da API */ 
      api.usuario   = $resource(api.baseUrl + 'usuario/:id', {id : '@id'},
        {update: {
          method: 'PUT'
        }
      })
      

      console.log("carregar api")
      api.autenticacao = $resource(api.baseUrl + 'login');
      return api;
    }

})();
