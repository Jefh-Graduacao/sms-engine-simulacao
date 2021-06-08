using EngineSimulacao.Api;

namespace EngineSimulacao.ExemploPosto.Entidades
{
    public sealed class Carro : Entidade
    {
        public Carro()
        {
            Gerenciador<Carro>.nascimento(this);
        }

        protected override void DestruirInstancia()
        {
            Gerenciador<Carro>.morte(this);
        }
    }
}