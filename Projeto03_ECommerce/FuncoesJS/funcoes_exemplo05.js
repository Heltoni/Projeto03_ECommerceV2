$(document).ready(function () {
    //Array de Estados
    var estados = [
        { id: 1, estado: "SP" },
        { id: 2, estado: "RJ" },
        { id: 3, estado: "MG" },
        { id: 4, estado: "BA" }
    ];

    //Array de Cidades
    var cidades = [
        { id: 1, idestado: 1, cidade: "CAMPINAS" },
        { id: 2, idestado: 1, cidade: "SOROCABA" },
        { id: 3, idestado: 2, cidade: "NITEROI" },
        { id: 4, idestado: 2, cidade: "CABO FRIO" },
        { id: 5, idestado: 2, cidade: "ANGRA" },
        { id: 6, idestado: 3, cidade: "BELO HORIZONTE" },
        { id: 7, idestado: 3, cidade: "BETIM" },
        { id: 8, idestado: 3, cidade: "EXTREMA" },
        { id: 9, idestado: 4, cidade: "SALVADOR" },
        { id: 10, idestado: 4, cidade: "PORTO SEGURO" }
    ];

    //listando os estados
    $.each(estados, function (indice, item) {
        $("#estado").append($("<option>", {
            value: item.id,
            text: item.estado
        }));
    });

    //listando as cidades
    $("#estado").change(function () {
        var idestado = $(this).val();

        var cidadesFiltradas = $.grep(cidades, function (elemento) {
            return elemento.idestado == idestado;
        });

        $("#cidade").html("<option>Selecione a cidade</option>");
        $.each(cidadesFiltradas, function (indice, item) {
            $("#cidade").append($("<option>", {
                value: item.id,
                text: item.cidade
            }));
        });
    });
});