using EngineSimulacao.Api;
using EngineSimulacao.ExemploPosto.Eventos;

namespace EngineSimulacao.ExemploPosto
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var motor = new MotorExecucao<MemoriaPostoGasolina>();
            motor.Agendador.CriarRecurso("funcionarios", new Recurso(1, "Funcionários", 3));
            var evtIniciar = motor.criarEvento<ChegadaCarros>();
            motor.Agendador.AgendarAgora(evtIniciar);
            motor.Agendador.Simular();
        }
    }
}
