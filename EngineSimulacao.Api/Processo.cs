namespace EngineSimulacao.Api
{   
    //Possívelmente será removido, pois iremos desenvolver com modelagem baseada em eventos
    public abstract class Processo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Duração { get; set; }
        public bool Ativo { get; set; }
        Processo(int Id, string Nome, decimal Duração){
            this.Id = Id;
            this.Nome = Nome;
            this.Duração = Duração;
            this.Ativo = false;
        }
    }
}