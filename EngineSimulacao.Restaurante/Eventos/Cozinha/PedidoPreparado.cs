using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Entidades;
using EngineSimulacao.Restaurante.Recursos;
using System.Collections.Generic;

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

            Agendador.AgendarAgora(new EnviarPedidoParaCozinha());
            
            _clientes.Pedido.ProntroParaComer = true;
            if (null != _clientes.LugarOcupado)
            {
                MotorRestaurante.FilaEntrega.Adicionar(_clientes);

                MotorRestaurante.garcom.PedidoPronto.ProduzirMarcas(1);
            }
        }
    }
}
