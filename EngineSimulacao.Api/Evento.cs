namespace EngineSimulacao.Api
{
    public abstract class Evento<M> where M:new()
    {
        protected readonly MotorExecucao<M> motor;
        public Evento(MotorExecucao<M> motor){
            this.motor = motor;
        }
        public abstract void Executar();
    }
}