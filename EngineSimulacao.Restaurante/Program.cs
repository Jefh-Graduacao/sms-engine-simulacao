using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Entidades;
using EngineSimulacao.Restaurante.Eventos.Clientes;
using EngineSimulacao.Restaurante.Recursos;
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
            
            //Simular Todo Sistema
            Agendador.Simular(() => CallbackMotor(MotorRestaurante.Garcom));

            //Simular Por determinado tempo
            //Agendador.SimularPorDeterminadoTempo(180, () => CallbackMotor(MotorRestaurante.Garcom));

            List<HistoricoBase> listaHistorico = ColetaDeDados.ListaDeHistoricos;

            foreach (var historico in listaHistorico)
            {
                Console.WriteLine("\n\n------------");
                Console.WriteLine(historico.Nome + " maior tempo de vida " + historico.maiorTempoDeVida());
                Console.WriteLine(historico.Nome + " menor tempo de vida " + historico.menorTempoDeVida());
                Console.WriteLine(historico.Nome + " tempo médio de vida " + historico.tempoMedioDeVida());
                Console.WriteLine("------------\n\n");
            }

            MostrarEstatisticasDeFilas();
            MostrarEstatisticasDosRecursos();
        }

        private static void CallbackMotor(Garcom garcom)
        {
            garcom.RedePetri.ExecutarCiclo();
        }

        private static void MostrarEstatisticasDosRecursos()
        {
            var alocacaoRecursoBalcao = ColetaDeDados.GetHistoricoBase("Histórico AlocacaoGerenciada`1< BancoBalcao >");
            var alocacaoRecursoMesas2L = ColetaDeDados.GetHistoricoBase("Histórico AlocacaoGerenciada`1< Mesa2Lugares >");
            var alocacaoRecursoMesas4L = ColetaDeDados.GetHistoricoBase("Histórico AlocacaoGerenciada`1< Mesa4Lugares >");
            var alocacaoRecursoAtendenteCx1 = ColetaDeDados.GetHistoricoBase("Histórico AlocacaoGerenciada`1< AtendenteCaixa1 >");
            var alocacaoRecursoAtendenteCx2 = ColetaDeDados.GetHistoricoBase("Histórico AlocacaoGerenciada`1< AtendenteCaixa2 >");
            var alocacaoRecursoCozinheiro = ColetaDeDados.GetHistoricoBase("Histórico AlocacaoGerenciada`1< Cozinheiro >");

            ImprimirEstatisticaRecursos(alocacaoRecursoBalcao, "Banco Balcao");
            ImprimirEstatisticaRecursos(alocacaoRecursoMesas2L, "Mesas 2 Lugares");
            ImprimirEstatisticaRecursos(alocacaoRecursoMesas4L, "Mesas 4 Lugares");
            ImprimirEstatisticaRecursos(alocacaoRecursoAtendenteCx1, "Atendente Caixa 1");
            ImprimirEstatisticaRecursos(alocacaoRecursoAtendenteCx2, "Atendente Caixa 2");
            ImprimirEstatisticaRecursos(alocacaoRecursoCozinheiro, "Cozinheiro");
        }

        private static void ImprimirEstatisticaRecursos(HistoricoBase x, string recursoNome)
        {
            if (x != null)
                Console.WriteLine($"O tempo médio que o recurso {recursoNome} fica alocado é de "
                                + $"{x.tempoMedioDeVida().ToString("N4")} {MotorRestaurante.unidadeMedidaTempo}.");
        }

        private static void MostrarEstatisticasDeFilas()
        {
            var historicoChegadaClientes = (Historico<ChegadaClientes>)ColetaDeDados.GetHistoricoBase("Histórico EventoGerenciado ChegadaClientes");

            var historicoFilaCx1 = (Historico<GrupoClientes>)ColetaDeDados.GetHistoricoBase("Histórico ConjuntoEntidade Fila Caixa 1");
            var historicoFilaCx2 = (Historico<GrupoClientes>)ColetaDeDados.GetHistoricoBase("Histórico ConjuntoEntidade Fila Caixa 2");

            var bancoBalcao = (Historico<GrupoClientes>)ColetaDeDados.GetHistoricoBase("Histórico ConjuntoEntidade Fila Balcão");
            var mesas2L = (Historico<GrupoClientes>)ColetaDeDados.GetHistoricoBase("Histórico ConjuntoEntidade Fila Mesas de 2 Lugares");
            var mesas4L = (Historico<GrupoClientes>)ColetaDeDados.GetHistoricoBase("Histórico ConjuntoEntidade Fila Mesas de 4 Lugares");

            var filaPedidosCozinha = (Historico<GrupoClientes>)ColetaDeDados.GetHistoricoBase("Histórico ConjuntoEntidade Fila Pedidos Cozinha");
            var filaPedidosEntrega = (Historico<GrupoClientes>)ColetaDeDados.GetHistoricoBase("Histórico ConjuntoEntidade Fila de Pedidos para Entrega");

            if (historicoChegadaClientes != null)
                Console.WriteLine($"Chegaram {historicoChegadaClientes.lista.Count} clientes.");

            ImprimirEstatisticaFila(historicoFilaCx1, "clientes", "fila 1");
            ImprimirEstatisticaFila(historicoFilaCx2, "clientes", "fila 2");

            ImprimirEstatisticaFila(bancoBalcao, "clientes", "fila do balcão");
            ImprimirEstatisticaFila(mesas2L, "clientes", "fila de mesas 2 lugares");
            ImprimirEstatisticaFila(mesas4L, "clientes", "fila de mesas 4 lugares");

            ImprimirEstatisticaFila(filaPedidosCozinha, "pedidos", "fila de pedidos para cozinha");
            ImprimirEstatisticaFila(filaPedidosEntrega, "pedidos", "fila de pedidos para entrega");

        }

        private static void ImprimirEstatisticaFila(Historico<GrupoClientes> hisGrupoClientes, string oQuePassaPelafila, string nomeDaFila)
        {
            ImprimirPassouPelaFila(hisGrupoClientes, oQuePassaPelafila, nomeDaFila);
            ImprimirTempoNaFila(hisGrupoClientes, oQuePassaPelafila, nomeDaFila);
            Console.WriteLine("");
        }

        private static void ImprimirTempoNaFila(Historico<GrupoClientes> hisGrupoClientes, string oQuePassaPelafila, string nomeDaFila)
        {
            var texto = $"Tempo médio dos {oQuePassaPelafila} na {nomeDaFila}";

            if (hisGrupoClientes != null)
                Console.WriteLine($"{texto} é de {hisGrupoClientes.tempoMedioDeVida().ToString("N4")}  {MotorRestaurante.unidadeMedidaTempo}.");
        }

        private static void ImprimirPassouPelaFila(Historico<GrupoClientes> hisGrupoClientes, string oQuePassaPelafila, string nomeDaFila)
        {
            var texto = $"{oQuePassaPelafila} passaram pela";

            if (hisGrupoClientes != null)
                Console.WriteLine($"{hisGrupoClientes.lista.Count} {texto} {nomeDaFila}.");
        }


    }
}
