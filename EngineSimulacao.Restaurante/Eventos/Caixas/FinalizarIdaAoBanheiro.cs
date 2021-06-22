using EngineSimulacao.Api;

namespace EngineSimulacao.Restaurante.Eventos.Caixas
{
    public sealed class FinalizarIdaAoBanheiro : EventoGerenciado
    {
        protected override void Estrategia()
        {
            MotorRestaurante.Garcom.CaixaRetorno.ProduzirMarcas(1);

            var proximaIdaAoBanheiro = MotorRestaurante.TempoIdaAoBanheiroCaixa;
            if (Agendador.Tempo + proximaIdaAoBanheiro <= MotorRestaurante.TempoMaximoIdasAoBanheiro)
                Agendador.AgendarEm(new IrAoBanheiro(), proximaIdaAoBanheiro);
        }
    }
}
