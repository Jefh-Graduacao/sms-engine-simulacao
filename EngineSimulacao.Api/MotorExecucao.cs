using System;
using System.Collections.Generic;

namespace EngineSimulacao.Api
{   
    public class MotorExecucao<EnumConjuntos> where EnumConjuntos:struct, Enum
    {
        private readonly Dictionary<EnumConjuntos, ConjuntoEntidade<EnumConjuntos>> conjuntos = new();
        public readonly Agendador<EnumConjuntos> Agendador = new Agendador<EnumConjuntos>();
        public MotorExecucao() {
            this.instanciarConjuntos();
        }
        public E criarEvento<E>() where E:Evento<EnumConjuntos>
        {
            return (E)Activator.CreateInstance(typeof(E), this);
        }
        public ConjuntoEntidade<EnumConjuntos> PegarConjunto(EnumConjuntos nomeConjunto) {
            this.conjuntos.TryGetValue(nomeConjunto, out var conjunto);
            return conjunto;
        }

        private void instanciarConjuntos(){
            foreach (string stringNome in Enum.GetNames(typeof(EnumConjuntos)))
            {
                var nomeConjunto = Enum.Parse<EnumConjuntos>(stringNome);
                this.conjuntos.Add(nomeConjunto, new ConjuntoEntidade<EnumConjuntos>(this));
            }
        }
    }
}
