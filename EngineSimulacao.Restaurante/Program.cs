using EngineSimulacao.Api;
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

        private static void estatisticas(List<HistoricoBase> listaHistorico){
            Historico<ChegadaClientes> HistoricoChegadaClientes;    
            HistoricoChegadaClientes = (Historico<ChegadaClientes>) GetHistoricoBase(listaHistorico, "Histórico EventoGerenciado ChegadaClientes");
            
            Historico<GrupoClientes>  HistoricoFilaCx1;    
            HistoricoFilaCx1 =  (Historico<GrupoClientes>) GetHistoricoBase(listaHistorico, "Histórico ConjuntoEntidade Fila Caixa 1");
            
            Historico<GrupoClientes>  HistoricoFilaCx2;    
            HistoricoFilaCx2 =  (Historico<GrupoClientes>) GetHistoricoBase(listaHistorico, "Histórico ConjuntoEntidade Fila Caixa 2");
            
            if(HistoricoChegadaClientes!=null)
                Console.WriteLine("Chegaram "+HistoricoChegadaClientes.lista.Count+" clientes.");

            if(HistoricoFilaCx1!=null)
                Console.WriteLine(HistoricoFilaCx1.lista.Count+" clientes passaram pela fila 1.");

            if(HistoricoFilaCx2!=null)
                Console.WriteLine(HistoricoFilaCx2.lista.Count+" clientes passaram pela fila 2.");
            
        }


        private static HistoricoBase GetHistoricoBase(List<HistoricoBase> listaHistorico, string nome){
            foreach(var hisBase in listaHistorico){
                if(hisBase.nome.Equals(nome)){
                    return hisBase;
                }
            }
            return null;
        } 

    }
}
