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
        public Lugar PedidoPronto => RedePetri.ObterLugar(3);
        public Lugar LevandoPedido => RedePetri.ObterLugar(30);
        public Lugar PedidoNaMesa => RedePetri.ObterLugar(31);
        
        public GrupoClientes _clientes;
        public Garcom()
        {
            RedePetri = new RedePetri();
            RedePetri.CriarLugar(1, 1, this.marcaProduzida, this.marcaConsumida);
            inicializarRedeEntregaPedido();
        }

        private void inicializarRedeEntregaPedido()
        {
            RedePetri.CriarLugar(3, 0, this.marcaProduzida, this.marcaConsumida);   
            RedePetri.CriarLugar(30, 0, this.marcaProduzida, this.marcaConsumida);
            RedePetri.CriarLugar(31, 0, this.marcaProduzida, this.marcaConsumida);

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

        public void Executar()
        {
            RedePetri.ExecutarCiclo();
        }

        private void marcaProduzida(Lugar lugar)
        {
            fluxoEntregaPedidoMarcaProduzida(lugar);
        }

        private void marcaConsumida(Lugar lugar)
        {
            fluxoEntregaPedidoMarcaConsumida(lugar);
        }

        private void fluxoEntregaPedidoMarcaProduzida(Lugar lugar)
        {
            switch(lugar.Id){
                case 30:
                {
                    Agendador.AgendarEm(new FinalizarEntregaPedido(), GeradorRandomicoContexto.Gerador.Normal(2, 0.3));
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
            switch(lugar.Id){
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
        private void transicaoSaida(Transicao transicao)
        {
            Console.WriteLine("saida transição " + transicao.Id);
        }
    }
}
