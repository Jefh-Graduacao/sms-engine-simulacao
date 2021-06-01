using System.Collections.Generic;
using EngineSimulacao.Api;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class ParametrosChegadaCarros {} 
    public sealed class ChegadaCarros : Evento<MemoriaPostoGasolina>
    {
        public ChegadaCarros(MotorExecucao<MemoriaPostoGasolina> Motor) : base(Motor) { }

        public override void Executar() {
            var idCarro = this.motor.Agendador.CriarEntidade("Carro");

            var funcionarios = this.motor.Agendador.ObterRecurso("funcionarios");

            if (funcionarios.VerificarDisponibilidade(1))
            {
                var evt = this.motor.CriarEvento<IniciarServico>(new ParametrosIniciarServico(idCarro));
                this.motor.Agendador.AgendarAgora(evt);
            }
            else
            {
                this.motor.memoria.filaAtendimento.Enqueue(idCarro);
            }

            if (this.motor.Agendador.Tempo < 100)
            {
                var evt = this.motor.CriarEvento<ChegadaCarros>();
                this.motor.Agendador.AgendarEm(evt, 2);
            }
        }
    }
}
