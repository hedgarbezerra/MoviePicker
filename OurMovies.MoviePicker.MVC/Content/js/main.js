moment.locale('pt-br');
VeeValidate.localize('pt_br', localeVee);
VeeValidate.setInteractionMode('eager');
Vue.component('ValidationProvider', VeeValidate.ValidationProvider);
Vue.component('ValidationObserver', VeeValidate.ValidationObserver);

const REQUESTMETHOD = Object.freeze({ "GET": 1, "POST": 2, "PUT": 3, "DELETE": 4 });
const TOASTMETHOD = Object.freeze({ "ERROR": 1, "SHOW": 2, "SUCCESS": 3, "INFO": 4 });
const toastOptions = {
    position: 'bottom-right',
    duration: 3000,
    keepOnHover: true,
    iconPack: 'material',
    theme: 'bubble',
    type: 'info'
};

function focarEl(el = HTMLDocument) {
    el.scrollIntoView({
        behavior: 'smooth',
        block: 'center'
    });
}


function toastMessage(mensagem = '', tipoToast = TOASTMETHOD.INFO, icon = 'help_outline', action = []) {
    if (tipoToast == 1) {
        Vue.toasted.error(mensagem, {
            icon,
            action
        });
    }
    else if (tipoToast == 2) {
        Vue.toasted.show(mensagem, {
            icon,
            action
        });
    }
    else if (tipoToast == 3) {
        Vue.toasted.success(mensagem, {
            icon,
            action
        });
    }
    else if (tipoToast == 4) {
        Vue.toasted.info(mensagem, {
            icon,
            action
        })
    }
}


function validarForm(form, callback = undefined) {
    form.validate().then(success => {
        if (!success) {
            const error = Object.entries(form.refs)
                .map(([key, value]) => ({
                    key, value
                })).filter(err => {
                    if (!err || !err.value || !err.value.failedRules) return false;
                    return Object.keys(err.value.failedRules).length > 0;
                });

            if (error && error.length > 0)
                focarEl(form.refs[error[0]['key']].$el);
        }
        else {
            if (callback != undefined)
                callback();
        }
    })
}

//retorna Promise
async function fazerRequest(url = "", metodo = REQUESTMETHOD, dados = Object) {
    var dados;

    if (metodo == 1) {
        await axios.get(url).then(res => dados = res.data)

    }
    else if (metodo == 2) {
        await axios.post(url, dados).then(res => dados = res.data)

    }
    return dados;
}

function validarCPF(cpf) {
    cpf = cpf.replace(/[^\d]+/g, '');
    if (cpf == '') return false;
    // Elimina CPFs invalidos conhecidos
    if (cpf.length != 11 ||
        cpf == "00000000000" ||
        cpf == "11111111111" ||
        cpf == "22222222222" ||
        cpf == "33333333333" ||
        cpf == "44444444444" ||
        cpf == "55555555555" ||
        cpf == "66666666666" ||
        cpf == "77777777777" ||
        cpf == "88888888888" ||
        cpf == "99999999999")
        return false;
    // Valida 1o digito
    add = 0;
    for (i = 0; i < 9; i++)
        add += parseInt(cpf.charAt(i)) * (10 - i);
    rev = 11 - (add % 11);
    if (rev == 10 || rev == 11)
        rev = 0;
    if (rev != parseInt(cpf.charAt(9)))
        return false;
    // Valida 2o digito
    add = 0;
    for (i = 0; i < 10; i++)
        add += parseInt(cpf.charAt(i)) * (11 - i);
    rev = 11 - (add % 11);
    if (rev == 10 || rev == 11)
        rev = 0;
    if (rev != parseInt(cpf.charAt(10)))
        return false;
    return true;
}

function validarCNPJ(cnpj) {

    cnpj = cnpj.replace(/[^\d]+/g, '');

    if (cnpj == '') return false;

    if (cnpj.length != 14)
        return false;

    // Elimina CNPJs invalidos conhecidos
    if (cnpj == "00000000000000" ||
        cnpj == "11111111111111" ||
        cnpj == "22222222222222" ||
        cnpj == "33333333333333" ||
        cnpj == "44444444444444" ||
        cnpj == "55555555555555" ||
        cnpj == "66666666666666" ||
        cnpj == "77777777777777" ||
        cnpj == "88888888888888" ||
        cnpj == "99999999999999")
        return false;

    // Valida DVs
    tamanho = cnpj.length - 2
    numeros = cnpj.substring(0, tamanho);
    digitos = cnpj.substring(tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(0))
        return false;

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(1))
        return false;

    return true;

}

function customValidators() {
    VeeValidate.extend('dates', {
        validate: (value) => {
            let inputDate = moment(value);
            let today = moment();

            return inputDate.isBefore(today) && inputDate.isValid()
        },
        message: `The date must be valid and before ${moment().format('YYYY-MM-DD')}`
    });

    VeeValidate.extend('different', {
        params: ['otherInput'],
        validate: (value, { otherInput }) => {
            if (!!!otherInput)
                return true;
            return value.id != otherInput.id;
        },
        message: `The inputs must be different.`
    });
}