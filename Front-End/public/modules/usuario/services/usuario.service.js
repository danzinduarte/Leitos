angular.module('app.usuario')
.factory('usuarioService', function(api) {
    
    var usuarioFactory = {};

    usuarioFactory.getAll = function (){
        var ds = new api.usuario();
        return ds.$get()
    }

    usuarioFactory.getById = function(usuarioId) {
        
        var ds = new api.usuario();
        ds.id = usuarioId;
        return ds.$get()
    }
    
    usuarioFactory.save = function(usuarioModel){
        var ds = new api.usuario();
        ds.usuario = usuarioModel;
        ds.id = usuarioModel.id

        if (usuarioModel.id) {
            return ds.$update()											
		} else {
			return ds.$save()               				
		}
        
    }

    usuarioFactory.delete = function(usuarioId){
        var ds = new api.usuario();
        ds.id = usuarioId

        return ds.$delete({id : usuarioId})
    }

    return usuarioFactory;

});