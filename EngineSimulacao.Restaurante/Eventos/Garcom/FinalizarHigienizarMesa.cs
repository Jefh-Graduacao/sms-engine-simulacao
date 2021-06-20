using EngineSimulacao.Api;

namespace EngineSimulacao.Restaurante.Eventos.Clientes
{
    public sealed class FinalizarHigienizarMesa : EventoGerenciado
    {
        protected override void Estrategia()
        {
           MotorRestaurante.garcom.MesaHigienizada.ProduzirMarcas(1);
        }
    }
}