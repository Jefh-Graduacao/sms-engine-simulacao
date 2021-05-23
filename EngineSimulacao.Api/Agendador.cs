namespace EngineSimulacao.Api
{
    public class Agendador
    {
        //public List<Evento> FEL (Lista de eventos lançados)
        public int tempo { get; } //em segundos
        

        //Fica "Ouvindo" o FEL (Lista de eventos lançados)
        //Pega o evento, Mapeia um evento à uma função específica, dando split de seus argumentos.
            //Ex restaurante: evento criaCliente 
                //Procura a menor fila do caixa 
                //Adiciona o cliente na fila deste caixa (por exemplo, o caixa 1)
                //Caso esta fila tiver apenas 1 cliente (o que adicionamos), lançar evento "iniciarAtendimento-caixa1"
        //Evento nome PedidoPronto -> 

        // Mapeia um evento à uma função específica
        // nome.split('-'); Switch(arr[0]){
        //    // trabalha em cima dos outros valores vindos do split
        //}
    }
}