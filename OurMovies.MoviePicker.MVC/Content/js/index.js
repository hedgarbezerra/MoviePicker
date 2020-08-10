var app = new Vue({
    el: '#vueApp',
    data: {
        isLoading: false,
        listaFilmes: [],
        searchTerm: '',
        searchAssistidos: false,
        searchCategoria:'',
        categorias: []
    },
    methods: {
        fazerRequestFilmes(){
            this.isLoading = true;
            fazerRequest(`${window.location.origin}/api/Filmes/Listar`, REQUESTMETHOD.GET)
                .then(({data, success, message}) =>{
                    if(success){
                        this.isLoading = false;
                        toastMessage(message, TOASTMETHOD.SUCCESS, 'video_library');
                        this.listaFilmes = data;
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
        },
        diferencaDias(data){
            return moment(data).diff(moment(), 'days')
        },
        carregarCategorias(){
            this.isLoading = true;
            fazerRequest(`${window.location.origin}/api/Categorias/Listar`, REQUESTMETHOD.GET).then(({ data, success, message}) => {
                if(success)
                    this.categorias = data;                     
            }).catch(err => {
                setTimeout(() => {
                    this.carregarCategorias()
                }, 2000)
            }).finally(() =>{                
                this.isLoading = false;
            });
        }
    },
    computed:{        
        listaFilmesFiltrados(){
            var filmesFiltrados = this.listaFilmes.filter(filme => filme.Nome.toLowerCase().search(this.searchTerm.toLowerCase()) >= 0);
            
            if(this.searchAssistidos)
                filmesFiltrados = filmesFiltrados.filter(filme => filme.Assistido);
            
            if(this.searchCategoria != '')
                filmesFiltrados = filmesFiltrados.filter(x => x.Categorias.some(c => c.Id == app.searchCategoria));

            return filmesFiltrados;
        }
    },
    created(){        
        Vue.use(Toasted, toastOptions);
    },
    mounted(){
        this.fazerRequestFilmes(); 
        this.carregarCategorias();
    }
});