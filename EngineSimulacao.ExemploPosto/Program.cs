using EngineSimulacao.Api;
using EngineSimulacao.ExemploPosto.Eventos;

namespace EngineSimulacao.ExemploPosto
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var agendador = new Agendador();
            var motor = new MotorPostoGasolina
            {
                Agendador = agendador
            };

            agendador.MotorExecucao = motor;

            agendador.CriarRecurso("funcionarios", new Recurso(1, "Funcionários", 3));
            agendador.AgendarAgora(new ChegadaCarros(""));

            agendador.Simular();
        }
    }
}
