Vue.component('recuperar-senha', {
    methods: {
        validaFormRecuperacao() {
            validarForm(this.$refs.formRecuperarSenha, () => {
                app.isLoading = true;
                this.recuperarSenha();
            });
        },
        recuperarSenha() {
            var usuarioRecuperacao = {
                Nome: this.contato.nome,
                Usuario: this.contato.usuario,
                Contato: this.contato.contato
            };

            fazerRequest(`${window.location.origin}/api/Auth/RecuperarSenha`, REQUESTMETHOD.POST, usuarioRecuperacao).then(({ data, success, message }) => {
                if (success) {
                    toastMessage(message, TOASTMETHOD.SUCCESS, 'check_circle_outline');
                    $(`#recuperarSenha`).modal('hide');

                    this.contato = {
                        nome: '',
                        contato: '',
                        usuario: ''
                    };
                }
                else
                    toastMessage(message, TOASTMETHOD.ERROR, 'error_outline');

            }).catch(err => {
                toastMessage(err.response.data.ExceptionMessage == undefined ? 'Houve um problema ao registrar o filme.' : err.response.data.ExceptionMessage, TOASTMETHOD.ERROR, 'error_outline');
            })
                .finally(() => app.isLoading = false);
        }
    },
    data() {
        return {
            contato: {
                nome: this.user != undefined ? this.user : '',
                contato: '',
                usuario: ''
            }
        }
    },
    template: `
    <div class="modal fade" id="recuperarSenha" tabindex="-1" role="dialog" aria-labelledby="recuperarSenha" aria-hidden="true">
        <div role="document" class="modal-dialog modal-login">
            <div class="modal-content">
                <div class="card card-signup card-plain">
                    <div class="modal-header"></div>
                    <div class="modal-body">
                        
                        <validation-observer ref="formRecuperarSenha">
                            <form class="form" name="formRecuperarSenha" id="formRecuperarSenha" v-on:submit.prevent="validaFormRecuperacao" novalidate>
                                <p class="description text-center">Bota um filme ai!</p>
                                <div class="card-body">
                                    <validation-provider name="nome" vid="txtNome" rules="required" v-slot="{ errors }">
                                        <div class="form-group ">
                                            <label for="txtNome" class="bmd-label-floating">nome</label>                                    
                                            <input type="text" class="form-control" id="txtNome" v-model="contato.nome"  v-bind:class="{'has-error': errors[0]}">
                                            <span class="has-error">{{ errors[0] }}</span>
                                        </div>                                
                                    </validation-provider>

                                    <validation-provider name="usuário" vid="txtUsuario" rules="required" v-slot="{ errors }">
                                        <div class="form-group ">
                                            <label for="txtUsuario" class="bmd-label-floating">usuário</label>                                    
                                            <input type="text" class="form-control" id="txtUsuario" v-model="contato.usuario"  v-bind:class="{'has-error': errors[0]}">
                                            <span class="has-error">{{ errors[0] }}</span>
                                        </div>                                
                                    </validation-provider>

                                    <validation-provider name="e-mail" vid="txtContato" rules="max:255" v-slot="{ errors }">
                                        <div class="form-group">
                                            <label for="txtContato" class="bmd-label-floating">e-mail</label> 
                                            <input type="text" class="form-control" id="txtContato" v-model="contato.contato" v-bind:class="{'has-error': errors[0]}"></input>
                                            <span class="has-error">{{ errors[0] }}</span>
                                        </div>
                                    </validation-provider>
                                    
                                </div>
                                <div class="modal-footer justify-content-center">
                                    <button type="button" class="btn btn-primary btn-link btn-wd btn-lg" data-dismiss="modal">cancelar</button>
                                    <button type="submit" class="btn btn-primary btn-link btn-wd btn-lg">recuperar</button>
                                </div>
                            </form>
                        </validation-observer>
                    </div>
                </div>
            </div>
        </div>
    </div>
    `
})