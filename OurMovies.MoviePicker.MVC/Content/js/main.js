moment.locale('pt-br');
Vue.use(VueInfiniteLoading);
VeeValidate.localize('pt_br', localeVee);
VeeValidate.setInteractionMode('eager');
Vue.component('ValidationProvider', VeeValidate.ValidationProvider);
Vue.component('ValidationObserver', VeeValidate.ValidationObserver);
Vue.config.devtools = true;

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

function focarEl(el = HTMLDocument){
    el.scrollIntoView({
        behavior: 'smooth',
        block: 'center'
    });
}


function toastMessage(mensagem = '', tipoToast = TOASTMETHOD.INFO, icon = 'help_outline', action = []){
    if(tipoToast == 1){
        Vue.toasted.error(mensagem, {
            icon,
            action
        });
    }
    else if(tipoToast == 2){
        Vue.toasted.show(mensagem, {
            icon,
            action
        });
    }
    else if(tipoToast == 3){
        Vue.toasted.success(mensagem, {
            icon,
            action            
        });
    }
    else if(tipoToast == 4){
        Vue.toasted.info(mensagem, {
            icon,
            action
        })
    }
}


function validarForm(form, callback = undefined){
    form.validate().then(success =>{
        if(!success){
            const error = Object.entries(form.refs)
            .map(([key, value]) => ({
                key, value
            })).filter(err => {
                if(!err || !err.value || !err.value.failedRules) return false;
                return Object.keys(err.value.failedRules).length > 0;
            }); 
            
            if(error && error.length > 0)
                focarEl(form.refs[error[0]['key']].$el);
        }
        else{
            if(callback!= undefined)
                callback();
        }
    })
}

//retorna Promise
async function  fazerRequest(url = "", metodo = REQUESTMETHOD, dados = Object) {
    var dados;

    if(metodo == 1){
        await axios.get(url).then(res => dados = res.data)

    }
    else if(metodo == 2){
       await  axios.post(url, dados).then(res => dados = res.data)

    }
    return dados;
}