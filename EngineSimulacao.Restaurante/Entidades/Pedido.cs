using EngineSimulacao.Api;
using System;

namespace EngineSimulacao.Restaurante.Entidades
{
    public sealed class Pedido : EntidadeGerenciada {
        private GrupoClientes _clientes { get; set; }
        public bool ProntroParaComer { get; set; }
        public Pedido(GrupoClientes clientes)
        {
            _clientes = clientes;
        }
    }
}
