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
        public static double TempoParaFinalizar => (new GeradorDeRandomicosLCG()).normal(12.0, 1.5);
        public static double TempoEntreCarros => (new GeradorDeRandomicosLCG()).normal(2.0, 0.3);
        public const int TempoMaximoChegadaCarros = 100;
    }
}