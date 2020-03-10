var materialApp = angular
.module('materialApp', [
    'materialApp.routes',
    'ui.router',
    'ngMaterial',
    'ngResource',
    'ngMessages',
    'materialApp',
    'ngStorage',
    'app.usuario',
    'app.login'

    
]).config(function($mdThemingProvider,$mdDateLocaleProvider,$mdAriaProvider,$httpProvider) {
  
  $mdThemingProvider.theme('default')
  .primaryPalette('light-blue')
  .accentPalette('red');

  // Formata de data brasileiro
  $mdDateLocaleProvider.formatDate = function(date) {
      return date ? moment(date).format('DD/MM/YYYY') : null;
  };

  // Desativar os warnings de ARIA-LABEL (label para tecnologias assistivas)
  $mdAriaProvider.disableWarnings(); 

  $httpProvider.interceptors.push(function($q, $injector, $localStorage) {
      return {
        'request': function (config) {
          config.headers = config.headers || {};
          if ($localStorage.usuarioLogado) {
            config.headers.Authorization = 'Bearer ' + $localStorage.usuarioLogado.token;
          }

          return config;
        },
        'responseError': function(response) {
          switch (response.status) {
            case 401:
              var stateService = $injector.get('$state');
              stateService.go('login');
              toastr.error("Faça login novamente.","Token Expirado!",{progressBar:true,timeOut:3000})
              break;                

            default :
              return $q.reject(response);
          }
        }
      };
    })

});