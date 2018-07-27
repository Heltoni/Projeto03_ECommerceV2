function exibirValores() {
    //obtendo os valores dos campos de entrada
    var vCodigo = document.getElementById("codigo").value;
    var vDescricao = document.getElementById("descricao").value;
    var vPreco = document.getElementById("preco").value;

    //convertendo vPreco para double
    vPreco = parseFloat(vPreco);

    var erro1, erro2;
    if (vPreco === "0") {
        erro1 = "Comparando texto: não pode ser zero";
    }

    if (vPreco === 0) {
        erro2 = "Comparando numero: não pode ser zero";
    }

    //gerando uma resposta
    var resposta = "Código: " + vCodigo +
        "<br/>Descrição: " + vDescricao +
        "<br/>Preço: " + vPreco +
        "<br/>Erro1: " + erro1 +
        "<br/>Erro2: " + erro2;

    //exibindo a resposta em uma caixa de mensagens
    //alert(resposta);

    //exibindo a resposta na div
    var vResultado = document.getElementById("resultado");
    vResultado.innerHTML = resposta;
}

var botao = document.getElementById("btnExibir");
botao.addEventListener("click", exibirValores);