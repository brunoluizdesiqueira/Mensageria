using System;
using System.Threading.Tasks;
using Amqp;
using Amqp.Serialization;
using Mensageria.Shared;

namespace Mensageria.Domain.Entities
{
    public class Recebimento
    {
        private Session session;
        private ReceiverLink receiver;
        private string name;
        private string address;
        private Cliente cliente;
        private AmqpSerializer serializer;

        public Recebimento(Session session, string name, string address, Cliente cliente)
        {
            this.session = session;
            this.name = name;
            this.address = address;
            this.cliente = cliente;

            this.serializer = new AmqpSerializer(new ContractResolver()
            {
                PrefixList = new[] { "Mensageria.Domain.Entities" }
            });
        }

        public async Task Run()
        { 
            this.receiver = new ReceiverLink(this.session, this.name, this.address);
            this.receiver.Start(5, OnMessageCallback);
            Console.Read();
        }

        private void OnMessageCallback(IReceiverLink receiver, Message message)
        {
            try
            {   
                //var messageType = message.ApplicationProperties["Message.Type.FullName"];
                this.cliente = message.GetBody<Cliente>(serializer);
                Console.WriteLine("Received {0}", this.cliente);
                receiver.Accept(message);  
              }
              catch (Exception ex)
              {
                  receiver.Reject(message);
                  Console.WriteLine(ex);
              }
        }
    }
}