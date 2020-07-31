var app = new Vue({
    el: '#vueApp',
    data: {
        isLoading: false,
        listaFilmes: [],
        listaFilmesFiltrados: []
    },
    methods: {
        fazerRequestFilmes(){
            this.isLoading = true;
            fazerRequest('https://localhost:44340/api/Filmes/Listar', REQUESTMETHOD.GET)
                .then(({data, success, message}) =>{
                    if(success){
                        this.isLoading = false;
                        toastMessage(message, TOASTMETHOD.SUCCESS, 'video_library');
                        this.listaFilmes = data;
                        this.listaFilmesFiltrados = data;
                    }
                    else{
                        toastMessage(message, TOASTMETHOD.SHOW, 'notification_important');
                    }
            }).catch(err =>{
                this.isLoading = false;
                toastMessage('Houve um erro ao carregar os filmes, carregando novamente na força do ódio...', TOASTMETHOD.ERROR);

                setTimeout(() =>{
                    fazerRequestFilmes();
                }, 2000);
            })
        }
    },
    created(){        
        Vue.use(Toasted, toastOptions);
    },
    mounted(){
        this.fazerRequestFilmes(); 
    }
});