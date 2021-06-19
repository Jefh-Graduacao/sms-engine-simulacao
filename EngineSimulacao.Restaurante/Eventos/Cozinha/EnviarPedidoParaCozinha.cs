using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Recursos;

namespace EngineSimulacao.Restaurante.Eventos
{
    public sealed class EnviarPedidoParaCozinha : EventoGerenciado
    {
        protected override void Estrategia()
        {
            const int qtdCozinheirosNecessarios = 1;

            if (!GerenciadorDeRecursos<Cozinheiro>.VerificarDisponibilidade(qtdCozinheirosNecessarios))
                return;

            var cliente = MotorRestaurante.FilaPedidosCozinha.Remover();
            var cozinheiros = GerenciadorDeRecursos<Cozinheiro>.Alocar(qtdCozinheirosNecessarios);

            Agendador.AgendarEm(new PedidoPreparado(cozinheiros, cliente), MotorRestaurante.TempoPreparoPedido);
        }
    }
}
