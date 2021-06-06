using System;
using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    static public class Gerenciador<T> where T:ITemID
    {
        static private int _contadorId;
        static private Historico<T> historico = new();
        static public int gerarId() => _contadorId++;
        static public void nascimento(T instancia)
        {
            historico.nascimento(instancia);
        }
        static public void morte(T instancia)
        {
            historico.morte(instancia);
        }
        static public List<InfoInstancia<T>> listarVivos()
        {
            return historico.listarVivos();
        }
        static public List<InfoInstancia<T>> listarMortos()
        {
            return historico.listarMortos();
        }
    }
}