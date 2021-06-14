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
        public static readonly ConjuntoEntidade<Pedido> FilaEntrega = new("Fila de Pedidos para Entrega");

        public static readonly ConjuntoEntidade<GrupoClientes> FilaBalcao = new("Fila Balcão");
        public static readonly ConjuntoEntidade<GrupoClientes> FilaMesa2Lugares = new("Fila Mesas de 2 Lugares");
        public static readonly ConjuntoEntidade<GrupoClientes> FilaMesa4Lugares = new("Fila Mesas de 4 Lugares");

        public static void Inicializar()
        {
            GerenciadorDeRecursos<AtendenteCaixa1>.CriarRecursos(1);
            GerenciadorDeRecursos<AtendenteCaixa2>.CriarRecursos(1);
            GerenciadorDeRecursos<Cozinheiro>.CriarRecursos(3);
            GerenciadorDeRecursos<Garcom>.CriarRecursos(2);

            GerenciadorDeRecursos<BancoBalcao>.CriarRecursos(6);
            GerenciadorDeRecursos<Mesa2Lugares>.CriarRecursos(4);
            GerenciadorDeRecursos<Mesa4Lugares>.CriarRecursos(4);
        }
    }
}
