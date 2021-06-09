using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Eventos.Clientes;
using System;

namespace EngineSimulacao.Restaurante.Eventos
{
    public sealed class AtenderCaixa1 : EventoGerenciado
    {
        protected override void Estrategia()
        {
            var clientes = MotorRestaurante.FilaCaixa1.Remover();

            // faz pedido e pagamento

            Agendador.AgendarAgora(new EnviarPedidoParaCozinha()); // normal(14, 5)

            switch (clientes.Quantidade)
            {
                case 1:
                    Agendador.AgendarAgora(new IrParaBalcao(clientes));
                    break;
                case 2:
                    //Agendador.AgendarAgora(new IrParaMesas2Lugares());
                    break;
                default:
                    //Agendador.AgendarAgora(new IrParaMesas4Lugares());
                    break;
            }
        }
    }
}
