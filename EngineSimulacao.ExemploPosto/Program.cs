using System;
using EngineSimulacao.Api;
using EngineSimulacao.ExemploPosto.Eventos;

namespace EngineSimulacao.ExemploPosto
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var evtIniciar = new ChegadaCarros();
            Agendador.AgendarAgora(evtIniciar);
            Agendador.Simular();
        }
    }
}
