using EngineSimulacao.Api;

namespace EngineSimulacao.Restaurante.Eventos.Caixas
{
    public sealed class IrAoBanheiro : EventoGerenciado
    {
        protected override void Estrategia()
        {
            MotorRestaurante.Garcom.SubstituirCaixa.ProduzirMarcas(1);
        }
    }
}
