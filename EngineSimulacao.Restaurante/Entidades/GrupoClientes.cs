using EngineSimulacao.Api;
using System.Collections.Generic;

namespace EngineSimulacao.Restaurante.Entidades
{
    public sealed class GrupoClientes : EntidadeGerenciada
    {
        public int Quantidade { get; }
        public Pedido Pedido { get; set; }
        public IEnumerable<IAlocacaoGerenciada<RecursoGerenciado>> LugarOcupado;

        public GrupoClientes()
        {
            Quantidade = (int) GeradorRandomicoContexto.Gerador.Uniforme(1,4);
        }
    }
}
