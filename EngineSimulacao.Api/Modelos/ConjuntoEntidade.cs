using System.Collections.Generic;
using System.Linq;

namespace EngineSimulacao.Api
{
    public enum Mode
    {
        Nenhum,
        Fifo,
        Lifo,
        Prioridade
    }

    public sealed record HistoricoConjuntoEntidade(double Tempo, int Quantidade);

    public sealed class ConjuntoEntidade<TEntidade> where TEntidade : EntidadeGerenciada
    {
        private readonly Historico<TEntidade> _historico;
        private IEnumerable<TEntidade> _colecaoInterna;

        public string Nome { get; }
        public Mode Modo { get; set; }
        public int TamanhoMaximo { get; set; } = int.MaxValue;
        public int TamanhoAtual => _colecaoInterna.Count();
        public Historico<TEntidade> Historico => _historico;
        public List<HistoricoConjuntoEntidade> HistoricoQuantidades { get; } = new();

        public ConjuntoEntidade(string nome, Mode modo = Mode.Fifo)
        {
            Nome = nome;
            Modo = modo;
            _colecaoInterna = new List<TEntidade>();
            _historico = new Historico<TEntidade>("ConjuntoEntidade " + Nome);
        }

        public bool Adicionar(TEntidade entidade)
        {
            if (TamanhoAtual == TamanhoMaximo) return false;
            
            _historico.nascimento(entidade);

            switch (Modo)
            {
                case Mode.Fifo:
                    FIFOAdicionar(entidade);
                    break;
                case Mode.Lifo:
                    LIFOAdicionar(entidade);
                    break;
            }

            HistoricoQuantidades.Add(new HistoricoConjuntoEntidade(Agendador.Tempo, TamanhoAtual));
            return false;
        }

        public TEntidade Remover()
        {
            var entidade = Modo switch
            {
                Mode.Fifo => FIFORemover(),
                Mode.Lifo => LIFORemover(),
                _ => null
            };
            _historico.morte(entidade);
            
            HistoricoQuantidades.Add(new HistoricoConjuntoEntidade(Agendador.Tempo, TamanhoAtual));
            return entidade;
        }

        private void LIFOAdicionar(TEntidade entidade)
        {
            Stack<TEntidade> pilha = new Stack<TEntidade>(_colecaoInterna);
            pilha.Push(entidade);
            _colecaoInterna = new List<TEntidade>(pilha);
        }

        private void FIFOAdicionar(TEntidade entidade)
        {
            Queue<TEntidade> fila = new Queue<TEntidade>(_colecaoInterna);
            fila.Enqueue(entidade);
            _colecaoInterna = new List<TEntidade>(fila);
        }

        private TEntidade LIFORemover()
        {
            Stack<TEntidade> pilha = new Stack<TEntidade>(_colecaoInterna);
            TEntidade entidade = pilha.Pop();
            _colecaoInterna = new List<TEntidade>(pilha);
            return entidade;
        }

        private TEntidade FIFORemover()
        {
            Queue<TEntidade> fila = new Queue<TEntidade>(_colecaoInterna);
            TEntidade entidade = fila.Dequeue();
            _colecaoInterna = new List<TEntidade>(fila);
            return entidade;
        }
    }
}