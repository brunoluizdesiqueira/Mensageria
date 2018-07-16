namespace Mensageria.Domain.Entities
{
    public class Cliente
    {        
        public string Crm { get; private set; }
        public string Nome { get; private set; }
        public string Situacao { get; private set; }
        public string Caracteristica { get; private set; }
        public Cliente(string crm, string nome, string situacao, string caracteristica) 
        {
            this.Crm = crm;
                this.Nome = nome;
                this.Situacao = situacao;
                this.Caracteristica = caracteristica;          
        }

        public override string ToString()
        {
            return string.Format(
                "Crm: {0},\nNome: {1},\nSituacao: {2},\nCaracteristica",
                Crm,
                Nome,
                Situacao,
                Caracteristica);
        }

    }
}