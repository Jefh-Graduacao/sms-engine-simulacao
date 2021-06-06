using System;
using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    public class Historico<T> where T:notnull, ITemID
    {
        public string nome { get; private set; }
        public Historico(){
            var tipo = typeof(T);
            var tipoBase = tipo.BaseType;
            
            var nomeClasse = tipo.Name;
            var nomeClasseBase = tipoBase.Name;

            if(nomeClasseBase != "Object")
                this.init(nomeClasseBase + " " + nomeClasse);
            else
                this.init(typeof(T).Name);
        }
        public Historico(string nome) {
            this.init(nome);
        }

        private void init(string nome)
        {
            this.nome = "Histórico " + nome;
            ColetaDeDados.NovoHistorico<T>(this);
        }
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