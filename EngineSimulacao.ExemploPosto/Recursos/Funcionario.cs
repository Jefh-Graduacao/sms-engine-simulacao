namespace EngineSimulacao.Api
{
    public class Funcionario : Recurso
    {
        public Funcionario(){
            Gerenciador<Funcionario>.nascimento(this);
        }
    }
}
