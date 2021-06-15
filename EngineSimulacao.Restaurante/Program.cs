using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Eventos.Clientes;
using System;
using System.Collections.Generic;

namespace EngineSimulacao.Restaurante
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            MotorRestaurante.Inicializar();

            var evento = new ChegadaClientes();
            Agendador.AgendarAgora(evento);
            Agendador.Simular(MotorRestaurante.garcom.Executar);

            List<HistoricoBase> listaHistorico = ColetaDeDados.listaDeHistoricos;

            foreach (var historico in listaHistorico)
            {
                Console.WriteLine("\n\n------------");
                Console.WriteLine(historico.nome + " maior tempo de vida " + historico.maiorTempoDeVida());
                Console.WriteLine(historico.nome + " menor tempo de vida " + historico.menorTempoDeVida());
                Console.WriteLine(historico.nome + " tempo médio de vida " + historico.tempoMedioDeVida());
                Console.WriteLine("------------\n\n");
            }
        }
    }
}
