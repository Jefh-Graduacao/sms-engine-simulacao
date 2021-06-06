using System;
using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    static public class Gerenciador<T> where T:ITemID
    {
        static private Historico<T> historico = new();
        static public int gerarId()
        {
            return historico.lista.Count;
        }
        static public void nascimento(T instancia)
        {
            historico.nascimento(instancia);
        }
        static public void morte(T instancia){
            historico.morte(instancia);
        }
        // static private E instanciar<E>(int id) where E:T
        // {
        //     E instancia = (E)Activator.CreateInstance(typeof(E), id);
        //     return instancia;
        // }
    }
}