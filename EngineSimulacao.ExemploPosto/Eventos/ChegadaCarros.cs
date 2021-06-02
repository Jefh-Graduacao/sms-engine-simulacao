using System.Collections.Generic;
using EngineSimulacao.Api;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class ChegadaCarros : Evento<MemoriaPostoGasolina>
    {
        public ChegadaCarros(MotorExecucao<MemoriaPostoGasolina> Motor): base(Motor) { }

        public override void Executar() {
            var idCarro = this.motor.Agendador.CriarEntidade("Carro");

            var funcionarios = this.motor.Agendador.ObterRecurso("funcionarios");

            if (funcionarios.VerificarDisponibilidade(1))
            {
                var evtIniciar = this.motor.criarEvento<IniciarServico>();
                this.motor.Agendador.AgendarAgora(evtIniciar);
            }
            else
            {
                this.motor.memoria.filaAtendimento.Enqueue(idCarro);
            }

            if (this.motor.Agendador.Tempo < 100)
            {
                var evtChegada = this.motor.criarEvento<ChegadaCarros>();
                this.motor.Agendador.AgendarEm(evtChegada, 2);
            }
        }
    }
}
