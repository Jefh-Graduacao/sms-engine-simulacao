using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Entidades;
using EngineSimulacao.Restaurante.Recursos;
using System.Collections.Generic;

namespace EngineSimulacao.Restaurante.Eventos.Clientes
{
    public sealed class IrParaMesa : EventoGerenciado
    {
        private int _quantidadeLugares;

        public IrParaMesa(int quantidadeLugares)
        {
            _quantidadeLugares = quantidadeLugares;
        }
        protected override void Estrategia()
        {
            if (!VerificarFila()) return;

            if (!VerificarDisponibilidade(1)) return;

            var clientes = RemoverDaFilaAdequada();

            clientes.LugarOcupado = AlocarMesa(1);

            if (clientes.Pedido.ProntroParaComer)
            {
                Agendador.AgendarAgora(new ComecarAComer(clientes));
            }
        }

        private IEnumerable<IAlocacaoGerenciada<RecursoGerenciado>> AlocarMesa(int quantidade)
        {
            switch (_quantidadeLugares)
            {
                case 1: return GerenciadorDeRecursos<BancoBalcao>.Alocar(quantidade);
                case 2: return GerenciadorDeRecursos<Mesa2Lugares>.Alocar(quantidade);
                default: return GerenciadorDeRecursos<Mesa4Lugares>.Alocar(quantidade);
            }
        }

        private bool VerificarDisponibilidade(int quantidade)
        {
            switch (_quantidadeLugares)
            {
                case 1: return GerenciadorDeRecursos<BancoBalcao>.VerificarDisponibilidade(quantidade);
                case 2: return GerenciadorDeRecursos<Mesa2Lugares>.VerificarDisponibilidade(quantidade);
                default: return GerenciadorDeRecursos<Mesa4Lugares>.VerificarDisponibilidade(quantidade);
            }
        }
        private bool VerificarFila()
        {
            switch (_quantidadeLugares)
            {
                case 1: return MotorRestaurante.FilaBalcao.TamanhoAtual > 0;
                case 2: return MotorRestaurante.FilaMesa2Lugares.TamanhoAtual > 0;
                default: return MotorRestaurante.FilaMesa4Lugares.TamanhoAtual > 0;
            }
        }
        private GrupoClientes RemoverDaFilaAdequada()
        {
            switch (_quantidadeLugares)
            {
                case 1: return MotorRestaurante.FilaBalcao.Remover();
                case 2: return MotorRestaurante.FilaMesa2Lugares.Remover();
                default: return MotorRestaurante.FilaMesa4Lugares.Remover();
            }
        }
    }
}
