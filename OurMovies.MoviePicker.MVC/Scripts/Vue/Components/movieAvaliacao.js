Vue.component('movie-avaliacao', {
    props: {
        filme: {
            type: Object,
            required: true
        },
        idRef:{
            type:String,
            required: false
        }
    },
    methods: {
        validarFormaNota(){
            validarForm(this.$refs.formFilmeNota, () =>{
                this.avaliarFilme();
            });
        },
        avaliarFilme(){
            app.isLoading = true;
            this.filme.Nota = this.notaFilme;
            fazerRequest(`${window.location.origin}/api/Filmes/Avaliar`, REQUESTMETHOD.POST, this.filme).then(({ data, success, message }) => {
                if(success){
                    toastMessage(message, TOASTMETHOD.SUCCESS, 'favorite_border');
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').hide();
                }
                else{
                    toastMessage(message, TOASTMETHOD.SHOW, 'notification_important');
                }
            }).catch(err =>{
                toastMessage(err.response.data.ExceptionMessage, TOASTMETHOD.SHOW, 'notification_important');
            }).finally(() =>{
                app.isLoading = false;
            })
        }
    },
    template: `<div class="modal fade bd-example-modal-sm" :id="idRef" tabindex="-1" role="dialog"
    aria-labelledby="mySmallModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-sm">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">dê rating sem dar hate!</h4>
            </div>
            <div class="modal-body">
                <span class="text-muted">nada melhor que sinceridade nesses momentos, né?</span>
                <img src="/Content/img/winky-gif.gif" class="img-raised rounded img-fluid" alt="image background">
                <div class="row mt-3 justify-content-center">
                    <validation-observer ref="formFilmeNota">
                        <form id="formFilmeNota" name="formFilmeNota" v-on:submit.prevent="validarFormaNota"
                            class="form text-center" novalidate>
                            <span class="text-muted ">nota para "{{filme.Nome}}"</span>
                            <validation-provider vid="notaFilme" rules="required">
                                <div id="like" class="rating justify-content-center mt-2">
                                    <input type="radio" id="heart_5" name="like" v-model.number="notaFilme" value="5" />
                                    <label for="heart_5" title="Five"><i class="material-icons">favorite</i></label>
                                    <input type="radio" id="heart_4" name="like" v-model.number="notaFilme" value="4" />
                                    <label for="heart_4" title="Four"><i class="material-icons">favorite</i></label>
                                    <input type="radio" id="heart_3" name="like" v-model.number="notaFilme" value="3" />
                                    <label for="heart_3" title="Three"><i class="material-icons">favorite</i></label>
                                    <input type="radio" id="heart_2" name="like" v-model.number="notaFilme" value="2" />
                                    <label for="heart_2" title="Two"><i class="material-icons">favorite</i></label>
                                    <input type="radio" id="heart_1" name="like" v-model.number="notaFilme" value="1" />
                                    <label for="heart_1" title="One"><i class="material-icons">favorite</i></label>
                                </div>
                            </validation-provider>
                        </form>
                    </validation-observer>

                </div>

            </div>
            <div class="modal-footer justify-content-center">
                <button type="button" class="btn btn-primary btn-link btn-wd btn-lg" data-dismiss="modal">cancelar</button>
                <button type="submit" form="formFilmeNota" class="btn btn-primary btn-link btn-wd btn-lg">avaliar</button>
            </div>
        </div>
    </div>
</div>`,
    data(){
        return {
            notaFilme: Number
        }
    },
    mounted() { 
    },
    beforeDestroy() { }
});