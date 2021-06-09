using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Entidades;
using EngineSimulacao.Restaurante.Recursos;
using System;

namespace EngineSimulacao.Restaurante.Eventos.Clientes
{
    public sealed class IrParaBalcao : EventoGerenciado
    {
        private readonly GrupoClientes _clientes;

        public IrParaBalcao(GrupoClientes clientes)
        {
            _clientes = clientes;
        }

        protected override void Estrategia()
        {
            if (!GerenciadorDeRecursos<BancoBalcao>.VerificarDisponibilidade(1))
            {
                MotorRestaurante.FilaBalcao.Adicionar(_clientes);
                return;
            }

            GerenciadorDeRecursos<BancoBalcao>.Alocar(1);
        }
    }
}
