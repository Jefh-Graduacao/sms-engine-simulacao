using System;
using System.Reflection;

namespace EngineSimulacao.Api
{
    public abstract class Gerenciado
    {
        private static int _contadorId;
        private static int _gerarId() => _contadorId++;
        public int Id { get; private set; }

        public Gerenciado()
        {
            this.Id = _gerarId();
        }

        protected void _nascerEmTodosOsNiveis(){
            Type tipoAtual = this.GetType();

            while(tipoAtual.Name != "Gerenciado"){
                this.chamarGerenciadorDoTipo(tipoAtual, "nascimento");
                tipoAtual = tipoAtual.BaseType;
            }
        }
        protected void _morrerEmTodosOsNiveis(){
            Type tipoAtual = this.GetType();

            while(tipoAtual.Name != "Gerenciado"){
                this.chamarGerenciadorDoTipo(tipoAtual, "morte");
                tipoAtual = tipoAtual.BaseType;
            }
        }
        private void chamarGerenciadorDoTipo(Type tipo, string nomeMetodo){
            var gerenciadorDoTipo = typeof(Gerenciador<>).MakeGenericType(tipo);
            MethodInfo metodo = gerenciadorDoTipo.GetMethod(nomeMetodo);
            object[] argumentos = {this};
            metodo.Invoke(null, argumentos);
        }
    }
}