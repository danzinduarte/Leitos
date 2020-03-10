angular.module('app.usuario')
.controller('UsuarioController', usuarioController);

function usuarioController(usuarioService, $localStorage,$state,usuarioId) {
	
    vm = this;
    
    vm.query = {
        text : ''
    }

	function init(){

        if (usuarioId) {
           usuarioService.getById(usuarioId).then(function(usuarioModel){
                console.log(usuarioModel.data)
                vm.ds = usuarioModel.data
                vm.ds.usuarioSenha1 = vm.ds.usuario.senha
            })  
        }
       
    }    
    
    init()     
    
    vm.cancelar             = cancelar
    vm.salvar               = salvar;
    vm.show                 = show
    
    function cancelar() {
        $state.go('usuario')
    }

    function salvar(){
        let usuarioModel = {
           
            usuario : {
                email : vm.ds.usuario.email,
                senha : vm.ds.usuario.senha,
                nome : vm.ds.usuario.nome
            }
        };
        
        let sucesso = function(resposta){
			console.log(resposta)
			
			if (resposta.sucesso) {				

				if (usuarioId) {
					toastr.info("Usuario atualizado com êxito","SUCESSO",{progressBar:true,timeOut:3000})
				} else {
					toastr.success("Usuario incluído com êxito :)","SUCESSO",{progressBar:true,timeOut:3000})	
				}
    
                $state.go('usuario')
			}
			
		}

		let erro = function(resposta){
            if (resposta.resource.usuarios.id) {
                toastr.warning("Erro ao editar o usuario")
            } else {
                toastr.warning("Erro ao cadastrar usuario")
            }	
            
        }
        
        usuariModel.id = usuarioId;
        usuarioService.save(usuarioModel).then(sucesso,erro)    
    
    }
    
}