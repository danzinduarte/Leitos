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
    public class TestClientes
    {
        private readonly DataDbContext context;
        private readonly  acesso_siafController controller;
        
        public TestClientes()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
            optionsBuilder.UseNpgsql("Server= localhost;Port=5432;user id=postgres;password=1234;database=ControleDeAcesso");

            context = new DataDbContext(optionsBuilder.Options);
            var acesso_siafRepo = new acesso_siafRepository(context);
            controller = new acesso_siafController(acesso_siafRepo);
            
        }
        [TestCategory("Controle de Testes Clientes")]
        [TestMethod]
        public void deve_salvar_objeto_perfeito_informandoCPF()
        {
            var cliente = new acesso_siaf()
            {
                numeroserie = "1234567891234",
                cpf = "05282083577",
                cnpj = "",
                status = "S",
                contrato = "M",
                androidgourmet = "N",
                
                androidpedidos = "S",
                numdispositivospedidos = 3,
                observacao = ""
            };
            controller.Create(cliente);

            Assert.AreEqual(0, cliente.numdispositivo);
        }

        [TestMethod]
        public void deve_salvar_objeto_perfeito_informandoCNPJ()
        {
            var cliente = new acesso_siaf()
            {
                numeroserie = "1234567891234",
                cpf = "",
                cnpj = "99.999.999/9999-99",
                status = "S",
                contrato = "M",
                androidgourmet = "S",
                numdispositivo = 1,
                androidpedidos = "S",
                numdispositivospedidos = 3,
                observacao = ""
            };
            controller.Create(cliente);

            Assert.AreNotEqual(0, cliente.id);
        }
        [TestMethod]
        public void nao_salvar_numeroserie_vazio()
        {
            var cliente = new acesso_siaf()
            {
                //numeroserie = "1234567891234",
                cpf = "",
                cnpj = "99.999.999/9999-99",
                status = "S",
                contrato = "M",
                androidgourmet = "S",
                numdispositivo = 1,
                androidpedidos = "S",
                numdispositivospedidos = 3,
                observacao = ""
            };
            controller.Create(cliente);

            Assert.AreEqual(0, cliente.id);
        }
        [TestMethod]
        public void deve_salvar_androidgourmet_nao_ativado_numdispositivo_0()
        {
            var cliente = new acesso_siaf()
            {
                numeroserie = "1234567891234",
                cpf = "",
                cnpj = "99.999.999/9999-99",
                status = "S",
                contrato = "M",
                androidgourmet = "N",
                numdispositivo = 1,
                androidpedidos = "S",
                numdispositivospedidos = 3,
                observacao = ""
            };
            controller.Create(cliente);

            Assert.IsTrue(cliente.numdispositivo == 0);
        }
        [TestMethod]
        public void deve_salvar_androidpedidos_nao_ativado_numdispositivospedidos_0()
        {
            var cliente = new acesso_siaf()
            {
                numeroserie = "1234567891234",
                cpf = "",
                cnpj = "99.999.999/9999-99",
                status = "S",
                contrato = "M",
                androidgourmet = "S",
                numdispositivo = 1,
                androidpedidos = "N",
                numdispositivospedidos = 3,
                observacao = ""
            };
            controller.Create(cliente);

            Assert.IsTrue(cliente.numdispositivospedidos == 0);
        }
         [TestMethod]
        public void nao_deve_salvar_observacao_null()
        {
            var cliente = new acesso_siaf()
            {
                numeroserie = "1234567891234",
                cpf = "",
                cnpj = "99.999.999/9999-99",
                status = "S",
                contrato = "M",
                androidgourmet = "S",
                numdispositivo = 1,
                androidpedidos = "N",
                numdispositivospedidos = 0,
                //observacao = ""
            };
            controller.Create(cliente);

             Assert.AreEqual(0, cliente.id);
        }
    }
}
