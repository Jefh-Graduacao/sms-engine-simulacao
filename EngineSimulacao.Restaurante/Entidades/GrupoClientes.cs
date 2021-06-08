using EngineSimulacao.Api;
using System;

namespace EngineSimulacao.Restaurante.Entidades
{
    public sealed class GrupoClientes : Entidade
    {
        private readonly Random _random = new();

        public int Quantidade { get; }

        public GrupoClientes()
        {
            Quantidade = _random.Next(1, 4);
        }

        protected override void DestruirInstancia()
        {
            throw new NotImplementedException();
        }
    }
}
