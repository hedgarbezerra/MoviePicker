Vue.component('movie-add-card', {
    props:{
        callback:{
            type: Function,
            required: false
        }
    },
    methods:{
        validaFormFilme(){
            validarForm(this.$refs.formCadastrarFilme, () => {
                app.isLoading = true;
                this.registrarFilme();
            });    
        },
        registrarFilme(){
            var filmeCtx = {
                Nome: this.filme.nome,
                Descricao: this.filme.descricao,
                Categorias: this.filme.categorias.map(x => this.categorias.find(y => y.Id == x))
            };

            fazerRequest('https://localhost:44340/api/Filmes/Incluir', REQUESTMETHOD.POST, filmeCtx).then(({ data, success, message}) => {
                if(success){
                    toastMessage(message, TOASTMETHOD.SUCCESS, 'check_circle_outline');                    
                    this.callback();
                }
                else
                    toastMessage(message, TOASTMETHOD.ERROR, 'error_outline');
                
            }).catch(err => {
                toastMessage('Não foi possível adiciona este filme no momento.', TOASTMETHOD.ERROR, 'error_outline');
            })
            .finally(() => app.isLoading = false);
        },
        carregarCategorias(){

            fazerRequest('https://localhost:44340/api/Categorias/Listar', REQUESTMETHOD.GET).then(({ data, success, message}) => {
                if(success)
                    this.categorias = data;                
                else
                    setTimeout(() => {
                        this.carregarCategorias()
                    }, 2000); 
                
                app.isLoading = false;
            }).catch(err => {
                app.isLoading = false;
                setTimeout(() => {
                    this.carregarCategorias()
                }, 2000)
            });
        }
    },
    data(){
        return {
            filme:{
                nome: '',
                descricao: '',
                categorias: []
            },
            categorias: []
        }
    },
    template:`
    <div class="modal fade" id="novoFilmeModal" tabindex="-1" role="dialog" aria-labelledby="novoFilmeModal" aria-hidden="true">
        <div role="document" class="modal-dialog modal-login">
            <div class="modal-content">
                <div class="card card-signup card-plain">
                    <div class="modal-header"></div>
                    <div class="modal-body">
                        
                        <validation-observer ref="formCadastrarFilme">
                            <form class="form" name="formCadastrarFilme" id="formCadastrarFilme" v-on:submit.prevent="validaFormFilme" novalidate>
                                <p class="description text-center">Bota um filme ai!</p>
                                <div class="card-body">
                                    <validation-provider name="filme" vid="txtNome" rules="required" v-slot="{ errors }">
                                        <div class="form-group ">
                                            <label for="txtNome" class="bmd-label-floating">nome do filme</label>                                    
                                            <input type="text" class="form-control" id="txtNome" v-model="filme.nome"  v-bind:class="{'has-error': errors[0]}">
                                            <span class="has-error">{{ errors[0] }}</span>
                                        </div>                                
                                    </validation-provider>

                                    <div class="form-group">
                                        <label for="txtDescricao" class="bmd-label-floating">descrição</label> 
                                        <textarea class="form-control" id="txtDescricao" v-model="filme.descricao" rows="2"></textarea>
                                    </div>

                                    <div class="form-group">
                                        <label for="cmbCategorias">categorias</label>
                                        <select multiple="true" v-model="filme.categorias" class="form-control selectpicker" data-style="btn btn-link" id="cmbCategorias">
                                            <option v-for="categoria in categorias" :key="categoria.Id" v-bind:value="categoria.Id">{{categoria.Nome}}</option>
                                        </select>
                                    </div> 
                                </div>
                                <div class="modal-footer justify-content-center">
                                    <button type="button" class="btn btn-primary btn-link btn-wd btn-lg" data-dismiss="modal">cancelar</button>
                                    <button type="submit" class="btn btn-primary btn-link btn-wd btn-lg">salvar filme</button>
                                </div>
                            </form>
                        </validation-observer>
                    </div>
                </div>
            </div>
        </div>
    </div>
    `,
    mounted(){
        $('#novoFilmeModal').modal({
            backdrop: true,
            keyboard: true,
            focus:true,
            show: false
        });
        this.carregarCategorias();

    },
    beforeDestroy(){

    }
})