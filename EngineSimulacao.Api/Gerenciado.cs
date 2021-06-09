using System;
using System.Reflection;

namespace EngineSimulacao.Api
{
    /// <summary>
    /// Gerencia o Id das instâncias, e o controle de seu 
    /// histórico através dos históricos relevantes
    /// </summary>
    public abstract class Gerenciado
    {
        private static int _contadorId;
        private static int _gerarId() => _contadorId++;
        public int Id { get; private set; }

        public Gerenciado()
        {
            this.Id = _gerarId();
        }
        /// <summary>
        /// Adiciona info da da criação da instância no histórico 
        /// de sua classe, e de todas as classes que herda
        /// </summary>
        protected void _nascerEmTodosOsNiveis(){
            Type tipoAtual = this.GetType();

            while(tipoAtual.Name != "Gerenciado"){
                this.chamarGerenciadorDoTipo(tipoAtual, "nascimento");
                tipoAtual = tipoAtual.BaseType;
            }
        }
        /// <summary>
        /// Adiciona info da destruição da instância no histórico 
        /// de sua classe, e de todas as classes que herda
        /// </summary>
        protected void _morrerEmTodosOsNiveis(){
            Type tipoAtual = this.GetType();

            while(tipoAtual.Name != "Gerenciado"){
                this.chamarGerenciadorDoTipo(tipoAtual, "morte");
                tipoAtual = tipoAtual.BaseType;
            }
        }
        /// <summary>
        /// Realiza ade maneira controlada uma chamada do formato:
        ///
        ///     Gerenciador<tipo>.nomeMetodo(this);
        ///
        /// </summary>
        private void chamarGerenciadorDoTipo(Type tipo, string nomeMetodo){
            var gerenciadorDoTipo = typeof(Gerenciador<>).MakeGenericType(tipo);
            MethodInfo metodo = gerenciadorDoTipo.GetMethod(nomeMetodo);
            object[] argumentos = {this};
            metodo.Invoke(null, argumentos);
        }
    }
}