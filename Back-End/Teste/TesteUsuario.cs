using System;
using api.Models;
using api.Repository;
using api.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;



namespace api.test
{
    [TestClass]
    public class TestUsuario
    {
        private readonly DataDbContext context;
        private readonly  UsuarioController controller;
        
        public TestUsuario()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
            optionsBuilder.UseNpgsql("Server= localhost;Port=5432;user id=postgres;password=1234;database=ControleDeAcesso");

            context = new DataDbContext(optionsBuilder.Options);
            var usuarioRepo = new usuarioRepository(context);
            controller = new UsuarioController(usuarioRepo);
            
        }
        [TestCategory("Controle de Testes Usuario")]
        [TestMethod]
        public void deve_salvar_objeto_perfeito()
        {
            var usuario = new Usuario()
            {
                email = "danzinduarte@hotmail.com",
                nome = "Danilo Duarte",
                senha = "123456",
                administrador = true,
                desativado = false
            };
            controller.Create(usuario);

            Assert.AreNotEqual(0, usuario.id);
        }
        [TestMethod]
        public void verificar_log_criacao()
        {
            var usuario = new Usuario()
            {
                email = "danzinduarte@hotmail.com",
                nome = "Danilo Duarte",
                senha = "123456",
                administrador = true,
                desativado = false
            };
            controller.Create(usuario);

            Assert.IsFalse(usuario.log_criacao == DateTime.Now);
        }
        [TestMethod]
        public void verificar_log_atualizacao()
        {
            var usuario = new Usuario()
            {
                email = "danzinduarte@hotmail.com",
                nome = "Danilo Duarte",
                senha = "123456",
                administrador = true,
                desativado = false
            };
            controller.Create(usuario);

            Assert.IsFalse(usuario.log_atualizacao == DateTime.Now);
        }
        [TestMethod]
        public void verificar_data_desativacao()
        {
            var usuario = new Usuario()
            {
                email = "danzinduarte@hotmail.com",
                nome = "Danilo Duarte",
                senha = "123456",
                administrador = true,
                desativado = true
            };
            controller.Create(usuario);

            Assert.IsFalse(usuario.data_desativacao == DateTime.Now);
        }
       [TestMethod]
        public void nao_deve_salvar_sem_email()
        {
            var usuario = new Usuario()
            {
               
                nome = "Danilo Duarte",
                senha = "123456",
                administrador = true,
                desativado = false
            };
            controller.Create(usuario);

            Assert.AreEqual(0, usuario.id);
        }
        [TestMethod]
        public void nao_deve_salvar_sem_nome()
        {
            var usuario = new Usuario()
            {
                email = "danzinduarte@hotmail.com",
                //nome = "Danilo Duarte",
                senha = "123456",
                administrador = true,
                desativado = false
            };
            controller.Create(usuario);

            Assert.AreEqual(0, usuario.id);
        }
        [TestMethod]
        public void nao_deve_salvar_sem_senha()
        {
            var usuario = new Usuario()
            {
                email = "danzinduarte@hotmail.com",
                nome = "Danilo Duarte",
                //senha = "123456",
                administrador = true,
                desativado = false
            };
            controller.Create(usuario);

            Assert.AreEqual(0, usuario.id);
        }
    }
}
