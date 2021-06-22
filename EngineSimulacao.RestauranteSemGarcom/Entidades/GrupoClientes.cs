using System;
using System.Collections.Generic;
using EngineSimulacao.Api;

namespace EngineSimulacao.RestauranteSemGarcom.Entidades
{
    public sealed class GrupoClientes : EntidadeGerenciada
    {
        public int Quantidade { get; }
        public Pedido Pedido { get; set; }
        public IEnumerable<IAlocacaoGerenciada<RecursoGerenciado>> LugarOcupado;

        private readonly Random _random = new();

        public GrupoClientes()
        {
            Quantidade = _random.Next(1, 5); //[1,5) = [1,4]
        }
    }
}
