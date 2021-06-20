using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Entidades;
using EngineSimulacao.Restaurante.Eventos.Clientes;
using EngineSimulacao.Restaurante.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EngineSimulacao.Restaurante
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            MotorRestaurante.Inicializar();

            var evento = new ChegadaClientes();
            Agendador.AgendarAgora(evento);
            Agendador.Simular(() => CallbackMotor(MotorRestaurante.garcom));

            List<HistoricoBase> listaHistorico = ColetaDeDados.ListaDeHistoricos;

            foreach (var historico in listaHistorico)
            {
                Console.WriteLine("\n\n------------");
                Console.WriteLine(historico.Nome + " maior tempo de vida " + historico.maiorTempoDeVida());
                Console.WriteLine(historico.Nome + " menor tempo de vida " + historico.menorTempoDeVida());
                Console.WriteLine(historico.Nome + " tempo médio de vida " + historico.tempoMedioDeVida());
                Console.WriteLine("------------\n\n");
            }

            MostrarEstatisticas(listaHistorico);
        }

        private static List<(double tempo, int quantidade)> RegistroFila1 = new();
        private static void CallbackMotor(Garcom garcom)
        {
            garcom.RedePetri.ExecutarCiclo();
            RegistroFila1.Add((Agendador.Tempo, MotorRestaurante.FilaCaixa1.TamanhoAtual));

        }

        private static void MostrarEstatisticas(List<HistoricoBase> listaHistorico)
        {
            var filaCaixa1 = RegistroFila1.GroupBy(x => (int) x.tempo)
                .Select(x => (x.Key, x.First().quantidade))
                .Select(x => $"{x.Key};{x.quantidade}")
                .Aggregate((a, b) => a + Environment.NewLine + b);



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
                if (hisBase.Nome.Equals(nome))
                {
                    return hisBase;
                }
            }
            return null;
        }

    }
}
