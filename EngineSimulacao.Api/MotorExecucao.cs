using System;


namespace EngineSimulacao.Api
{   
    public abstract class MotorExecucao<Memoria> where Memoria:new()
    {
        public Memoria memoria = new Memoria();
        public Agendador Agendador = new Agendador();
    }
}
