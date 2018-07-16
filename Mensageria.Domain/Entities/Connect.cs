using System.Threading.Tasks;
using Amqp;
using Amqp.Framing;
using Amqp.Listener;
using Amqp.Types;

namespace Mensageria.Domain.Entities
{
    public class Connect
    {
        private Connection connection;
        public string address { get; private set; }
        public Session session { get; set; }
        public Connect()
        {
            Run().Wait();
        }
        private async Task Run()
        {
            this.address = "amqp://guest:guest@localhost:5672";
            this.connection = await Connection.Factory.CreateAsync(new Address(address));
            this.session = new Session(this.connection);
        }

    }
}