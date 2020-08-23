var app = new Vue({
    el: '#vueApp',
    data:{
        isLoading: false,
        filme:{
            nome: '',
            descricao: '',
            categorias: []
        },
        listaCategorias: [],
        listaFilmesAdicionar: []
    },
    methods:{
        validaFormFilme(){
            validarForm(this.$refs.formCadastrarFilme, () => {
                this.adicionarFilmeLista(this.filme);
            });    
        },
        carregarCategorias(){
            this.isLoading = true;
            fazerRequest(`${window.location.origin}/api/Categorias/Listar`, REQUESTMETHOD.GET).then(({ data, success, message}) => {
                if(success)
                    this.listaCategorias = data;                
                else
                    setTimeout(() => {
                        this.carregarCategorias()
                    }, 2000); 
                
            }).catch(err => {
                setTimeout(() => {
                    this.carregarCategorias()
                }, 2000)
            }).finally(() =>{                
                this.isLoading = false;
            });
        },
        cadastrarFilmes(){
            this.isLoading = true;
            fazerRequest(`${window.location.origin}/api/Filmes/IncluirDiversos`, REQUESTMETHOD.POST, this.listaFilmesAdicionar)
            .then(({data, success, message}) =>{
                if (success) {
                    toastMessage(message, TOASTMETHOD.SUCCESS, 'check_circle_outline');
                    this.listaFilmesAdicionar = [];
                    this.limparForm();
                }
                elses
                    toastMessage(message, TOASTMETHOD.ERROR, 'error_outline');

            }).catch(err => {
                toastMessage(err.response.data.ExceptionMessage == undefined ? 'Não foi possível adicionar os filmes no momento.' : err.response.data.ExceptionMessage, TOASTMETHOD.ERROR, 'error_outline');
            })
            .finally(() =>{
                this.isLoading = false;
            });
        },
        limparForm(){
            this.filme = {
                nome: '',
                descricao: '',
                categorias: []
            }
        },
        validarFormFilme(){},
        adicionarFilmeLista(filme){
            let filmeCtx = {
                Nome: filme.nome,
                Descricao: filme.descricao,
                Categorias: this.filme.categorias.map(x => this.listaCategorias.find(y => y.Id == x))
            };

            this.listaFilmesAdicionar.push(filmeCtx);
            this.limparForm();
        },
        removerFilmeLista(index){
            this.listaFilmesAdicionar.splice(index, 1);
        }
    },
    created(){        
        Vue.use(Toasted, toastOptions);
    },
    mounted(){
        this.carregarCategorias();
    }
});