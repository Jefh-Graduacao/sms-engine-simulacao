using System.Collections.Generic;
using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Entidades;
using EngineSimulacao.Restaurante.Recursos;

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

        private bool VerificarDisponibilidade(int quantidade)
        {
            switch (_caixa)
            {
                case 1: return GerenciadorDeRecursos<AtendenteCaixa1>.VerificarDisponibilidade(quantidade);
                default: return GerenciadorDeRecursos<AtendenteCaixa2>.VerificarDisponibilidade(quantidade);
            }
        }

        private IEnumerable<IAlocacaoGerenciada<RecursoGerenciado>> AlocarRecursoAdequado(int quantidade)
        {
            switch (_caixa)
            {
                case 1: return GerenciadorDeRecursos<AtendenteCaixa1>.Alocar(quantidade);
                default: return GerenciadorDeRecursos<AtendenteCaixa2>.Alocar(quantidade);
            }
        }

        protected override void Estrategia()
        {
            if (!VerificarFila()) return;

            if (!VerificarDisponibilidade(1)) return;

            IEnumerable<IAlocacaoGerenciada<RecursoGerenciado>> atendentes = AlocarRecursoAdequado(1);

            var clientes = RemoverDaFilaAdequada();

            Agendador.AgendarEm(new FinalizarAtendimentoCaixa(_caixa, clientes, atendentes), MotorRestaurante.TempoAtendimentoCaixa);
        }
    }
}