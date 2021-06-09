using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Entidades;

namespace EngineSimulacao.Restaurante.Eventos.Clientes
{
    public sealed class ChegadaClientes : EventoGerenciado
    {
        protected override void Estrategia()
        {
            if (MotorRestaurante.FilaCaixa1.TamanhoAtual < MotorRestaurante.FilaCaixa2.TamanhoAtual)
                MotorRestaurante.FilaCaixa1.Adicionar(new GrupoClientes());
            else
                MotorRestaurante.FilaCaixa2.Adicionar(new GrupoClientes());
        }
    }
}
