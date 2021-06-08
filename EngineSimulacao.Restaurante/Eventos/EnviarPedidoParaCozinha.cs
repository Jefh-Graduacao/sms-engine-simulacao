using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Recursos;
using System;

namespace EngineSimulacao.Restaurante.Eventos
{
    public sealed class EnviarPedidoParaCozinha : Evento
    {
        protected override void Estrategia()
        {
            const int qtdCozinheirosNecessarios = 1;

            if (!GerenciadorDeRecursos<Cozinheiro>.VerificarDisponibilidade(qtdCozinheirosNecessarios))
                return;

            var cozinheiro = GerenciadorDeRecursos<Cozinheiro>.Alocar(qtdCozinheirosNecessarios);
            var pedido = MotorRestaurante.FilaPedidosCozinha.Remover();

            Agendador.AgendarEm(new PedidoPreparado(cozinheiro, pedido), 14); // normal (14, 5)
        }

        protected override void Destruir()
        {
            throw new NotImplementedException();
        }
    }
}
