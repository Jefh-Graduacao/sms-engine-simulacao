using System;
using System.Collections.Generic;

namespace EngineSimulacao.Api
{
        public class Recurso:TemID
    {
        public bool Alocado { get; set; } = false;
        public Recurso(){
            Gerenciador<Recurso>.nascimento(this);
        }
    }
}
