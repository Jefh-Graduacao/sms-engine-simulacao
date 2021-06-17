using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Entidades;

namespace EngineSimulacao.Restaurante.Eventos.Clientes
{
    public sealed class ComecarAComer : EventoGerenciado
    {
        private readonly GrupoClientes _clientes;

        public ComecarAComer(GrupoClientes clientes)
        {
            this._clientes = clientes;
        }
        protected override void Estrategia()
        {
            Agendador.AgendarEm(new SairDaMesa(_clientes), MotorRestaurante.TempoDeRefeição);
        }
    }
}
