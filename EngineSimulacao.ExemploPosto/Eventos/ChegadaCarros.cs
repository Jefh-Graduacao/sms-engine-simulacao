using EngineSimulacao.Api;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class ChegadaCarros : Evento<ConjuntosPosto>
    {
        public ChegadaCarros(MotorExecucao<ConjuntosPosto> Motor): base(Motor) {}

        public override void Executar() {
            ConjuntoEntidade<ConjuntosPosto> filaAtendimento = this.motor.PegarConjunto(ConjuntosPosto.filaAtendimento);

            var novoCarro = this.motor.Agendador.CriarEntidade();
            filaAtendimento.Adicionar(novoCarro);
            var evtIniciar = this.motor.criarEvento<IniciarServico>();
            this.motor.Agendador.AgendarAgora(evtIniciar);
          
            if (this.motor.Agendador.Tempo < CONFIG.TEMPO_MAXIMO_CHEGADA_CARROS)
            {
                var evtChegada = this.motor.criarEvento<ChegadaCarros>();
                this.motor.Agendador.AgendarEm(evtChegada, CONFIG.TEMPO_ENTRE_CARROS);
            }
        }
    }
}
