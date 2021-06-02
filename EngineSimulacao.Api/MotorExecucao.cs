using System;


namespace EngineSimulacao.Api
{   
    public class MotorExecucao<Memoria> where Memoria:new()
    {
        public Memoria memoria = new Memoria();
        public Agendador<Memoria> Agendador = new Agendador<Memoria>();
        public MotorExecucao() {}
        public E criarEvento<E>() where E:Evento<Memoria>
        {
            return (E)Activator.CreateInstance(typeof(E), this);
        }
    }
}
