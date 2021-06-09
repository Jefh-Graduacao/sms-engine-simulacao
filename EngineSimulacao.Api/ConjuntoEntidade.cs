using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    public enum Mode
    {
        Nenhum,
        Fifo,
        Lifo,
        Prioridade
    }

    public class ConjuntoEntidade<TEntidade> where TEntidade : EntidadeGerenciada
    {
        private readonly Historico<TEntidade> _historico;
        private List<TEntidade> _entidadesAtuais = new();

        public string Nome { private set; get; }
        public Mode Modo { get; set; } = Mode.Fifo;
        public int TamanhoMaximo { get; set; } = int.MaxValue;
        public int TamanhoAtual => _entidadesAtuais.Count;

        public ConjuntoEntidade(string nome)
        {
            _historico = new Historico<TEntidade>("ConjuntoEntidade " + nome);
        }

        public bool Adicionar(TEntidade entidade)
        {
            if (TamanhoAtual == TamanhoMaximo) return false;

            var tempo = Agendador.Tempo;
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
            return false;
        }

        public TEntidade Remover()
        {
            TEntidade entidade = null;
            switch (Modo)
            {
                case Mode.Fifo:
                    entidade = FIFORemover();
                    break;
                case Mode.Lifo:
                    entidade = LIFORemover();
                    break;
            }
            _historico.morte(entidade);
            return entidade;
        }

        private void LIFOAdicionar(TEntidade entidade)
        {
            Stack<TEntidade> pilha = new Stack<TEntidade>(_entidadesAtuais);
            pilha.Push(entidade);
            _entidadesAtuais = new List<TEntidade>(pilha);
        }

        private void FIFOAdicionar(TEntidade entidade)
        {
            Queue<TEntidade> fila = new Queue<TEntidade>(_entidadesAtuais);
            fila.Enqueue(entidade);
            _entidadesAtuais = new List<TEntidade>(fila);
        }

        private TEntidade LIFORemover()
        {
            Stack<TEntidade> pilha = new Stack<TEntidade>(_entidadesAtuais);
            TEntidade entidade = pilha.Pop();
            _entidadesAtuais = new List<TEntidade>(pilha);
            return entidade;
        }

        private TEntidade FIFORemover()
        {
            Queue<TEntidade> fila = new Queue<TEntidade>(_entidadesAtuais);
            TEntidade entidade = fila.Dequeue();
            _entidadesAtuais = new List<TEntidade>(fila);
            return entidade;
        }
    }
}