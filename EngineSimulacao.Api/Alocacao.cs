using System;
using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    public class Alocacao<E>:TemID where E:Recurso, new()
    {
        public E recurso { get; private set; }
        public Alocacao(E recurso)
        {
            Gerenciador<Alocacao<E>>.nascimento(this);
            this.recurso = recurso;
            this.recurso.Alocado = true;
        }

        public void Desalocar()
        {
            Gerenciador<Alocacao<E>>.morte(this);
            this.recurso.Alocado = false;
        }
    }
}