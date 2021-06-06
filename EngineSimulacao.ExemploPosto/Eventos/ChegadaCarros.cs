using EngineSimulacao.Api;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class ChegadaCarros : Evento
    {
        public ChegadaCarros() {
            Gerenciador<ChegadaCarros>.nascimento(this);
        }
        protected override void Estrategia() {
            var novoCarro = new Carro();
            MotorPosto.filaAtendimento.Adicionar(novoCarro);
            var evtIniciar = new IniciarServico();
            Agendador.AgendarAgora(evtIniciar);

            if(Agendador.Tempo < MotorPosto.TEMPO_MAXIMO_CHEGADA_CARROS){
                var evtChegada = new ChegadaCarros();
                Agendador.AgendarEm(evtChegada, MotorPosto.TEMPO_ENTRE_CARROS);
            }
        }

        protected override void Destruir()
        {
            Gerenciador<ChegadaCarros>.morte(this);
        }
    }
}
