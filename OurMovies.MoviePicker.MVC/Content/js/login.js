var app = new Vue({
    el:'#vueApp',
    data:{
        isLoading: false,
        usuario: {
            senha: '',
            login: ''
        }
    },
    methods:{
        validarLogin() {
            validarForm(this.$refs.formLogin, () => {
                this.isLoading = true;
                this.fazerLogin();
            });            
        },
        fazerLogin() {
            let usuarioLogin = {
                Usuario: this.usuario.login,
                Senha: this.usuario.senha
            };

            fazerRequest('https://localhost:44340/api/Auth/Login', REQUESTMETHOD.POST, usuarioLogin).then(res => {
                if(res.success)
                    window.location.replace('Index');
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