using System;
using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    static public class GerenciadorDeRecursos<E> where E:Recurso, new()
    {
        static public void criarNRecursos(int quantidade)
        {
            for(int i = 0; i<quantidade; i++){
                new E();
            }
        }
        static private List<E> _listarRecursosLivres(){
            List<InfoInstancia<E>> infoRecursosVivos = Gerenciador<E>.listarVivos();
            List<E> recursosLivres = new();
            foreach(var infoRecurso in infoRecursosVivos){
                if(false == infoRecurso.Instancia.Alocado)
                    recursosLivres.Add(infoRecurso.Instancia);
            }
            return recursosLivres;
        }
        static public bool VerificarDisponibilidade(int quantidade)
        {
            List<E> recursosLivres = _listarRecursosLivres();
            return recursosLivres.Count >= quantidade;
        }
        static public List<Alocacao<E>> Alocar(int quantidade)
        {
            List<E> recursosLivres = _listarRecursosLivres();

            if(recursosLivres.Count < quantidade)
                throw new Exception("Não existem recursos livres suficientes");

            List<Alocacao<E>> novasAlocacoes = new();
            for(int i = 0; i < quantidade; i++){
                var novaAlocacao = new Alocacao<E>(recursosLivres[i]);
                novasAlocacoes.Add(novaAlocacao);
            }
            
            return novasAlocacoes;
        }
        static public void Liberar(List<Alocacao<E>> listaAlocacao)
        {
            foreach(var alocacao in listaAlocacao)
            {
                alocacao.Desalocar();
            }
        }
    }
}