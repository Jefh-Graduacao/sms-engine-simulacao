using System;
using System.Collections.Generic;
using System.Linq;
using EngineSimulacao.Api;
using EngineSimulacao.RestauranteSemGarcom.Entidades;
using EngineSimulacao.RestauranteSemGarcom.Eventos.Clientes;
using EngineSimulacao.RestauranteSemGarcom.Recursos;

namespace EngineSimulacao.RestauranteSemGarcom
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            MotorRestaurante.Inicializar();

            var evento = new ChegadaClientes();
            Agendador.AgendarAgora(evento);
            Agendador.Simular(CallbackMotor);

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

        private static List<(int, int)> listaHistoricoFila1 = new();
        public static void CallbackMotor()
        {
            if ((int) Agendador.Tempo % 10 == 0)
            {
                listaHistoricoFila1.Add(((int)Agendador.Tempo, MotorRestaurante.FilaCaixa1.TamanhoAtual));
            }
        }

        private static void MostrarEstatisticas(List<HistoricoBase> listaHistorico)
        {
            var historicoChegadaClientes = (Historico<ChegadaClientes>)GetHistoricoBase(listaHistorico, "Histórico EventoGerenciado ChegadaClientes");

            var historicoFilaCx1 = (Historico<GrupoClientes>)GetHistoricoBase(listaHistorico, "Histórico ConjuntoEntidade Fila Caixa 1");
            var historicoFilaCx2 = (Historico<GrupoClientes>)GetHistoricoBase(listaHistorico, "Histórico ConjuntoEntidade Fila Caixa 2");

            var bancoBalcao = (Historico<GrupoClientes>)GetHistoricoBase(listaHistorico, "Histórico ConjuntoEntidade Fila Balcão");
            var mesas2L = (Historico<GrupoClientes>)GetHistoricoBase(listaHistorico, "Histórico ConjuntoEntidade Fila Mesas de 2 Lugares");
            var mesas4L = (Historico<GrupoClientes>)GetHistoricoBase(listaHistorico, "Histórico ConjuntoEntidade Fila Mesas de 4 Lugares");

            var filaPedidosCozinha = (Historico<GrupoClientes>)GetHistoricoBase(listaHistorico, "Histórico ConjuntoEntidade Fila Pedidos Cozinha");
            var filaPedidosEntrega = (Historico<GrupoClientes>)GetHistoricoBase(listaHistorico, "Histórico ConjuntoEntidade Fila de Pedidos para Entrega");

            if (historicoChegadaClientes != null)
                Console.WriteLine($"Chegaram {historicoChegadaClientes.lista.Count} clientes.");

            ImprimirNoConsole(historicoFilaCx1, "clientes", "fila 1");
            ImprimirNoConsole(historicoFilaCx2, "clientes", "fila 2");
            Console.WriteLine("");
            
            ImprimirNoConsole(bancoBalcao, "clientes", "fila do balcão");
            ImprimirNoConsole(mesas2L, "clientes", "fila de mesas 2 lugares");
            ImprimirNoConsole(mesas4L, "clientes", "fila de mesas 4 lugares");
            Console.WriteLine("");
            
            ImprimirNoConsole(filaPedidosCozinha, "pedidos", "fila de pedidos para cozinha");
            ImprimirNoConsole(filaPedidosEntrega, "pedidos", "fila de pedidos para entrega");

        }

        private static void ImprimirNoConsole(Historico<GrupoClientes> hisGrupoClientes, string oQuePassaPelafila, string nomeDaFila)
        {
            var texto = $"{oQuePassaPelafila} passaram pela";

            if (hisGrupoClientes != null)
                Console.WriteLine($"{hisGrupoClientes.lista.Count} {texto} {nomeDaFila}.");
        }

        private static HistoricoBase GetHistoricoBase(List<HistoricoBase> listaHistorico, string nome) 
            => listaHistorico.FirstOrDefault(hisBase => hisBase.Nome.Equals(nome));
    }
}
