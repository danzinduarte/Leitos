angular.module('app.usuario')
.controller('UsuarioListaController', usuarioListaController);

function usuarioListaController(usuarioService, $state,$mdDialog,$localStorage) {
	
	vm = this;

	function init(){
        
        carregaUsuarios()
    
	}

	init()

    vm.cancelar             = cancelar;
    vm.excluir              = excluir;
    vm.exclui               = exclui;
    vm.novoUsuario        = novoUsuario
    vm.carregaUsuarios    = carregaUsuarios
    vm.editaUsuario       = editaUsuario
    
	function carregaUsuarios(){
        usuarioService.getAll().then(function(resposta){
            console.log(resposta)
            vm.dataset = resposta.data
        })
    } 

	function novoUsuario(){
		$state.go('usuario-novo')	
    }
    
    function editaUsuario(usuarioId) {
		$state.go('usuario-editar', {id : usuarioId})		
	}
	
	function exclui(ev,usuarios){
		
        let confirmacao = $mdDialog.confirm()
                .title('Aguardando confirmação')
                .textContent('Confirma a exclusao do usuario ' + usuarios.nome)
                .ariaLabel('Msg interna do botao')
                .targetEvent(ev)
                .ok('Sim')
                .cancel('Não');

        $mdDialog.show(confirmacao).then(function() {
                vm.excluir(usuarios.id)
        });
    }

    function excluir(usuarioId){

        let sucesso = function(resposta){			
            if (resposta.sucesso) {
				toastr.info("Usuario excluido com sucesso","SUCESSO")
            }
            carregaUsuarios();
        }

        let erro = function(resposta){
            console.log(resposta)	
            toastr.warning("Erro ao excluir usuario")	
        }

        usuarioService.delete(usuarioId).then(sucesso,erro)
    }

    function cancelar() {
        $state.go('usuario')
    }

   
}