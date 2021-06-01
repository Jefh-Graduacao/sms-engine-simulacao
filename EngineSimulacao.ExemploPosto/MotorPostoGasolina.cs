using System;
using EngineSimulacao.Api;
using EngineSimulacao.ExemploPosto.Eventos;
using System.Collections.Generic;
using System.Linq;

namespace EngineSimulacao.ExemploPosto
{
    public class MemoriaPostoGasolina {
        public Queue<int> filaAtendimento = new();
    }
    public sealed class MotorPostoGasolina : MotorExecucao<MemoriaPostoGasolina>
    {
        public MotorPostoGasolina() {
            this.Agendador = new Agendador<MemoriaPostoGasolina>();
        }
    }
}
