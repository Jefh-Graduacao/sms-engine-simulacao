using System;

namespace EngineSimulacao.Api
{
    // TODO: Rever execeções
    public sealed class Recurso
    {
        public Recurso(int id, string nome, int quantidadeTotal)
        {
            Id = id;
            Nome = nome;
            QuantidadeTotal = quantidadeTotal;
            QuantidadeDisponivel = quantidadeTotal;
        }

        public int Id { get; }
        public string Nome { get; }
        public int QuantidadeTotal { get; }
        public int QuantidadeDisponivel { get; private set; }

        public bool TentarAlocar(int quantidade)
        {
            if (quantidade <= 0 || quantidade > QuantidadeDisponivel)
                throw new InvalidOperationException("Não há recursos disponíveis");

            QuantidadeDisponivel -= quantidade;
            return true;
        }

        public void Liberar(int quantidade)
        {
            var novaQtd = QuantidadeDisponivel + quantidade;

            if (novaQtd > QuantidadeTotal)
                throw new Exception("");

            QuantidadeDisponivel += quantidade;
        }
        public bool VerificarDisponibilidade(int quantidade) => quantidade <= QuantidadeDisponivel;
    }
}
