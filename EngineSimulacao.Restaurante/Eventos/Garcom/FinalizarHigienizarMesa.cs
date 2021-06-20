using EngineSimulacao.Api;

namespace EngineSimulacao.Restaurante.Eventos.Garcom
{
    public sealed class FinalizarHigienizarMesa : EventoGerenciado
    {
        protected override void Estrategia()
        {
            MotorRestaurante.garcom.MesaHigienizada.ProduzirMarcas(1);
        }
    }
}