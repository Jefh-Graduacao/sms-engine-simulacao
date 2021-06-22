using System.Collections.Generic;
using EngineSimulacao.Api;
using EngineSimulacao.RestauranteSemGarcom.Entidades;
using EngineSimulacao.RestauranteSemGarcom.Eventos.Clientes;
using EngineSimulacao.RestauranteSemGarcom.Recursos;

namespace EngineSimulacao.RestauranteSemGarcom.Eventos.Cozinha
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

            Agendador.AgendarAgora(new EnviarPedidoParaCozinha());
            
            _clientes.Pedido.ProntroParaComer = true;
            if (null != _clientes.LugarOcupado)
            {
                Agendador.AgendarAgora(new ComecarAComer(_clientes));
            }
        }
    }
}
