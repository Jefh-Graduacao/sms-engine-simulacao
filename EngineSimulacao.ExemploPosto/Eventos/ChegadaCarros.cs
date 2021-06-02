using System.Collections.Generic;
using EngineSimulacao.Api;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class ParametrosChegadaCarros {} 
    public sealed class ChegadaCarros : IEvento
    {
        private MotorPostoGasolina motorPostoGasolina;
        public ChegadaCarros(MotorPostoGasolina Motor) {
            this.motorPostoGasolina = Motor;
         }

        public void Executar() {
            var idCarro = this.motorPostoGasolina.Agendador.CriarEntidade("Carro");

            var funcionarios = this.motorPostoGasolina.Agendador.ObterRecurso("funcionarios");

            if (funcionarios.VerificarDisponibilidade(1))
            {
                var evt = new IniciarServico(this.motorPostoGasolina, idCarro);
                this.motorPostoGasolina.Agendador.AgendarAgora(evt);
            }
            else
            {
                this.motorPostoGasolina.memoria.filaAtendimento.Enqueue(idCarro);
            }

            if (this.motorPostoGasolina.Agendador.Tempo < 100)
            {
                var evt = new ChegadaCarros(this.motorPostoGasolina);
                this.motorPostoGasolina.Agendador.AgendarEm(evt, 2);
            }
        }
    }
}
