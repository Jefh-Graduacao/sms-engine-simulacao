﻿using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Entidades;
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

            estatisticas(listaHistorico);

        }

        private static void estatisticas(List<HistoricoBase> listaHistorico)
        {
            Historico<ChegadaClientes> HistoricoChegadaClientes;

            HistoricoChegadaClientes = (Historico<ChegadaClientes>)GetHistoricoBase(listaHistorico, "Histórico EventoGerenciado ChegadaClientes");

            Historico<GrupoClientes> HistoricoFilaCx1, HistoricoFilaCx2,
                                     BancoBalcao, Mesas2L, Mesas4L,
                                     FilaPedidosCozinha, FilaPedidosEntrega;

            HistoricoFilaCx1 = (Historico<GrupoClientes>)GetHistoricoBase(listaHistorico, "Histórico ConjuntoEntidade Fila Caixa 1");
            HistoricoFilaCx2 = (Historico<GrupoClientes>)GetHistoricoBase(listaHistorico, "Histórico ConjuntoEntidade Fila Caixa 2");

            BancoBalcao = (Historico<GrupoClientes>)GetHistoricoBase(listaHistorico, "Histórico ConjuntoEntidade Fila Balcão");
            Mesas2L = (Historico<GrupoClientes>)GetHistoricoBase(listaHistorico, "Histórico ConjuntoEntidade Fila Mesas de 2 Lugares");
            Mesas4L = (Historico<GrupoClientes>)GetHistoricoBase(listaHistorico, "Histórico ConjuntoEntidade Fila Mesas de 4 Lugares");

            FilaPedidosCozinha = (Historico<GrupoClientes>)GetHistoricoBase(listaHistorico, "Histórico ConjuntoEntidade Fila Pedidos Cozinha");
            FilaPedidosEntrega = (Historico<GrupoClientes>)GetHistoricoBase(listaHistorico, "Histórico ConjuntoEntidade Fila de Pedidos para Entrega");

            if (HistoricoChegadaClientes != null)
                Console.WriteLine("Chegaram " + HistoricoChegadaClientes.lista.Count + " clientes.");

            ImprimirNoConsole(HistoricoFilaCx1, "clientes", "fila 1");
            ImprimirNoConsole(HistoricoFilaCx2, "clientes", "fila 2");
            Console.WriteLine("");
            
            ImprimirNoConsole(BancoBalcao, "clientes", "fila do balcão");
            ImprimirNoConsole(Mesas2L, "clientes", "fila de mesas 2 lugares");
            ImprimirNoConsole(Mesas4L, "clientes", "fila de mesas 4 lugares");
            Console.WriteLine("");
            
            ImprimirNoConsole(FilaPedidosCozinha, "pedidos", "fila de pedidos para cozinha");
            ImprimirNoConsole(FilaPedidosEntrega, "pedidos", "fila de pedidos para entrega");

        }

        private static void ImprimirNoConsole(Historico<GrupoClientes> hisGrupoClientes, string oQuePassaPelafila, string nomeDaFila)
        {
            var texto = " "+ oQuePassaPelafila +" passaram pela ";

            if (hisGrupoClientes != null)
                Console.WriteLine(hisGrupoClientes.lista.Count + texto + nomeDaFila + ".");

        }

        private static HistoricoBase GetHistoricoBase(List<HistoricoBase> listaHistorico, string nome)
        {
            foreach (var hisBase in listaHistorico)
            {
                if (hisBase.nome.Equals(nome))
                {
                    return hisBase;
                }
            }
            return null;
        }

    }
}
