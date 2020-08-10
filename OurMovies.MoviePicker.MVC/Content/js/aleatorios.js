var app = new Vue({
    el: '#vueApp',
    data:{
        isLoading: false,
        filmeAleatorio: null,
        categoriasSelecionadas: [],
        categorias: []
    },
    methods:{
        carregarFilmeAleatorio(){
            this.isLoading = true;
            let categoriasPesquisa = this.categoriasSelecionadas.length > 0 ? this.categoriasSelecionadas.map(x => this.categorias.find(y => y.Id == x)) : [];

            fazerRequest(`${window.location.origin}/api/Filmes/FilmeAleatorio`,REQUESTMETHOD.POST, categoriasPesquisa)
                .then(({data, success, message}) => {
                    if(success){
                        this.filmeAleatorio = data;
                        toastMessage(message, TOASTMETHOD.SUCCESS, 'shuffle');
                    }
                    else
                        toastMessage(message, TOASTMETHOD.SHOW, 'notification_important');

                })
                .catch(err => {
                    toastMessage('Houve um problema ao encontrar um filme aleatório.', TOASTMETHOD.ERROR);
                })
                .finally(() => {
                this.isLoading = false;
            })
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
    created(){        
        Vue.use(Toasted, toastOptions);
    },
    mounted(){
        this.carregarCategorias();
    }
});