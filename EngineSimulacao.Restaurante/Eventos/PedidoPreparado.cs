using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Recursos;
using System.Collections.Generic;
using EngineSimulacao.Restaurante.Entidades;
using EngineSimulacao.Restaurante.Eventos.Clientes;

namespace EngineSimulacao.Restaurante.Eventos
{
    public sealed class PedidoPreparado : EventoGerenciado
    {
        private readonly IEnumerable<IAlocacaoGerenciada<Cozinheiro>> _cozinheiros;
        private readonly GrupoClientes _clientes;

        public PedidoPreparado(IEnumerable<IAlocacaoGerenciada<Cozinheiro>> cozinheiros, GrupoClientes clientes)
        {
            _cozinheiros = cozinheiros;
            _clientes = clientes;
        }

        protected override void Estrategia()
        {
            GerenciadorDeRecursos<Cozinheiro>.Liberar(_cozinheiros);
            _clientes.Pedido.ProntroParaComer = true;
            if(null != _clientes.LugarOcupado){
                Agendador.AgendarAgora(new ComecarAComer(_clientes));
            }
        }
    }
}
