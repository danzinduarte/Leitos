angular.module('appCtrl', [])
.controller('appCtrl', function($mdSidenav, $stateParams, $rootScope,$state,$localStorage,$mdDialog) {

    self = this;

    // Update title using rootscope
    self.updateTitle = function() {
        $rootScope.title = $stateParams.title;
    }

    // Run updateTitle on each state change
    $rootScope.$on('$stateChangeSuccess', self.updateTitle);

	self.toggleLeft = function() {
    	$mdSidenav('left').toggle();
    }

    self.toggleRight = function() {
    	$mdSidenav('right').toggle();
    }
    self.logado = function(){
        apelido = $localStorage.usuarioLogado.email.split(" ")
        nome    = apelido[0]
        return nome;
    }

    self.editaDados = function () {
        id = $localStorage.usuarioLogado.id
        $state.go('usuario-editar', {id : id})	
    }

    self.sair = function(ev){
        let confirmacao = $mdDialog.confirm()
        .title('Atenção')
        .textContent('Deseja realmente sair da aplicação?')
        .ariaLabel('Msg interna do botao')
        .targetEvent(ev)
        .ok('Sim')
        .cancel('Não');

        $mdDialog.show(confirmacao).then(function() {
            $state.go('login')
            toastr.info("Até mais :)")
        });
    }

})