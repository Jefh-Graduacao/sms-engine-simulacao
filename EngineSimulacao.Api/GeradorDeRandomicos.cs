using System;

namespace EngineSimulacao.Api
{
    public static class GeradorRandomicoContexto
    {
        public static GeradorDeRandomicosLCG Gerador => new GeradorDeRandomicosLCG();
    }

    public class GeradorDeRandomicosLCG
    {
        private const long m = 4294967296;
        private const long a = 1664525;
        private const long c = 1013904223;
        private long _ultimoGerado;
        private double _penultimoGerado_0a1 = -999.99; //usado para nao definido -999.99
        private double _ultimoGerado_0a1;

        public GeradorDeRandomicosLCG()
        {
            _ultimoGerado = DateTime.Now.Ticks % m;
        }

        public GeradorDeRandomicosLCG(long seed)
        {
            _ultimoGerado = seed;
        }

        public long Proximo()
        {
            _ultimoGerado = ((a * _ultimoGerado) + c) % m;

            return _ultimoGerado;
        }

        public long Proximo(long ValorMáximo)
        {
            return Proximo() % ValorMáximo;
        }

        public double ProximoComEscalaDe0a1(long valorMaximo = 1000)
        {
            _penultimoGerado_0a1 = _ultimoGerado_0a1;
            _ultimoGerado_0a1 = (Proximo() % valorMaximo) / (valorMaximo - 1.0);
            return _ultimoGerado_0a1;
        }

        public double Exponencial(double média)
        {
            double x = ProximoComEscalaDe0a1();
            return -média * Math.Log(1.0 - x);
        }

        public double Normal(double media, double desvio)
        {
            if (_penultimoGerado_0a1 == -999.99)
            {
                ProximoComEscalaDe0a1();
            }

            ProximoComEscalaDe0a1();

            double Vi1 = 2.00 * _penultimoGerado_0a1 - 1.00;
            double Vi2 = 2.00 * _ultimoGerado_0a1 - 1.00;
            double W = Vi1 * Vi1 + Vi2 * Vi2;

            if (W < 1.0)
            {
                double y = Math.Sqrt((-2 * Math.Log(W)) / W);
                double x1 = y * Vi2;
                double normal = media + desvio * x1;
                return normal;
            }
            else
            {
                return Normal(media, desvio);
            }
        }
    }
}