using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Entidades;
using EngineSimulacao.Restaurante.Eventos.Clientes;

namespace EngineSimulacao.Restaurante.Eventos
{
    public sealed class FinalizarAtendimentoCaixa : EventoGerenciado
    {
        private int _caixa;
        private GrupoClientes _clientes;
        public FinalizarAtendimentoCaixa(int caixa, GrupoClientes clientes)
        {
            _caixa = caixa;
            _clientes = clientes;
        }
        public GrupoClientes RemoverDaFilaAdequada()
        {
            if (_caixa == 1)
                return MotorRestaurante.FilaCaixa1.Remover();
            else
                return MotorRestaurante.FilaCaixa2.Remover();
        }
        protected override void Estrategia()
        {
            Agendador.AgendarAgora(new EnviarPedidoParaCozinha());

            _clientes.Pedido = new Pedido(_clientes);

            MotorRestaurante.FilaPedidosCozinha.Adicionar(_clientes);

            switch (_clientes.Quantidade)
            {
                case 1:
                    MotorRestaurante.FilaBalcao.Adicionar(_clientes);
                    break;
                case 2:
                    MotorRestaurante.FilaMesa2Lugares.Adicionar(_clientes);
                    break;
                default:
                    MotorRestaurante.FilaMesa4Lugares.Adicionar(_clientes);
                    break;
            }
            Agendador.AgendarAgora(new IrParaMesa(_clientes.Quantidade));
            Agendador.AgendarAgora(new IniciarAtendimentoCaixa(_caixa));
        }
    }
}