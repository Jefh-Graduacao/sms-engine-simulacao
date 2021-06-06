using System;
using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    public abstract class HistoricoBase {
        public string nome { get; protected set; }
        public abstract int menorTempoDeVida();
        public abstract double tempoMedioDeVida();
        public abstract int maiorTempoDeVida();
    }
    public class Historico<T>:HistoricoBase where T:notnull, ITemID
    {
        public Historico(){
            this.init(this.gerarNomeHistorico());
        }
        public Historico(string nome) {
            this.init(nome);
        }
        public List<InfoInstancia<T>> lista { get; private set; } = new();

        public void nascimento(T instancia)
        {
            var info = new InfoInstancia<T>(instancia);
            info.viver();
            lista.Add(info);
        }
        public void morte(T instanciaMorta){
            var infoInstanciaDestruida = lista.Find(info=>info.Instancia.Id == instanciaMorta.Id);
            infoInstanciaDestruida.morrer();
        }
        public List<InfoInstancia<T>> listarVivos()
        {
            return this.lista.FindAll(Info=>Info.Vivo);
        }
        public List<InfoInstancia<T>> listarMortos()
        {
            return this.lista.FindAll(Info=>false == Info.Vivo);
        }

        public override int menorTempoDeVida(){
            if(this.lista.Count == 0) return 0;
            int menor = this.lista[0].TempoDeVida;
            foreach(var Info in this.lista){
                if(Info.TempoDeVida < menor) menor = Info.TempoDeVida;
            }
            return menor;
        }
        public override double tempoMedioDeVida() {
            if(this.lista.Count == 0) return 0;
            double soma = 0;
            foreach(var Info in this.lista){
                soma += Info.TempoDeVida;
            }
            return soma / this.lista.Count;
        }
        public override int maiorTempoDeVida(){
            if(this.lista.Count == 0) return 0;
            int maior = this.lista[0].TempoDeVida;
            foreach(var Info in this.lista){
                if(Info.TempoDeVida > maior) maior = Info.TempoDeVida;
            }
            return maior;
        }
        private void init(string nome)
        {
            this.nome = "Histórico " + nome;
            ColetaDeDados.NovoHistorico<T>(this);
        }
        private string pegarTiposGenericos(Type tipo){
            var argumentosGenericos = tipo.GetGenericArguments();
            if(argumentosGenericos.Length == 0)
                return "";
            string saida = "<";
            for(int i = 0; i < argumentosGenericos.Length; i++){
                var tipoArgumento = (Type)argumentosGenericos.GetValue(i);
                saida = saida + " " + tipoArgumento.Name;
            }
            saida = saida + " >";
            return saida;
        }

        private string gerarNomeHistorico(){
            var tipo = typeof(T);
            var tipoBase = tipo.BaseType;
            
            var nomeClasse = tipo.Name;
            var nomeClasseBase = tipoBase.Name;

            string genericos = this.pegarTiposGenericos(tipo);

            if(nomeClasseBase != "Object")
                return nomeClasseBase + " " + nomeClasse + " " +  genericos;
            else
                return nomeClasse + " " + genericos;
        }
    }
}