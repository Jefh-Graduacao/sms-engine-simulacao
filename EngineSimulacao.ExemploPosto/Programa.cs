using EngineSimulacao.Api;
using EngineSimulacao.ExemploPosto.Eventos;
using EngineSimulacao.ExemploPosto.Recursos;
using System;
using System.Collections.Generic;

namespace EngineSimulacao.ExemploPosto
{
    public static class Programa
    {
        public static void Main(string[] args)
        {
            GerenciadorDeRecursos<Funcionario>.CriarRecursos(MotorPosto.TotalFuncionarios);

            var evtIniciar = new ChegadaCarros();
            Agendador.AgendarAgora(evtIniciar);
            Agendador.Simular();

            List<HistoricoBase> listaHistorico = ColetaDeDados.ListaDeHistoricos;

            foreach (var historico in listaHistorico)
            {
                Console.WriteLine("\n\n------------");
                Console.WriteLine(historico.Nome + " maior tempo de vida " + historico.maiorTempoDeVida());
                Console.WriteLine(historico.Nome + " menor tempo de vida " + historico.menorTempoDeVida());
                Console.WriteLine(historico.Nome + " tempo médio de vida " + historico.tempoMedioDeVida());
                Console.WriteLine("------------\n\n");
            }
        }
    }
}
