var app = new Vue({
    el: '#vueApp',
    data: {
        isLoading: false,
        usuario: {
            senha: '',
            login: ''
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
                if(res.success)
                    window.location.replace('Login');
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