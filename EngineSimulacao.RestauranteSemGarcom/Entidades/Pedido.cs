﻿using EngineSimulacao.Api;

namespace EngineSimulacao.RestauranteSemGarcom.Entidades
{
    public sealed class Pedido : EntidadeGerenciada
    {
        private GrupoClientes _clientes { get; set; }
        public bool ProntroParaComer { get; set; }

        public Pedido(GrupoClientes clientes)
        {
            _clientes = clientes;
        }
    }
}
