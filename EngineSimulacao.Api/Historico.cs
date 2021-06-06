using System;
using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    public class Historico<T> where T:ITemID
    {
        public List<InfoInstancia<T>> lista { get; private set; } = new();

        public void nascimento(T instancia)
        {
            var info = new InfoInstancia<T>();
            info.Instancia = instancia;
            info.Vivo = true;
            info.TempoCriacao = Agendador.Tempo;
            info.Prioridade = Int32.MinValue;
            
            lista.Add(info);
        }
        public void morte(T instanciaMorta){
            var infoInstanciaDestruida = lista.Find(info=>info.Instancia.Id == instanciaMorta.Id);
            infoInstanciaDestruida.TempoDestruicao = Agendador.Tempo;
            infoInstanciaDestruida.Vivo = false;
        }
        public int menorTempoDeVida(){
            /* TODO */
            return 0;
        }
        public int tempoMedioDeVida() {
            /* TODO */
            return 0;
        }
        public int maiorTempoDeVida(){
            /* TODO */
            return 0;
        }
    }
}