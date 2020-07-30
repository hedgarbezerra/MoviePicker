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
                    app.$refs.formLogin.refs.confirmacao.setErrors([res.message]);
                    
                this.isLoading = false;
            });
        }
    },
    watch:{
        'usuario.senha':function(newVal, oldVal){
            if(newVal!= oldVal)
                app.$refs.formLogin.refs.confirmacao.setErrors([]);
        },
        'usuario.usuario':function(newVal, oldVal){
            if(newVal!= oldVal)
                app.$refs.formLogin.refs.confirmacao.setErrors([]);
        }
    }    
});