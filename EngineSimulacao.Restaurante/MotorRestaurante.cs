using System.Linq;
using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Entidades;
using EngineSimulacao.Restaurante.Recursos;

namespace EngineSimulacao.Restaurante
{
    public static class MotorRestaurante
    {
        public static readonly ConjuntoEntidade<GrupoClientes> FilaCaixa1 = new("Fila Caixa 1");
        public static readonly ConjuntoEntidade<GrupoClientes> FilaCaixa2 = new("Fila Caixa 2");

        public static readonly ConjuntoEntidade<GrupoClientes> FilaPedidosCozinha = new("Fila Pedidos Cozinha");
        public static readonly ConjuntoEntidade<GrupoClientes> FilaEntrega = new("Fila de Pedidos para Entrega");

        public static readonly ConjuntoEntidade<GrupoClientes> FilaBalcao = new("Fila Balcão");
        public static readonly ConjuntoEntidade<GrupoClientes> FilaMesa2Lugares = new("Fila Mesas de 2 Lugares");
        public static readonly ConjuntoEntidade<GrupoClientes> FilaMesa4Lugares = new("Fila Mesas de 4 Lugares");
        public static Garcom garcom;
        public static void Inicializar()
        {
            GerenciadorDeRecursos<Garcom>.CriarRecursos(1);
            garcom = GerenciadorDeRecursos<Garcom>.Alocar(1).First().Recurso;

            GerenciadorDeRecursos<AtendenteCaixa>.CriarRecursos(2);
            GerenciadorDeRecursos<Cozinheiro>.CriarRecursos(3);
            GerenciadorDeRecursos<Garcom>.CriarRecursos(2);

            GerenciadorDeRecursos<BancoBalcao>.CriarRecursos(6);
            GerenciadorDeRecursos<Mesa2Lugares>.CriarRecursos(4);
            GerenciadorDeRecursos<Mesa4Lugares>.CriarRecursos(4);
        }
    }
}
