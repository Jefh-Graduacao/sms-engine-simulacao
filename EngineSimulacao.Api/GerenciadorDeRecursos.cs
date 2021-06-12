using System;
using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    public static class GerenciadorDeRecursos<TRecurso> where TRecurso : RecursoGerenciado, new()
    {
        public static void CriarRecursos(int quantidade)
        {
            for (int i = 0; i < quantidade; i++)
            {
                new TRecurso();
            }
        }

        private static List<TRecurso> _listarRecursosLivres()
        {
            List<InfoInstancia<TRecurso>> infoRecursosVivos = Gerenciador<TRecurso>.listarVivos();
            List<TRecurso> recursosLivres = new();
            foreach (var infoRecurso in infoRecursosVivos)
            {
                if (false == infoRecurso.Instancia.Alocado)
                    recursosLivres.Add(infoRecurso.Instancia);
            }
            return recursosLivres;
        }

        public static bool VerificarDisponibilidade(int quantidade)
        {
            List<TRecurso> recursosLivres = _listarRecursosLivres();
            return recursosLivres.Count >= quantidade;
        }

        public static IEnumerable<IAlocacaoGerenciada<TRecurso>> Alocar(int quantidade)
        {
            List<TRecurso> recursosLivres = _listarRecursosLivres();

            if (recursosLivres.Count < quantidade)
                throw new Exception("Não existem recursos livres suficientes");

            List<IAlocacaoGerenciada<TRecurso>> novasAlocacoes = new();
            for (int i = 0; i < quantidade; i++)
            {
                var novaAlocacao = new AlocacaoGerenciada<TRecurso>(recursosLivres[i]);
                novasAlocacoes.Add(novaAlocacao);
            }

            return novasAlocacoes;
        }

        public static void Liberar(IEnumerable<IAlocacaoGerenciada<TRecurso>> listaAlocacao)
        {
            foreach (var alocacao in listaAlocacao)
            {
                alocacao.Desalocar();
            }
        }
    }
}