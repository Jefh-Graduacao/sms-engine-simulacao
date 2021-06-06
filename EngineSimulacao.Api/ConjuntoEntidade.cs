using System;
using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    public enum Mode
    {
        FIFO,
        LIFO,
        PRIORITY,
        NONE
    }
    public class ConjuntoEntidade<E> where E:Entidade
    {
        public string Nome { private set; get; }
        public Mode Modo { get; set; } = Mode.FIFO;
        public int TamanhoMaximo { get; set; } = Int32.MaxValue;
        public int TamanhoAtual => this._entidadesAtuais.Count;
        private Historico<E> _historico;
        private List<E> _entidadesAtuais = new();

        public ConjuntoEntidade(string nome)
        {
            this._historico = new Historico<E>("ConjuntoEntidade " + nome);
        }
        public bool Adicionar(E entidade){
            if(TamanhoAtual == TamanhoMaximo) return false;

            var tempo = Agendador.Tempo;
            _historico.nascimento(entidade);
            
            switch(this.Modo){
                case Mode.FIFO:
                    this.FIFOAdicionar(entidade);
                    break;
                case Mode.LIFO:
                    this.LIFOAdicionar(entidade);
                    break;
            }
            return false;
        }
        public E Remover() {
            E entidade = null;
            switch(this.Modo){
                case Mode.FIFO:
                    entidade = this.FIFORemover();
                    break;
                case Mode.LIFO:
                    entidade = this.LIFORemover();
                    break;
            }
            _historico.morte(entidade);
            return entidade;
        }
        private void LIFOAdicionar(E entidade){
            Stack<E> pilha = new Stack<E>(this._entidadesAtuais);
            pilha.Push(entidade);
            this._entidadesAtuais = new List<E>(pilha);
        } 

        private void FIFOAdicionar(E entidade){
            Queue<E> fila = new Queue<E>(this._entidadesAtuais);
            fila.Enqueue(entidade);
            this._entidadesAtuais = new List<E>(fila);
        }
        private E LIFORemover(){
            Stack<E> pilha = new Stack<E>(this._entidadesAtuais);
            E entidade = pilha.Pop();
            this._entidadesAtuais = new List<E>(pilha);
            return entidade;
        } 

        private E FIFORemover(){
            Queue<E> fila = new Queue<E>(this._entidadesAtuais);
            E entidade = fila.Dequeue();
            this._entidadesAtuais = new List<E>(fila);
            return entidade;
        }
    }
}