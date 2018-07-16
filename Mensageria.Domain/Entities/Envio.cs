namespace Mensageria.Domain.Entities
{
    using System;
    using Amqp;
    using Amqp.Framing;
    using Amqp.Listener;
    using Amqp.Types;
    using Amqp.Serialization;
    using Mensageria.Shared;
    public class Envio
    {
        private Session session;
        private SenderLink sender;
        private AmqpSerializer serializer;
        private string name;
        private string address;
        private int quantidade;
        private Cliente cliente;
        private Message message;

        public Envio(Session session, string name, string address, int quantidade, Cliente cliente)
        {
            this.session = session;
            this.name = name;
            this.address = address;
            this.quantidade = quantidade;
            this.cliente = cliente;

            this.serializer = new AmqpSerializer(new ContractResolver()
            {
                PrefixList = new[] { "Mensageria.Domain.Entities" }
            });
        }
        public void Send()
        {
            this.sender = new SenderLink(this.session, this.name, this.address);
            try
            {
                for (int i = 0; i < this.quantidade; i++)
                {      
                    SendMessage(this.sender, "Cliente", this.cliente, serializer);
                    Console.WriteLine("Enviado: \n{0}", cliente.ToString());
                }
           }
           finally
           {
               sender.Close();
               session.Close();
           }
        }
        private void SendMessage(SenderLink sender, string subject, object value, AmqpSerializer serializer)
        {
            this.message = new Message() { BodySection = new AmqpValue<object>(value, serializer) };

            this.message.ApplicationProperties = new ApplicationProperties();
            this.message.ApplicationProperties["Message.Type.FullName"] = typeof(Cliente).FullName;
            this.message.Properties = new Properties() { MessageId = Guid.NewGuid().ToString() };
            this.message.Properties = new Properties() { Subject = subject };

            sender.Send(this.message);
        }
        
    }
}