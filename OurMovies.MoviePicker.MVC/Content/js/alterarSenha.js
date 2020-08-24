var app = new Vue({
    el: '#vueApp',
    data: {
        isLoading: false,
        verSenha: false,
        verConfirmacao: false,
        usuario: {
            senha: '',
            confirmacaoSenha: ''
        }
    },
    methods: {
        validarNovaSenha() {
            validarForm(this.$refs.formNovaSenha, () => {
                this.alterarSenha();
                this.isLoading = true;
            });
        },
        alterarSenha() {
            let usuario = {
                Senha: this.usuario.senha
            };

            fazerRequest(`${window.location.origin}/api/Auth/AtualizarSenha`, REQUESTMETHOD.POST, usuario).then(({ data, success, message }) => {
                if (success) {
                    toastMessage(message, TOASTMETHOD.SUCCESS, 'check_circle_outline');
                    setTimeout(() => window.location.replace('Login'), 3000)
                }
                else
                    toastMessage(message, TOASTMETHOD.ERROR, 'error_outline');

                this.isLoading = false;
            }).catch(err => {
                this.isLoading = false;
                toastMessage(err.response.data.ExceptionMessage == undefined ? 'Não foi possível concluir seu cadastro.' : err.response.data.ExceptionMessage, TOASTMETHOD.ERROR, 'error_outline')
            });
        }
    },
    created() {
        Vue.use(Toasted, toastOptions);
    }
});