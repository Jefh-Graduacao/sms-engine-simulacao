using System;

namespace EngineSimulacao.Api
{
    public abstract class Evento<EnumConjuntos> where EnumConjuntos:struct, Enum
    {
        protected readonly MotorExecucao<EnumConjuntos> motor;
        public Evento(MotorExecucao<EnumConjuntos> motor){
            this.motor = motor;
        }
        public abstract void Executar();
    }
}