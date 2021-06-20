using System;
using EngineSimulacao.Api;
using EngineSimulacao.Restaurante.Entidades;
using EngineSimulacao.Restaurante.Eventos.Clientes;
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
            RedePetri.CriarLugar(1, 1, this.marcaProduzida, this.marcaConsumida);
            inicializarRedeSubtituirCaixa();
            inicializarRedeEntregaPedido();
            inicializarRedeClienteVaiSentar();
        }

        private void inicializarRedeSubtituirCaixa()
        {
            RedePetri.CriarLugar(2, 0, this.fluxoSubtituirCaixaMarcaProduzida, this.fluxoSubtituirCaixaMarcaConsumida);
            RedePetri.CriarLugar(20, 0, this.fluxoSubtituirCaixaMarcaProduzida, this.fluxoSubtituirCaixaMarcaConsumida);
            RedePetri.CriarLugar(21, 0, this.fluxoSubtituirCaixaMarcaProduzida, this.fluxoSubtituirCaixaMarcaConsumida);

            RedePetri.CriarTransicao(20, this.transicaoSaida);
            RedePetri.CriarTransicao(21, this.transicaoSaida);

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
            RedePetri.CriarLugar(3, 0, this.fluxoEntregaPedidoMarcaProduzida, this.fluxoEntregaPedidoMarcaConsumida);
            RedePetri.CriarLugar(30, 0, this.fluxoEntregaPedidoMarcaProduzida, this.fluxoEntregaPedidoMarcaConsumida);
            RedePetri.CriarLugar(31, 0, this.fluxoEntregaPedidoMarcaProduzida, this.fluxoEntregaPedidoMarcaConsumida);

            RedePetri.CriarTransicao(30, this.transicaoSaida);
            RedePetri.CriarTransicao(31, this.transicaoSaida);

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

        private void inicializarRedeClienteVaiSentar()
        {
            RedePetri.CriarLugar(4, 0, this.fluxoClienteVaiSentarMarcaProduzida, this.fluxoClienteVaiSentarMarcaConsumida);
            RedePetri.CriarLugar(40, 0, this.fluxoClienteVaiSentarMarcaProduzida, this.fluxoClienteVaiSentarMarcaConsumida);
            RedePetri.CriarLugar(41, 0, this.fluxoClienteVaiSentarMarcaProduzida, this.fluxoClienteVaiSentarMarcaConsumida);

            RedePetri.CriarTransicao(40, this.transicaoSaida);
            RedePetri.CriarTransicao(41, this.transicaoSaida);

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

        }
        
        private void fluxoSubtituirCaixaMarcaConsumida(Lugar lugar)
        {

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
                        break;
                    }
                case 31:
                    {
                        Agendador.AgendarAgora(new ComecarAComer(_clientes));
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

        }

        private void fluxoClienteVaiSentarMarcaConsumida(Lugar lugar)
        {

        }

        private void transicaoSaida(Transicao transicao)
        {
            //Não faz nada
            //Console.WriteLine("saida transição " + transicao.Id);
        }
    }
}
