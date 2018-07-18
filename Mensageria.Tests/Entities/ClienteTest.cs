using Mensageria.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mensageria.Tests.Entities
{
    [TestClass]
    public class ClienteTest
    {
        [TestMethod]
        public void DeveSerPossivelCadastrarCliente()
        {
            var cliente = new Cliente("2", "Fulano de tal", "Suspenso", "Cancelado");  
            cliente.ToString();  
        }

        [TestMethod]
        public void DeveSerPossivelEnviarCLiente()
        {
            var cliente = new Cliente("1", "Fulano", "Suspenso", "Cancelado"); 
            var connect = new Connect();
            var envio = new Envio(connect.session, "sender-link","immobile", 100, cliente);
            envio.Send();
        }
        
        [TestMethod]
        public void DeveSerPossivelConsumirCLiente()
        {
            Cliente cliente = new Cliente();
            var connect = new Connect();
            var recebimento = new Recebimento(connect.session, "test-receiver", "immobile", cliente);
            recebimento.Run().Wait();     
        }
    }
}