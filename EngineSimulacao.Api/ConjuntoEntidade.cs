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
    public class ConjuntoEntidade<EnumConjuntos> where EnumConjuntos:struct, Enum
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Mode Modo { get; set; } = Mode.FIFO;
        public int TamanhoMaximo { get; set; } = Int32.MaxValue;
        public int TamanhoAtual => this.entidadesAtuais.Count;
        private readonly MotorExecucao<EnumConjuntos> motorExecucao;
        private List<InfoEntidade> historicoEntidades = new();
        private List<Entidade> entidadesAtuais = new();

        public ConjuntoEntidade(MotorExecucao<EnumConjuntos> motorExecucao){
            this.motorExecucao = motorExecucao;
        }

        public bool Adicionar(Entidade entidade){
            if(TamanhoAtual == TamanhoMaximo) return false;

            var tempo = motorExecucao.Agendador.Tempo;
            var infoEntidade = new InfoEntidade(entidade, tempo);
            this.historicoEntidades.Add(infoEntidade);
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
        public Entidade Remover() {
            Entidade entidade;
            switch(this.Modo){
                case Mode.FIFO:
                    entidade = this.FIFORemover();
                    break;
                case Mode.LIFO:
                    entidade = this.LIFORemover();
                    break;
                default:
                    entidade = new Entidade(-1);
                    break;
            }
            InfoEntidade infoEntidadeRemovida = this.historicoEntidades.Find(info=>info.Entidade.Id == entidade.Id);
            infoEntidadeRemovida.TempoSaida = motorExecucao.Agendador.Tempo;
            infoEntidadeRemovida.NoConjunto = false;
            return entidade;
        }
        private void LIFOAdicionar(Entidade entidade){
            Stack<Entidade> pilha = new Stack<Entidade>(this.entidadesAtuais);
            pilha.Push(entidade);
            this.entidadesAtuais = new List<Entidade>(pilha);
        } 

        private void FIFOAdicionar(Entidade entidade){
            Queue<Entidade> fila = new Queue<Entidade>(this.entidadesAtuais);
            fila.Enqueue(entidade);
            this.entidadesAtuais = new List<Entidade>(fila);
        }
        private Entidade LIFORemover(){
            Stack<Entidade> pilha = new Stack<Entidade>(this.entidadesAtuais);
            Entidade entidade = pilha.Pop();
            this.entidadesAtuais = new List<Entidade>(pilha);
            return entidade;
        } 

        private Entidade FIFORemover(){
            Queue<Entidade> fila = new Queue<Entidade>(this.entidadesAtuais);
            Entidade entidade = fila.Dequeue();
            this.entidadesAtuais = new List<Entidade>(fila);
            return entidade;
        }
    }
}