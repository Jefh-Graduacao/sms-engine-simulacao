using EngineSimulacao.Api;
using EngineSimulacao.RestauranteSemGarcom.Entidades;

namespace EngineSimulacao.RestauranteSemGarcom.Eventos.Clientes
{
    public sealed class SairDaMesa : EventoGerenciado
    {
        private readonly GrupoClientes _clientes;

        public SairDaMesa(GrupoClientes clientes)
        {
            _clientes = clientes;
        }

        protected override void Estrategia()
        {
            foreach (var banco in _clientes.LugarOcupado)
                banco.Desalocar();

            Agendador.AgendarAgora(new IrParaMesa(_clientes.Quantidade));
        }
    }
}
