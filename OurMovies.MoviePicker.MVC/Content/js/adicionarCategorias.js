var app = new Vue({
    el: '#vueApp',
    data:{
        isLoading: false,
        categoria:{
            nome: ''
        },
        listaCategoriasAdicionar: [],
        listaCategoriasExistentes:[]
    },
    methods:{
        validaFormFilme(){
            validarForm(this.$refs.formCadastrarCategoria, () => {
                this.adicionarCategoriaLista(this.categoria);
            });    
        },
        cadastrarCategorias(){
            this.isLoading = true;
            fazerRequest(`${window.location.origin}/api/Categorias/IncluirDiversas`, REQUESTMETHOD.POST, this.listaCategoriasAdicionar)
            .then(({data, success, message}) =>{
                if (success) {
                    toastMessage(message, TOASTMETHOD.SUCCESS, 'check_circle_outline');
                    this.listaCategoriasAdicionar = [];
                    this.limparForm();
                }
                else
                    toastMessage(message, TOASTMETHOD.ERROR, 'error_outline');

            }).catch(err => {
                toastMessage('Não foi possível adicionar as categorias no momento.', TOASTMETHOD.ERROR, 'error_outline');
            })
            .finally(() =>{
                this.isLoading = false;
            });
        },
        limparForm(){
            this.categoria = {
                nome: ''
            }
        },
        adicionarCategoriaLista(categoria){
            let categoriaCtx = {
                Nome: categoria.nome
            };

            this.listaCategoriasAdicionar.push(categoriaCtx);
            this.limparForm();
        },
        removerCategoriaLista(index = Number){
            this.listaCategoriasAdicionar.splice(index, 1);
        }
    },
    created(){        
        Vue.use(Toasted, toastOptions);
    }
});