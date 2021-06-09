using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Recursos;
using System;
using System.Collections.Generic;
using EngineSimulacao.Restaurante.Entidades;

namespace EngineSimulacao.Restaurante.Eventos
{
    public sealed class PedidoPreparado : EventoGerenciado
    {
        private readonly List<AlocacaoGerenciada<Cozinheiro>> _cozinheiros;
        private readonly Pedido _pedido;

        public PedidoPreparado(List<AlocacaoGerenciada<Cozinheiro>> cozinheiros, Pedido pedido)
        {
            _cozinheiros = cozinheiros;
            _pedido = pedido;
        }

        protected override void Estrategia()
        {
            GerenciadorDeRecursos<Cozinheiro>.Liberar(_cozinheiros);

            // Adiciona na fila de entregas 
        }
    }
}
