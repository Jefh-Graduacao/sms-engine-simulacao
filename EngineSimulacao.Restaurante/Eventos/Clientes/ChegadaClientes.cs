using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Entidades;

namespace EngineSimulacao.Restaurante.Eventos.Clientes
{
    public sealed class ChegadaClientes : EventoGerenciado
    {
        protected override void Estrategia()
        {
            if (MotorRestaurante.FilaCaixa1.TamanhoAtual < MotorRestaurante.FilaCaixa2.TamanhoAtual)
            {
                MotorRestaurante.FilaCaixa1.Adicionar(new GrupoClientes());
                var evtIniciarCaixa1 = new IniciarAtendimentoCaixa(1);
                Agendador.AgendarAgora(evtIniciarCaixa1);
            } else 
            {
                MotorRestaurante.FilaCaixa2.Adicionar(new GrupoClientes());
                var evtIniciarCaixa2 = new IniciarAtendimentoCaixa(2);
                Agendador.AgendarAgora(evtIniciarCaixa2);
            }

            if (Agendador.Tempo >= 180)
                return;

            var evtChegada = new ChegadaClientes();
            Agendador.AgendarEm(evtChegada, 3);
        }
    }
}
