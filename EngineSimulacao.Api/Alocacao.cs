using System;
using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    public class Alocacao<E>:ITemID where E:Recurso, new()
    {
        public int Id { get; private set; }
        public E recurso { get; private set; }
        public Alocacao(E recurso)
        {
            this.Id = Gerenciador<Alocacao<E>>.gerarId();
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