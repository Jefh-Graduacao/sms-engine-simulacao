using System;
using System.IO;
using System.Threading.Tasks;

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
        public long Uniforme(long min, long max)
        {
            long valor = Proximo(max) + 1;
            if (valor < min)
                return min;
            return valor;
        }

        public double Exponencial(double média)
        {
            double x = ProximoComEscalaDe0a1();
            double exponencial = Math.Pow(-média, -1) * Math.Log(1.0 - x);
            if (exponencial > double.MaxValue)
                return Exponencial(média);
            return exponencial;
        }

        public double Normal(double media, double desvio, double min = Double.MinValue, double max = Double.MaxValue)
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

                if (normal > max)
                    return max;
                else if (normal < min)
                    return min;
                else
                    return normal;
            }
            else
            {
                return Normal(media, desvio, min, max);
            }
        }

        public async Task testadorGeradorStremsAsync(int numeroDeAmostras = 50)
        {
            int s = numeroDeAmostras;
            string[] lines = new string[s];

            for (int i = 0; i < s; i++)
            {
                var a = GeradorRandomicoContexto.Gerador;
                lines[i] = a.Normal(8, 2).ToString();
            }
            await File.WriteAllLinesAsync("normal.txt", lines);

            for (int i = 0; i < s; i++)
            {
                var ass = GeradorRandomicoContexto.Gerador;
                lines[i] = ass.Exponencial(3).ToString();
            }
            await File.WriteAllLinesAsync("exponencial.txt", lines);

        }
    }
}