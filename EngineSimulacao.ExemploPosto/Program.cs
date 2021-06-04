using System;
using EngineSimulacao.Api;
using EngineSimulacao.ExemploPosto.Eventos;

namespace EngineSimulacao.ExemploPosto
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var motor = new MotorExecucao<ConjuntosPosto>();
            motor.Agendador.CriarRecurso("funcionarios", new Recurso(1, "Funcionários", CONFIG.TOTAL_FUNCIONARIOS));
            var evtIniciar = motor.criarEvento<ChegadaCarros>();
            motor.Agendador.AgendarAgora(evtIniciar);
            motor.Agendador.Simular();
        }
    }
}
