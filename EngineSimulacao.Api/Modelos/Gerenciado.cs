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
        public static string NomeTipo { get; } = nameof(Gerenciado);
        private static int _contadorId;
        private static int _gerarId() => _contadorId++;
        public int Id { get; private set; }

        protected Gerenciado()
        {
            Id = _gerarId();
        }

        /// <summary>
        /// Adiciona info da criação da instância no histórico 
        /// de sua classe, e de todas as classes que herda
        /// </summary>
        protected void _nascerEmTodosOsNiveis()
        {
            Type tipoAtual = GetType();

            while (tipoAtual.Name != NomeTipo)
            {
                chamarGerenciadorDoTipo(tipoAtual, "nascimento");
                tipoAtual = tipoAtual.BaseType;
            }
        }

        /// <summary>
        /// Adiciona info da destruição da instância no histórico 
        /// de sua classe, e de todas as classes que herda
        /// </summary>
        protected void _morrerEmTodosOsNiveis()
        {
            Type tipoAtual = GetType();

            while (tipoAtual.Name != "Gerenciado")
            {
                chamarGerenciadorDoTipo(tipoAtual, "morte");
                tipoAtual = tipoAtual.BaseType;
            }
        }
        /// <summary>
        /// Realiza a de maneira controlada uma chamada do formato:
        ///
        ///     Gerenciador<tipo>.nomeMetodo(this);
        ///
        /// </summary>
        private void chamarGerenciadorDoTipo(Type tipo, string nomeMetodo)
        {
            var gerenciadorDoTipo = typeof(Gerenciador<>).MakeGenericType(tipo);
            MethodInfo metodo = gerenciadorDoTipo.GetMethod(nomeMetodo);
            object[] argumentos = { this };
            metodo.Invoke(null, argumentos);
        }
    }
}