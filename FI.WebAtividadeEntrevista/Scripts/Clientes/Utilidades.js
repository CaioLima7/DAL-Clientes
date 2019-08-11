$(document).ready(function () {
    $('.data').mask('00/00/0000');
    $('.tempo').mask('00:00:00');
    $('.data_tempo').mask('00/00/0000 00:00:00');
    $('.cep').mask('00000-000');
    $('.tel').mask('(00) 0 0000-0000');
    $('.ddd_tel').mask('(00) 0000-0000');
    $('.cpf').mask('000.000.000-00');
    $('.cnpj').mask('00.000.000/0000-00');
    $('.dinheiro').mask('000.000.000.000.000,00', { reverse: true });
    $('.dinheiro2').mask("#.##0,00", { reverse: true });
    $('.rg').mask("00.000.00-0");
});


$(document).ready(function () {
    chamarGrid();
})

function chamarGrid() {
    $("#gridClientesBeneficiarios").bootgrid({
        ajax: true,
        post: function () {
            return {
                id: "b0df282a-0d67-40e5-8558-c9e93b7befed"
            };
        },
        url: "/Cliente/ListarBeneficiarios",
        formatters: {
            "commands": function (column, row) {
                return "<button type=\"button\" class=\"btn btn-xs btn-default command-edit\" data-row-id=\"" + row.id + "\"><span class=\"glyphicon-pencil\"></span></button> " +
                    "<button type=\"button\" class=\"btn btn-xs btn-default command-delete\" data-row-id=\"" + row.id + "\"><span class=\"glyphicon glyphicon-trash\"></span></button>";
            }
        }
    }).on("loaded.rs.jquery.bootgrid", function () {
        $("#gridClientesBeneficiarios-header").remove();
        $(".infos").remove();

        $(".command-edit").on("click", function (e) {

        });
        $(".command-delete").on("click", function (e) {
            debugger;
            var cpfBeneficiario = $(this).closest('tr').children('td').first().text();
            $.ajax({
                method: "POST",
                url: '/Cliente/RemoverBeneficiario',
                data: {
                    CPF: cpfBeneficiario
                },
                error: function (data) {
                    console.log("Falhou!");
                },
                success: function (data) {
                    console.log("Sucesso!");
                    $('#modalExemplo').modal('hide')
                    location.reload();
                }
            });
        });
    })
}