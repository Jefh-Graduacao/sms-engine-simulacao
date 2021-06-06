using System;
using System.Collections.Generic;

namespace EngineSimulacao.Api
{
        public class Recurso:ITemID
    {
        public int Id { get; private set; }
        public bool Alocado { get; set; } = false;
        public Recurso(){
            this.Id = Gerenciador<Recurso>.gerarId();
            Gerenciador<Recurso>.nascimento(this);
        }
    }
}
