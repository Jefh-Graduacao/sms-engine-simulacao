using System;
using EngineSimulacao.Api;
using EngineSimulacao.ExemploPosto.Eventos;
using System.Collections.Generic;

namespace EngineSimulacao.ExemploPosto
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var evtIniciar = new ChegadaCarros();
            Agendador.AgendarAgora(evtIniciar);
            Agendador.Simular();
            List<dynamic> listaHistorico = ColetaDeDados.listaDeHistoricos;
        }
    }
}
