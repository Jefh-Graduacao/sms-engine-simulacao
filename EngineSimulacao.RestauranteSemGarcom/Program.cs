using EngineSimulacao.Api;
using EngineSimulacao.RestauranteSemGarcom.Entidades;
using EngineSimulacao.RestauranteSemGarcom.Eventos.Clientes;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EngineSimulacao.RestauranteSemGarcom
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            MotorRestaurante.Inicializar();

            var evento = new ChegadaClientes();
            Agendador.AgendarAgora(evento);

            Console.Clear();
            Console.WriteLine("################################################################");
            Console.WriteLine("\nBem vindo ao nosso trabalho!");
            ImprimirMenu();

            string line;
            string mensagem;
            double tempo;
            while ((line = Console.ReadLine()) != "8")
            {
                switch (line)
                {
                    case "1":
                        //Simular Todo Sistema
                        Agendador.Simular();
                        break;

                    case "2":
                        //Simular Até determinado tempo
                        mensagem = $"Digite o tempo ({MotorRestaurante.unidadeMedidaTempo}) em que a simulação irá parar:";
                        tempo = obterTempoInformadoPeloUsuario(mensagem);
                        Agendador.SimularAtéDeterminadoTempo(tempo);
                        break;

                    case "3":
                        //Simular Por determinado tempo
                        mensagem = $"Digite por quanto tempo ({MotorRestaurante.unidadeMedidaTempo}) a simulação será executada3:";
                        tempo = obterTempoInformadoPeloUsuario(mensagem);
                        Agendador.SimularPorDeterminadoTempo(tempo);
                        break;

                    case "4":
                        //Simular Passo a Passo (Um passo por vez)
                        Agendador.SimularUmaExecucao();
                        break;

                    case "5":
                        MostrarEstatisticasDeFilas();
                        MostrarEstatisticasDosRecursos();
                        break;
                    case "6":
                        foreach (var historico in ColetaDeDados.ListaDeHistoricos)
                        {
                            Console.WriteLine("\n\n------------");
                            Console.WriteLine(historico.Nome + " maior tempo de vida " + historico.maiorTempoDeVida());
                            Console.WriteLine(historico.Nome + " menor tempo de vida " + historico.menorTempoDeVida());
                            Console.WriteLine(historico.Nome + " tempo médio de vida " + historico.tempoMedioDeVida());
                            Console.WriteLine("------------\n\n");
                        }
                        break;
                    case "7":
                        //limpar console 
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Valor inválido, favor tente novamente.");
                        break;
                }
                if (line != "8")
                    ImprimirMenu();
            }
        }

        private static void ImprimirMenu()
        {
            Console.WriteLine("\n################################################################");
            Console.WriteLine($"\nTempo atual do Sistema: {Agendador.Tempo.ToString("N4")} {MotorRestaurante.unidadeMedidaTempo}.");
            Console.WriteLine($"Quantidade de Eventos Futuros: {Agendador.TamanhoListaEventosFuturos}");
            Console.WriteLine("\nDigite:\n");
            Console.WriteLine("1 -> Simular todo o Sistema.");
            Console.WriteLine("2 -> Simular até um tempo determinado.");
            Console.WriteLine("3 -> Simular por um tempo determinado.");
            Console.WriteLine("4 -> Simular um passo.");
            Console.WriteLine("5 -> Mostrar estatisticas Atuais.");
            Console.WriteLine("6 -> Mostrar historico completo.");
            Console.WriteLine("7 -> Limpar Terminal.");
            Console.WriteLine("8 -> Sair.");
        }

        private static double obterTempoInformadoPeloUsuario(string mensagem)
        {
            Console.WriteLine(mensagem);
            string line;

            while (true)
            {
                line = Console.ReadLine();
                try
                {
                    double valor = Double.Parse(line);
                    return valor;
                }
                catch (System.Exception)
                {
                    Console.WriteLine("Valor Invalido");
                    Thread.Sleep(500);
                    Console.WriteLine(mensagem);
                }
            }
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

            // usado para validar modelo com sistema (Tamanho da Fila x Tempo do Modelo)
            var coz = MotorRestaurante.FilaPedidosCozinha.HistoricoQuantidades;
            var cx1 = MotorRestaurante.FilaCaixa1.HistoricoQuantidades;
            var cx2 = MotorRestaurante.FilaCaixa2.HistoricoQuantidades;
            var l1 = MotorRestaurante.FilaBalcao.HistoricoQuantidades;
            var l2 = MotorRestaurante.FilaMesa2Lugares.HistoricoQuantidades;
            var l4 = MotorRestaurante.FilaMesa4Lugares.HistoricoQuantidades;

            if (historicoChegadaClientes != null)
                Console.WriteLine($"\nChegaram {historicoChegadaClientes.lista.Count} clientes.");

            ImprimirEstatisticaFila(MotorRestaurante.FilaCaixa1, "clientes");
            ImprimirEstatisticaFila(MotorRestaurante.FilaCaixa2, "clientes");

            ImprimirEstatisticaFila(MotorRestaurante.FilaBalcao, "clientes");
            ImprimirEstatisticaFila(MotorRestaurante.FilaMesa2Lugares, "clientes");
            ImprimirEstatisticaFila(MotorRestaurante.FilaMesa4Lugares, "clientes");

            ImprimirEstatisticaFila(MotorRestaurante.FilaPedidosCozinha, "pedidos");

        }

        private static void ImprimirEstatisticaFila(ConjuntoEntidade<GrupoClientes> fila, string oQuePassaPelafila)
        {
            ImprimirPassouPelaFila(fila, oQuePassaPelafila);
            ImprimirTempoNaFila(fila, oQuePassaPelafila);
            Console.WriteLine("");
        }

        private static void ImprimirPassouPelaFila(ConjuntoEntidade<GrupoClientes> fila, string oQuePassaPelafila)
        {
            var texto = $"{oQuePassaPelafila} passaram pela";

            if (fila.Historico != null)
                Console.WriteLine($"{fila.Historico.lista.Count} {texto} {fila.Nome.ToLower()}.");
        }

        private static void ImprimirTempoNaFila(ConjuntoEntidade<GrupoClientes> fila, string oQuePassaPelafila)
        {
            var texto = $"Tempo médio dos {oQuePassaPelafila} na {fila.Nome.ToLower()}";


            if (fila.Historico != null)
            {
                var desvio = $"Désvio médio: +/- {fila.Historico.desvioPadraoDeVida().ToString("N4")} {MotorRestaurante.unidadeMedidaTempo}.";
                Console.WriteLine($"{texto} é de {fila.Historico.tempoMedioDeVida().ToString("N4")}  {MotorRestaurante.unidadeMedidaTempo}. {desvio}");
            }

        }

    }
}
