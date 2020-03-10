angular.module('app.login')
.controller('LoginController', LoginController);

function LoginController($localStorage, loginService, $state) {
	
    vm = this;   
   

	function init(){
        $localStorage.usuarioLogado = '';
    }

    init()

    vm.signUp = fnSignUp;

    function fnSignUp(){
        let sucesso = function(resposta){
				
            $localStorage.usuarioLogado = resposta.data;
            apelido = $localStorage.usuarioLogado.pessoa.nome.split(" ")
            nome = apelido[0]
            $state.go('home')
            toastr.success('Seja Bem Vindo ' + nome + '!')
        }
        
        let erro = function(resposta){
            if (resposta.statusText == '') {
                toastr.warning("Desculpe, houve um erro em nosso servidor")
            } else {
                toastr.warning("Nome de usuário ou senha inválidos!")
            }
        }
        
        loginService.signUp(vm.email,vm.senha).then(sucesso,erro)

    }
    (function ($) {
        "use strict";
    
        /*==================================================================
        [ Validate ]*/
        var input = $('.validate-input .input100');
    
        $('.validate-form').on('submit',function(){
            var check = true;
    
            for(var i=0; i<input.length; i++) {
                if(validate(input[i]) == false){
                    showValidate(input[i]);
                    check=false;
                }
            }
    
            return check;
        });
    
        $('.validate-form .input100').each(function(){
            $(this).focus(function(){
               hideValidate(this);
            });
        });
    
        function validate (input) {
            if($(input).attr('type') == 'email' || $(input).attr('name') == 'email') {
                if($(input).val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) == null) {
                    return false;
                }
            }
            else {
                if($(input).val().trim() == ''){
                    return false;
                }
            }
        }
    
        function showValidate(input) {
            var thisAlert = $(input).parent();
    
            $(thisAlert).addClass('alert-validate');
        }
    
        function hideValidate(input) {
            var thisAlert = $(input).parent();
    
            $(thisAlert).removeClass('alert-validate');
        }
        
    })(jQuery);
}