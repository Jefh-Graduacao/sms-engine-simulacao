using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Entidades;
using EngineSimulacao.Restaurante.Eventos.Clientes;
using EngineSimulacao.Restaurante.Recursos;
using System;
using System.Threading;

namespace EngineSimulacao.Restaurante
{
    public static class Programa
    {
        public static void Main(string[] args)
        {
            MotorRestaurante.Inicializar();

            var evento = new ChegadaClientes();
            Agendador.AgendarAgora(evento);

            
            _executarMenu(() => CallbackMotor(MotorRestaurante.Garcom));

        }

        private static void CallbackMotor(Garcom garcom)
        {
            garcom.RedePetri.ExecutarCiclo();
        }


        #region METODOS PARA MENU

        private static void _executarMenu(Action callback = null)
        {
            Console.Clear();
            Console.WriteLine("################################################################");
            Console.WriteLine("\nBem vindo ao nosso trabalho!");
            _imprimirMenu();

            string line;
            string mensagem;
            double tempo;
            while ((line = Console.ReadLine()) != "7")
            {
                switch (line)
                {
                    case "":
                        //Simular Passo a Passo (Um passo por vez)
                        Agendador.SimularUmaExecucao(callback);
                        break;

                    case "1":
                        //Simular Todo Sistema
                        Agendador.Simular(callback);
                        break;

                    case "2":
                        //Simular Até determinado tempo
                        mensagem = $"Digite o tempo ({MotorRestaurante.unidadeMedidaTempo}) em que a simulação irá parar:";
                        tempo = _obterTempoInformadoPeloUsuario(mensagem);
                        Agendador.SimularAtéDeterminadoTempo(tempo, callback);
                        break;

                    case "3":
                        //Simular Por determinado tempo
                        mensagem = $"Digite por quanto tempo ({MotorRestaurante.unidadeMedidaTempo}) a simulação será executada:";
                        tempo = _obterTempoInformadoPeloUsuario(mensagem);
                        Agendador.SimularPorDeterminadoTempo(tempo, callback);
                        break;

                    case "4":
                        _mostrarEstatisticasDeFilas();
                        _mostrarEstatisticasDosRecursos();
                        break;

                    case "5":
                        foreach (var historico in ColetaDeDados.ListaDeHistoricos)
                        {
                            Console.WriteLine("\n\n------------");
                            Console.WriteLine(historico.Nome + " maior tempo de vida " + historico.maiorTempoDeVida());
                            Console.WriteLine(historico.Nome + " menor tempo de vida " + historico.menorTempoDeVida());
                            Console.WriteLine(historico.Nome + " tempo médio de vida " + historico.tempoMedioDeVida());
                            Console.WriteLine("------------\n\n");
                        }
                        break;
                    case "6":
                        //limpar console 
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Valor inválido, favor tente novamente.");
                        break;
                }
                if (line != "7")
                    _imprimirMenu();
            }
        }
        
        private static void _imprimirMenu()
        {
            Console.WriteLine("\n################################################################");
            Console.WriteLine($"\nTempo atual do Sistema: {Agendador.Tempo.ToString("N4")} {MotorRestaurante.unidadeMedidaTempo}.");
            Console.WriteLine($"Quantidade de Eventos Futuros: {Agendador.TamanhoListaEventosFuturos}");
            Console.WriteLine("\nDigite:\n");
            Console.WriteLine("Vazio -> Simular um passo.");
            Console.WriteLine("1 -> Simular todo o Sistema.");
            Console.WriteLine("2 -> Simular até um tempo determinado.");
            Console.WriteLine("3 -> Simular por um tempo determinado.");
            Console.WriteLine("4 -> Mostrar estatisticas Atuais.");
            Console.WriteLine("5 -> Mostrar historico completo.");
            Console.WriteLine("6 -> Limpar Terminal.");
            Console.WriteLine("7 -> Sair.");
        }

        private static double _obterTempoInformadoPeloUsuario(string mensagem)
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

        #endregion

        #region METODOS ESTATISTICAS DO RECURSOS

        private static void _mostrarEstatisticasDosRecursos()
        {
            var alocacaoRecursoBalcao = ColetaDeDados.GetHistoricoBase("Histórico AlocacaoGerenciada`1< BancoBalcao >");
            var alocacaoRecursoMesas2L = ColetaDeDados.GetHistoricoBase("Histórico AlocacaoGerenciada`1< Mesa2Lugares >");
            var alocacaoRecursoMesas4L = ColetaDeDados.GetHistoricoBase("Histórico AlocacaoGerenciada`1< Mesa4Lugares >");
            var alocacaoRecursoAtendenteCx1 = ColetaDeDados.GetHistoricoBase("Histórico AlocacaoGerenciada`1< AtendenteCaixa1 >");
            var alocacaoRecursoAtendenteCx2 = ColetaDeDados.GetHistoricoBase("Histórico AlocacaoGerenciada`1< AtendenteCaixa2 >");
            var alocacaoRecursoCozinheiro = ColetaDeDados.GetHistoricoBase("Histórico AlocacaoGerenciada`1< Cozinheiro >");

            _imprimirEstatisticaRecursos(alocacaoRecursoBalcao, "Banco Balcao");
            _imprimirEstatisticaRecursos(alocacaoRecursoMesas2L, "Mesas 2 Lugares");
            _imprimirEstatisticaRecursos(alocacaoRecursoMesas4L, "Mesas 4 Lugares");
            _imprimirEstatisticaRecursos(alocacaoRecursoAtendenteCx1, "Atendente Caixa 1");
            _imprimirEstatisticaRecursos(alocacaoRecursoAtendenteCx2, "Atendente Caixa 2");
            _imprimirEstatisticaRecursos(alocacaoRecursoCozinheiro, "Cozinheiro");
        }

        private static void _imprimirEstatisticaRecursos(HistoricoBase x, string recursoNome)
        {
            if (x != null)
                Console.WriteLine($"O tempo médio que o recurso {recursoNome} fica alocado é de "
                                + $"{x.tempoMedioDeVida().ToString("N4")} {MotorRestaurante.unidadeMedidaTempo}.");
        }

        #endregion

        #region METODOS ESTATISTICAS DAS FILAS
        private static void _mostrarEstatisticasDeFilas()
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

            _imprimirEstatisticaFila(MotorRestaurante.FilaCaixa1, "clientes");
            _imprimirEstatisticaFila(MotorRestaurante.FilaCaixa2, "clientes");

            _imprimirEstatisticaFila(MotorRestaurante.FilaBalcao, "clientes");
            _imprimirEstatisticaFila(MotorRestaurante.FilaMesa2Lugares, "clientes");
            _imprimirEstatisticaFila(MotorRestaurante.FilaMesa4Lugares, "clientes");

            _imprimirEstatisticaFila(MotorRestaurante.FilaPedidosCozinha, "pedidos");
            _imprimirEstatisticaFila(MotorRestaurante.FilaEntrega, "pedidos");

        }

        private static void _imprimirEstatisticaFila(ConjuntoEntidade<GrupoClientes> fila, string oQuePassaPelafila)
        {
            _imprimirPassouPelaFila(fila, oQuePassaPelafila);
            _imprimirTempoNaFila(fila, oQuePassaPelafila);
            Console.WriteLine("");
        }

        private static void _imprimirPassouPelaFila(ConjuntoEntidade<GrupoClientes> fila, string oQuePassaPelafila)
        {
            var texto = $"{oQuePassaPelafila} passaram pela";

            if (fila.Historico != null){
                Console.WriteLine($"{fila.Historico.lista.Count} {texto} {fila.Nome.ToLower()}.");
                Console.WriteLine($"Tamanho atual da {fila.Nome.ToLower()}: {fila.TamanhoAtual}.");
            }
        }

        private static void _imprimirTempoNaFila(ConjuntoEntidade<GrupoClientes> fila, string oQuePassaPelafila)
        {
            var texto = $"Tempo médio dos {oQuePassaPelafila} na {fila.Nome.ToLower()}";


            if (fila.Historico != null)
            {
                var desvio = $"Désvio médio: +/- {fila.Historico.desvioPadraoDeVida().ToString("N4")} {MotorRestaurante.unidadeMedidaTempo}.";
                Console.WriteLine($"{texto} é de {fila.Historico.tempoMedioDeVida().ToString("N4")}  {MotorRestaurante.unidadeMedidaTempo}. {desvio}");
            }

        }

        #endregion       

  
    }
}
