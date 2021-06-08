using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Recursos;
using System;
using System.Collections.Generic;

namespace EngineSimulacao.Restaurante.Eventos
{
    public sealed class PedidoPreparado : Evento
    {
        private readonly List<Alocacao<Cozinheiro>> _cozinheiros;

        public PedidoPreparado(List<Alocacao<Cozinheiro>> cozinheiros)
        {
            _cozinheiros = cozinheiros;
        }

        protected override void Estrategia()
        {
            GerenciadorDeRecursos<Cozinheiro>.Liberar(_cozinheiros);

            // Adiciona na fila de entregas 
        }

        protected override void Destruir()
        {
            throw new NotImplementedException();
        }
    }
}
