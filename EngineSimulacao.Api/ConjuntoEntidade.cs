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
    public class InfoEntidade
    {
        public bool NoConjunto;
        public int TempoEntrada;
        public int TempoSaida;
        public int Prioridade;
        public Entidade Entidade;

        public InfoEntidade(Entidade Entidade, int TempoEntrada){
            this.Entidade = Entidade;
            this.TempoEntrada = TempoEntrada;
            this.Prioridade = -1;
        }
        public InfoEntidade(Entidade Entidade, int TempoEntrada, int Prioridade){
            this.Entidade = Entidade;
            this.TempoEntrada = TempoEntrada;
            this.Prioridade = Prioridade;
        }
    }

    public class ConjuntoEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Mode Modo { get; set; }
        public int TamanhoMaximo { get; set; }

        private List<InfoEntidade> historicoEntidades;
        private List<Entidade> entidadesAtuais;

        public void Adicionar(Entidade entidade, int tempo){
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
        }
        public void Remover(int tempo) {
            Entidade entidade;
            switch(this.Modo){
                case Mode.FIFO:
                    entidade = this.FIFORemover();
                    break;
                case Mode.LIFO:
                    entidade = this.LIFORemover();
                    break;
                default:
                    entidade = new Entidade();
                    break;
            }
            InfoEntidade infoEntidadeRemovida = this.historicoEntidades.Find(info=>info.Entidade.Equals(entidade));
            infoEntidadeRemovida.TempoSaida = tempo;
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