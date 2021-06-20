using System;
using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Entidades;
using EngineSimulacao.Restaurante.Eventos.Caixas;
using EngineSimulacao.Restaurante.Eventos.Clientes;
using EngineSimulacao.Restaurante.Eventos.Garcom;
using RedesPetri.Entidades;
using RedesPetri.Entidades.Arcos;

namespace EngineSimulacao.Restaurante.Recursos
{
    public sealed class Garcom : RecursoGerenciado
    {
        public RedePetri RedePetri { get; }

        public Lugar GarcomLivre => RedePetri.ObterLugar(1);

        public Lugar SubstituirCaixa => RedePetri.ObterLugar(2);
        public Lugar GarcomNoCaixa => RedePetri.ObterLugar(20);
        public Lugar CaixaRetorno => RedePetri.ObterLugar(21);

        public Lugar PedidoPronto => RedePetri.ObterLugar(3);
        public Lugar LevandoPedido => RedePetri.ObterLugar(30);
        public Lugar PedidoNaMesa => RedePetri.ObterLugar(31);

        public Lugar ClienteVaiSentar => RedePetri.ObterLugar(4);
        public Lugar HigienizandoMesa => RedePetri.ObterLugar(40);
        public Lugar MesaHigienizada => RedePetri.ObterLugar(41);

        public GrupoClientes _clientes;

        public Garcom()
        {
            RedePetri = new RedePetri();
            RedePetri.CriarLugar(1, 1, marcaProduzida, marcaConsumida);
            inicializarRedeSubtituirCaixa();
            inicializarRedeEntregaPedido();
            InicializarRedeLimpezaMesa();
        }

        private void inicializarRedeSubtituirCaixa()
        {
            RedePetri.CriarLugar(2, 0, fluxoSubtituirCaixaMarcaProduzida, fluxoSubtituirCaixaMarcaConsumida);
            RedePetri.CriarLugar(20, 0, fluxoSubtituirCaixaMarcaProduzida, fluxoSubtituirCaixaMarcaConsumida);
            RedePetri.CriarLugar(21, 0, fluxoSubtituirCaixaMarcaProduzida, fluxoSubtituirCaixaMarcaConsumida);

            RedePetri.CriarTransicao(20);
            RedePetri.CriarTransicao(21);

            RedePetri.CriarArco(
                GarcomLivre,
                (RedePetri.ObterTransicao(20), 1),
                TipoArco.Normal
            );

            RedePetri.CriarArco(
                SubstituirCaixa,
                (RedePetri.ObterTransicao(20), 1),
                TipoArco.Normal
            );

            RedePetri.CriarArco(
                RedePetri.ObterTransicao(20),
                (GarcomNoCaixa, 1)
            );

            RedePetri.CriarArco(
                GarcomNoCaixa,
                (RedePetri.ObterTransicao(21), 1),
                TipoArco.Normal
            );

            RedePetri.CriarArco(
                CaixaRetorno,
                (RedePetri.ObterTransicao(21), 1),
                TipoArco.Normal
            );

            RedePetri.CriarArco(
                RedePetri.ObterTransicao(21),
                (GarcomLivre, 1)
            );
        }

        private void inicializarRedeEntregaPedido()
        {
            RedePetri.CriarLugar(3, 0, fluxoEntregaPedidoMarcaProduzida, fluxoEntregaPedidoMarcaConsumida);
            RedePetri.CriarLugar(30, 0, fluxoEntregaPedidoMarcaProduzida, fluxoEntregaPedidoMarcaConsumida);
            RedePetri.CriarLugar(31, 0, fluxoEntregaPedidoMarcaProduzida, fluxoEntregaPedidoMarcaConsumida);

            RedePetri.CriarTransicao(30);
            RedePetri.CriarTransicao(31);

            RedePetri.CriarArco(
                GarcomLivre,
                (RedePetri.ObterTransicao(30), 1),
                TipoArco.Normal
            );

            RedePetri.CriarArco(
                PedidoPronto,
                (RedePetri.ObterTransicao(30), 1),
                TipoArco.Normal
            );

            RedePetri.CriarArco(
                RedePetri.ObterTransicao(30),
                (LevandoPedido, 1)
            );

            RedePetri.CriarArco(
                LevandoPedido,
                (RedePetri.ObterTransicao(31), 1),
                TipoArco.Normal
            );

            RedePetri.CriarArco(
                PedidoNaMesa,
                (RedePetri.ObterTransicao(31), 1),
                TipoArco.Normal
            );

            RedePetri.CriarArco(
                RedePetri.ObterTransicao(31),
                (GarcomLivre, 1)
            );
        }

        private void InicializarRedeLimpezaMesa()
        {
            RedePetri.CriarLugar(4, 0, fluxoClienteVaiSentarMarcaProduzida, fluxoClienteVaiSentarMarcaConsumida);
            RedePetri.CriarLugar(40, 0, fluxoClienteVaiSentarMarcaProduzida, fluxoClienteVaiSentarMarcaConsumida);
            RedePetri.CriarLugar(41, 0, fluxoClienteVaiSentarMarcaProduzida, fluxoClienteVaiSentarMarcaConsumida);

            RedePetri.CriarTransicao(40);
            RedePetri.CriarTransicao(41);

            RedePetri.CriarArco(
                GarcomLivre,
                (RedePetri.ObterTransicao(40), 1),
                TipoArco.Normal
            );

            RedePetri.CriarArco(
                ClienteVaiSentar,
                (RedePetri.ObterTransicao(40), 1),
                TipoArco.Normal
            );

            RedePetri.CriarArco(
                RedePetri.ObterTransicao(40),
                (HigienizandoMesa, 1)
            );

            RedePetri.CriarArco(
                HigienizandoMesa,
                (RedePetri.ObterTransicao(41), 1),
                TipoArco.Normal
            );

            RedePetri.CriarArco(
                MesaHigienizada,
                (RedePetri.ObterTransicao(41), 1),
                TipoArco.Normal
            );

            RedePetri.CriarArco(
                RedePetri.ObterTransicao(41),
                (GarcomLivre, 1)
            );
        }

        public void Executar()
        {
            RedePetri.ExecutarCiclo();
        }

        private void marcaProduzida(Lugar lugar)
        {
        }

        private void marcaConsumida(Lugar lugar)
        {
        }

        private void fluxoSubtituirCaixaMarcaProduzida(Lugar lugar)
        {
            switch (lugar.Id)
            {
                case 20:
                {
                    Agendador.AgendarEm(new FinalizarIdaAoBanheiro(), MotorRestaurante.TempoRetornoDoBanheiro);
                    break;
                }
            }
        }

        private void fluxoSubtituirCaixaMarcaConsumida(Lugar lugar)
        {
            switch (lugar.Id)
            {
                case 2:
                {
                    if (MotorRestaurante.Debug) Console.WriteLine($"\t\t\tCaixa vai ao banheiro {Agendador.Tempo:N6}");
                    break;
                }
                case 21:
                {
                    if (MotorRestaurante.Debug) Console.WriteLine($"\t\t\tCaixa volta do banheiro {Agendador.Tempo:N6}");
                    break;
                }
                default:
                {
                    break;
                }
            }
        }

        private void fluxoEntregaPedidoMarcaProduzida(Lugar lugar)
        {
            switch (lugar.Id)
            {
                case 30:
                    {
                        Agendador.AgendarEm(new FinalizarEntregaPedido(), MotorRestaurante.TempoEntregaPedidoPeloGarcom);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void fluxoEntregaPedidoMarcaConsumida(Lugar lugar)
        {
            switch (lugar.Id)
            {
                case 3:
                    {
                        _clientes = MotorRestaurante.FilaEntrega.Remover();
                        if (MotorRestaurante.Debug) Console.WriteLine($"\tGarçom começa a entrega {_clientes.Id}! {Agendador.Tempo}");
                        break;
                    }
                case 31:
                    {
                        Agendador.AgendarAgora(new ComecarAComer(_clientes));
                        if (MotorRestaurante.Debug) Console.WriteLine($"\tCliente {_clientes.Id} vai comecar comer! {Agendador.Tempo}");
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void fluxoClienteVaiSentarMarcaProduzida(Lugar lugar)
        {
            switch (lugar.Id)
            {
                case 40:
                    {
                        Agendador.AgendarEm(new FinalizarHigienizarMesa(), MotorRestaurante.TempoHigienizaçaoMesaPeloGarcom);
                        if (MotorRestaurante.Debug) Console.WriteLine($"\t\tGarçom comeca limpar mesa! {Agendador.Tempo}");
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void fluxoClienteVaiSentarMarcaConsumida(Lugar lugar)
        {
            switch (lugar.Id)
            {
                case 4:
                    {
                        if (MotorRestaurante.Debug) Console.WriteLine($"\t\tCliente sentou! {Agendador.Tempo}");
                        break;
                    }
                case 41:
                    {
                        if (MotorRestaurante.Debug) Console.WriteLine($"\t\tMesa Higienizada!{Agendador.Tempo}");
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
