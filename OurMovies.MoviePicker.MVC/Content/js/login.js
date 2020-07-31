var app = new Vue({
    el:'#vueApp',
    data:{
        isLoading: false,
        verSenha: false,
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

            fazerRequest('https://localhost:44340/api/Auth/Login', REQUESTMETHOD.POST, usuarioLogin).then(({ data, success, message}) => {
                if(success){
                    toastMessage(message, TOASTMETHOD.SUCCESS, 'check_circle_outline');
                    setTimeout(()=> window.location.replace('Index'), 2500)
                }
                else
                    toastMessage(message, TOASTMETHOD.ERROR, 'error_outline');
                
                this.isLoading = false;
            }).catch(err => {
                this.isLoading = false;
                toastMessage('Não foi possível conectar você no momento.', TOASTMETHOD.ERROR, 'error_outline')
            });
        }
    }
});