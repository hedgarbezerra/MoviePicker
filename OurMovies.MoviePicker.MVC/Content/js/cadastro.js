var app = new Vue({
    el: '#vueApp',
    data: {
        isLoading: false,
        usuario: {
            senha: '',
            login: '',
            confirmacaoSenha: ''
        }
    },
    methods: {
        validarCadastro() {
            validarForm(this.$refs.formCadastro, () => {
                this.fazerCadastro();
                this.isLoading = true;
            });
        },
        fazerCadastro() {
            let usuarioLogin = {
                Usuario: this.usuario.login,
                Senha: this.usuario.senha
            };
           
            fazerRequest('https://localhost:44340/api/Auth/Cadastrar', REQUESTMETHOD.POST, usuarioLogin).then(res => {
                if(res.success){
                    toastMessage(res.message, TOASTMETHOD.SUCCESS, 'check_circle_outline');
                    setTimeout(()=> window.location.replace('Login'), 2500)
                }
                else
                    toastMessage(res.message, TOASTMETHOD.ERROR, 'error_outline');
                    
                this.isLoading = false;
            }).catch(err => {
                this.isLoading = false;
                toastMessage('Não foi possível cadastrar você no momento.', TOASTMETHOD.ERROR, 'error_outline')
            });
        }
    }
});