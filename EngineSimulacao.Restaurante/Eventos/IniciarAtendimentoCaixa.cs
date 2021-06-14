using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Entidades;

namespace EngineSimulacao.Restaurante.Eventos
{
    public sealed class IniciarAtendimentoCaixa : EventoGerenciado
    {
        private int _caixa;

        public IniciarAtendimentoCaixa(int caixa)
        {
            _caixa = caixa;
        }
        public bool VerificarFila()
        {
            if (_caixa == 1)
                return MotorRestaurante.FilaCaixa1.TamanhoAtual > 0;
            else
                return MotorRestaurante.FilaCaixa2.TamanhoAtual > 0;
        }
        public GrupoClientes RemoverDaFilaAdequada()
        {
            if (_caixa == 1)
                return MotorRestaurante.FilaCaixa1.Remover();
            else
                return MotorRestaurante.FilaCaixa2.Remover();
        }
        protected override void Estrategia()
        {
            if (!VerificarFila()) return;

            var clientes = RemoverDaFilaAdequada();

            Agendador.AgendarEm(new FinalizarAtendimentoCaixa(_caixa, clientes), 12);
        }
    }
}