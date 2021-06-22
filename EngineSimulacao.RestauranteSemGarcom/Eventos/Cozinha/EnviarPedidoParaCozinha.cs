using EngineSimulacao.Api;
using EngineSimulacao.RestauranteSemGarcom.Recursos;

namespace EngineSimulacao.RestauranteSemGarcom.Eventos.Cozinha
{
    public sealed class EnviarPedidoParaCozinha : EventoGerenciado
    {
        protected override void Estrategia()
        {

            if (!(MotorRestaurante.FilaPedidosCozinha.TamanhoAtual > 0))
                return;

            const int qtdCozinheirosNecessarios = 1;

            if (!GerenciadorDeRecursos<Cozinheiro>.VerificarDisponibilidade(qtdCozinheirosNecessarios))
                return;

            var cliente = MotorRestaurante.FilaPedidosCozinha.Remover();
            var cozinheiros = GerenciadorDeRecursos<Cozinheiro>.Alocar(qtdCozinheirosNecessarios);

            Agendador.AgendarEm(new PedidoPreparado(cozinheiros, cliente), MotorRestaurante.TempoPreparoPedido);
        }
    }
}
