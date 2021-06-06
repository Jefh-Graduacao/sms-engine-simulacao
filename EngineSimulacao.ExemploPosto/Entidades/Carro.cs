using EngineSimulacao.Api;

namespace EngineSimulacao.ExemploPosto
{
    public sealed class Carro : Entidade
    {
        public Carro() {
            Gerenciador<Carro>.nascimento(this);
        }

        protected override void DestruirIntancia()
        {
            Gerenciador<Carro>.morte(this);
        }
    }
}