using EngineSimulacao.Api;
using EngineSimulacao.ExemploPosto.Entidades;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class ChegadaCarros : Evento
    {
        public ChegadaCarros()
        {
            Gerenciador<ChegadaCarros>.nascimento(this);
        }

        protected override void Estrategia()
        {
            var novoCarro = new Carro();
            MotorPosto.FilaAtendimento.Adicionar(novoCarro);

            var evtIniciar = new IniciarServico();
            Agendador.AgendarAgora(evtIniciar);

            if (Agendador.Tempo >= MotorPosto.TempoMaximoChegadaCarros)
                return;

            var evtChegada = new ChegadaCarros();
            Agendador.AgendarEm(evtChegada, MotorPosto.TempoEntreCarros);
        }

        protected override void Destruir()
        {
            Gerenciador<ChegadaCarros>.morte(this);
        }
    }
}
