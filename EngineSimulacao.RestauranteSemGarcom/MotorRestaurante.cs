using System.Linq;
using EngineSimulacao.Api;
using EngineSimulacao.RestauranteSemGarcom.Entidades;
using EngineSimulacao.RestauranteSemGarcom.Eventos.Caixas;
using EngineSimulacao.RestauranteSemGarcom.Recursos;

namespace EngineSimulacao.RestauranteSemGarcom
{
    public static class MotorRestaurante
    {
        public const bool Debug = true;

        // -----  FILAS -----
        public static readonly ConjuntoEntidade<GrupoClientes> FilaCaixa1 = new("Fila Caixa 1");
        public static readonly ConjuntoEntidade<GrupoClientes> FilaCaixa2 = new("Fila Caixa 2");

        public static readonly ConjuntoEntidade<GrupoClientes> FilaPedidosCozinha = new("Fila Pedidos Cozinha");

        public static readonly ConjuntoEntidade<GrupoClientes> FilaBalcao = new("Fila Balcão");
        public static readonly ConjuntoEntidade<GrupoClientes> FilaMesa2Lugares = new("Fila Mesas de 2 Lugares");
        public static readonly ConjuntoEntidade<GrupoClientes> FilaMesa4Lugares = new("Fila Mesas de 4 Lugares");

        // ----- TEMPOS EM MINUTOS
        public const string unidadeMedidaTempo = "min";
        public const double TempoMaximoChegadaClientes = 180.00;
        public static double TEC_ChegadaCLientes => GeradorRandomicoContexto.Gerador.Exponencial(3);
        public static double TempoAtendimentoCaixa => GeradorRandomicoContexto.Gerador.Normal(8, 2);
        public static double TempoPreparoPedido => GeradorRandomicoContexto.Gerador.Normal(14, 5, 0.1, 35);
        public static double TempoDeRefeição => GeradorRandomicoContexto.Gerador.Normal(20, 8, 0.1, 45);

        public static void Inicializar()
        {
            GerenciadorDeRecursos<AtendenteCaixa1>.CriarRecursos(1);
            GerenciadorDeRecursos<AtendenteCaixa2>.CriarRecursos(1);
            GerenciadorDeRecursos<Cozinheiro>.CriarRecursos(3);

            GerenciadorDeRecursos<BancoBalcao>.CriarRecursos(6);
            GerenciadorDeRecursos<Mesa2Lugares>.CriarRecursos(4);
            GerenciadorDeRecursos<Mesa4Lugares>.CriarRecursos(4);
        }
    }
}
