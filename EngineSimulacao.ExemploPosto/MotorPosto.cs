using EngineSimulacao.Api;
using EngineSimulacao.ExemploPosto.Entidades;

namespace EngineSimulacao.ExemploPosto
{
    public static class MotorPosto
    {
        //Modificar para funções com retorno probabilístico, caso necessário
        public static readonly ConjuntoEntidade<Carro> FilaAtendimento = new("FilaAtendimento");

        public const int FuncionariosNecessarios = 1;
        public const int TotalFuncionarios = 2;
        public const int TempoParaFinalizar = 12;
        public const int TempoEntreCarros = 2;
        public const int TempoMaximoChegadaCarros = 100;
    }
}