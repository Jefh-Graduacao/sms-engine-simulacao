using EngineSimulacao.Api;

namespace EngineSimulacao.Restaurante.Eventos.Clientes
{
    public sealed class FinalizarEntregaPedido : EventoGerenciado
    {
        protected override void Estrategia()
        {
           MotorRestaurante.garcom.PedidoNaMesa.ProduzirMarcas(1);
        }
    }
}