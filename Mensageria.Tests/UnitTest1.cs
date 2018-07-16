using Mensageria.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mensageria.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var cliente = new Cliente("1", "Fulano", "Suspenso", "Cancelado"); 
        }
    }
}
