using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Entidades;
using EngineSimulacao.Restaurante.Recursos;

namespace EngineSimulacao.Restaurante.Eventos
{
    public sealed class IniciarAtendimentoCaixa : EventoGerenciado
    {
        private int _caixa;

        public IniciarAtendimentoCaixa(int caixa){
            _caixa = caixa;
        }
        public bool VerificarFila()
        {
            if(_caixa == 1)
                return MotorRestaurante.FilaCaixa1.TamanhoAtual > 0;
            else
                return MotorRestaurante.FilaCaixa2.TamanhoAtual > 0;
        }
        public GrupoClientes RemoverDaFilaAdequada()
        {
            if(_caixa == 1)
                return MotorRestaurante.FilaCaixa1.Remover();
            else
                return MotorRestaurante.FilaCaixa2.Remover();
        }
        protected override void Estrategia()
        {
            if(!VerificarFila()) return;
            
            if(!GerenciadorDeRecursos<AtendenteCaixa>.VerificarDisponibilidade(1)) return;

            var atendentes = GerenciadorDeRecursos<AtendenteCaixa>.Alocar(1);
            
            var clientes = RemoverDaFilaAdequada();

            Agendador.AgendarEm(new FinalizarAtendimentoCaixa(_caixa, clientes, atendentes), 12);
        }
    }
}