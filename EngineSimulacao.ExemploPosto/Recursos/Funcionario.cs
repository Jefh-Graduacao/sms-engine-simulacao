using EngineSimulacao.Api;

namespace EngineSimulacao.ExemploPosto.Recursos
{
    public class Funcionario : Recurso
    {
        public Funcionario()
        {
            Gerenciador<Funcionario>.nascimento(this);
        }
    }
}
