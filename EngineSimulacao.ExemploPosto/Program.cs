using System;
using EngineSimulacao.Api;
using EngineSimulacao.ExemploPosto.Eventos;

namespace EngineSimulacao.ExemploPosto
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Agendador.CriarRecurso("funcionarios", new Recurso(1, "Funcionários", MotorPosto.TOTAL_FUNCIONARIOS));
            var evtIniciar = new ChegadaCarros();
            Agendador.AgendarAgora(evtIniciar);
            Agendador.Simular();
        }
    }
}
