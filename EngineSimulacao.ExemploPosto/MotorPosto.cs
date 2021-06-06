using EngineSimulacao.Api;

namespace EngineSimulacao.ExemploPosto
{
    public static class MotorPosto
    {
        //Modificar para funções com retorno probabilístico, caso necessário
        public static ConjuntoEntidade<Carro> filaAtendimento = new();
        public static int FUNCIONARIOS_NECESSARIOS => 2;
        public static int TOTAL_FUNCIONARIOS => 6;
        public static int TEMPO_PARA_FINALIZAR => 12;
        public static int TEMPO_ENTRE_CARROS => 2;
        public static int TEMPO_MAXIMO_CHEGADA_CARROS => 100;
    }
}