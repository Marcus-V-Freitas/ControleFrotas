$(document).ready(function () {

    MascaraCEP();
    AJAXBuscarCEP();
    MascaraTelefone();
    MascaraCPFCNPJ();
    Enviar();
    MascaraPlaca();
    MascaraDatas();
    MascaraDinheiro();
    MascaraRenach();
    MascaraCNH();
    MascaraCarro();
    Carregamento();

});

function MascaraRenach() {
    $(".renach").mask("AA0.000.000.00");
}

function MascaraCNH() {
    $(".cnh").mask("000.000.000-00");
}

function MascaraCarro() {
    $(".renavam").mask("000.000.000.00");
}

function Carregamento() {
    $(document).ready(function () {
        $('.load').hide();
    });
}

function AJAXBuscarCEP() {
    $("#CEP").keyup(function () {
        OcultarMensagemDeErro();

        if ($(this).val().length == 10) {

            var cep = RemoverMascara($(this).val());
            $.ajax({
                type: "GET",
                url: "https://viacep.com.br/ws/" + cep + "/json/?callback=callback_name",
                dataType: "jsonp",
                error: function (data) {
                    MostrarMensagemDeErro("Opps! tivemos um erro na busca pelo CEP! Parece que os servidores estão offline!");
                },
                success: function (data) {
                    if (data.erro == undefined) {
                        $("#Estado").val(data.uf);
                        $("#Cidade").val(data.localidade);
                        $("#Endereco").val(data.logradouro);
                        $("#Bairro").val(data.bairro);
                        $("#Complemento").val(data.complemento);
                    } else {
                        MostrarMensagemDeErro("O CEP informado não existe!");
                    }

                }
            });
        }
    });
}

function Enviar() {
    $('#frm').bind('submit', function (e) {
        var button = $('#send');

        button.prop('disabled', true);

        var valid = true;

      
        if (!valid) {
            
            e.preventDefault();

          
            button.prop('disabled', false);
        }
    });
}


function RemoverMascara(valor) {
    return valor.replace(".", "").replace("-", "");
}

function MascaraDatas() {
    $(".data").mask("00/00/0000");
}

function MascaraDinheiro() {
    $('.dinheiro').mask('000.000.000.000.000,00', { reverse: true });
}

function MascaraCPFCNPJ() {
    $("#CPFCNPJ").keydown(function () {
        try {
            $("#CPFCNPJ").unmask();
        } catch (e) { }
        if ($(this).val().length < 11) {
            $("#CPFCNPJ").mask("000.000.000-00");
        } else {
            $("#CPFCNPJ").mask("00.000.000/0000-00");
        }
        var elem = this;
        setTimeout(function () {
            // mudo a posição do seletor
            elem.selectionStart = elem.selectionEnd = 10000;
        }, 0);
        // reaplico o valor para mudar o foco
        var currentValue = $(this).val();
        $(this).val('');
        $(this).val(currentValue);
    });
}

function MascaraPlaca() {
    $(".placa").mask("AAA-0000");
}

function MostrarMensagemDeErro(mensagem) {
    $(".alert-danger").css("display", "block");
    $(".alert-danger").text(mensagem);
}


function OcultarMensagemDeErro() {
    $(".alert-danger").css("display", "none");
}


function MascaraCEP() {
    $(".cep").mask("00.000-000");
}

function MascaraTelefone() {
    $(".telefone").mask("(00)00000-0000");
}




