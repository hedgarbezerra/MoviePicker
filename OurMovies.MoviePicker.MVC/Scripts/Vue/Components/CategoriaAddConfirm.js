Vue.component('categoria-add-card', {
    props: {
        callback: {
            type: Function,
            required: false
        }
    },
    methods: {
        validaFormCategoria() {
            validarForm(this.$refs.formCadastrarCategoria, () => {
                app.isLoading = true;
                this.registrarCategoria();
            });
        },
        registrarCategoria() {
            var categoriaCtx = {
                Nome: this.categoria.nome
            };

            fazerRequest(`${window.location.origin}/api/Categorias/Incluir`, REQUESTMETHOD.POST, categoriaCtx).then(({ data, success, message }) => {
                if (success) {
                    toastMessage(message, TOASTMETHOD.SUCCESS, 'check_circle_outline');
                    $(`#novaCategoriaModal`).modal('hide');

                    this.categoria = {
                        nome: ''
                    };

                    if (this.callback != undefined || this.callback != null)
                        setTimeout(() => this.callback(), 1000);
                }
                else
                    toastMessage(message, TOASTMETHOD.ERROR, 'error_outline');

            }).catch(err => {
                toastMessage(err.response.data.ExceptionMessage ?? 'Houve um problema ao registrar o filme.', TOASTMETHOD.ERROR, 'error_outline');
            })
                .finally(() => app.isLoading = false);
        }
    },
    data() {
        return {
            categoria: {
                nome: ''
            }
        }
    },
    template: `<div class="modal fade" id="novaCategoriaModal" tabindex="-1" role="dialog" aria-labelledby="novaCategoriaModal" aria-hidden="true">
    <div role="document" class="modal-dialog modal-login">
        <div class="modal-content">
            <div class="card card-signup card-plain">
                <div class="modal-header"></div>
                <div class="modal-body">                    
                    <validation-observer ref="formCadastrarCategoria">
                        <form class="form" name="formCadastrarCategoria" id="formCadastrarCategoria" v-on:submit.prevent="validaFormCategoria" novalidate>
                            <p class="description text-center">adicionar uma categoria</p>
                            <div class="card-body">
                                <validation-provider name="categoria" vid="txtNomeCategoria" rules="required" v-slot="{ errors }">
                                    <div class="form-group ">
                                        <label for="txtNomeCategoria" class="bmd-label-floating">categoria</label>                                    
                                        <input type="text" class="form-control" id="txtNomeCategoria" v-model="categoria.nome" v-bind:class="{'has-error': errors[0]}">
                                        <span class="has-error">{{ errors[0] }}</span>
                                    </div>                                
                                </validation-provider>
                            </div>
                            <div class="modal-footer justify-content-center">
                                <button type="button" class="btn btn-primary btn-link btn-wd btn-lg" data-dismiss="modal">cancelar</button>
                                <button type="submit" class="btn btn-primary btn-link btn-wd btn-lg">salvar categoria</button>
                            </div>
                        </form>
                    </validation-observer>
                </div>
            </div>
        </div>
    </div>
</div>`
})