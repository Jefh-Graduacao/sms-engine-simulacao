using EngineSimulacao.Api;
using EngineSimulacao.ExemploPosto.Eventos;

namespace EngineSimulacao.ExemploPosto
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var motor = new MotorPostoGasolina();
            motor.Agendador.CriarRecurso("funcionarios", new Recurso(1, "Funcionários", 3));
            var evtInicial = new ChegadaCarros(motor);
            motor.Agendador.AgendarAgora(evtInicial);
            motor.Agendador.Simular();
        }
    }
}
