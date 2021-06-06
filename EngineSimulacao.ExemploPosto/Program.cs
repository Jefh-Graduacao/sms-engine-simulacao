﻿using System;
using EngineSimulacao.Api;
using EngineSimulacao.ExemploPosto.Eventos;
using System.Collections.Generic;

namespace EngineSimulacao.ExemploPosto
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            GerenciadorDeRecursos<Funcionario>.criarNRecursos(MotorPosto.TOTAL_FUNCIONARIOS);
            var evtIniciar = new ChegadaCarros();
            Agendador.AgendarAgora(evtIniciar);
            Agendador.Simular();
            List<HistoricoBase> listaHistorico = ColetaDeDados.listaDeHistoricos;
            
            foreach(var historico in listaHistorico)
            {
                Console.WriteLine("\n\n------------");
                Console.WriteLine(historico.nome + " maior tempo de vida " + historico.maiorTempoDeVida() );
                Console.WriteLine(historico.nome + " menor tempo de vida " + historico.menorTempoDeVida() );
                Console.WriteLine(historico.nome + " tempo médio de vida " + historico.tempoMedioDeVida() );
                Console.WriteLine("------------\n\n");
            }
        }
    }
}
