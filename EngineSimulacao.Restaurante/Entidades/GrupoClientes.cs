using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Recursos;
using System;
using System.Collections.Generic;

namespace EngineSimulacao.Restaurante.Entidades
{
    public sealed class GrupoClientes : EntidadeGerenciada
    {
        private readonly Random _random = new();

        public int Quantidade { get; }
        public Pedido Pedido { get; set; }
        public IEnumerable<IAlocacaoGerenciada<RecursoGerenciado>> LugarOcupado;

        public GrupoClientes()
        {
            Quantidade = _random.Next(1, 4);
        }
    }
}
