using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Eventos;
using EngineSimulacao.Restaurante.Eventos.Clientes;

namespace EngineSimulacao.Restaurante
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            MotorRestaurante.Inicializar();

            var evento = new ChegadaClientes();
            Agendador.AgendarAgora(evento);
            Agendador.Simular();
        }
    }
}
