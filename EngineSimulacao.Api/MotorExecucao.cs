using System;


namespace EngineSimulacao.Api
{   
    public abstract class MotorExecucao<Memoria> where Memoria:new()
    {
        public Memoria memoria = new Memoria();
        public Agendador<Memoria> Agendador = new Agendador<Memoria>();
        
         public T CriarEvento<T>() where T:Evento<Memoria> {
            return (T)Activator.CreateInstance(typeof(T), this);
        }
        public T CriarEvento<T>(ParametrosEvento parametros) where T:Evento<Memoria> {
            return (T)Activator.CreateInstance(typeof(T), this, parametros);
        }
    }
}
