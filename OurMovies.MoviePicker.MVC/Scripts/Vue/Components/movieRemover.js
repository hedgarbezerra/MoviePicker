Vue.component('movie-remover', {
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
        removerFilme() {
            app.isLoading = true;
            fazerRequest(`${window.location.origin}/api/Filmes/Remover`, REQUESTMETHOD.POST, this.filme).then(({ data, success, message }) => {
                toastMessage(message, TOASTMETHOD.SUCCESS, 'play_circle_outline');
                this.removerFilmeMain();
            }).catch(err => {
                toastMessage(err.response.data.ExceptionMessage == undefined ? 'Não foi possível remover o filme selecionado.' : err.response.data.ExceptionMessage, TOASTMETHOD.SHOW, 'notification_important');
            }).finally(() => {
                app.isLoading = false;
            })
        },
        removerFilmeMain(){
            let index = app.listaFilmes.findIndex(x => x.Id == this.filme.Id);
            app.listaFilmes.splice(index, 1);
        }
    },
    template: `
    <div class="modal fade bd-example-modal-sm" :id="idRef" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <h4 class="modal-title">deseja realmente remover "{{filme.Nome}}"?</h4>
        </div>
        <div class="modal-body">
            <span class="text-muted">pense bem...bom, você quem sabe...</span>
            <img src="/Content/img/shame-gif.gif" class="img-raised rounded img-fluid" alt="image background">
        </div>
        <div class="modal-footer justify-content-center">
            <button type="button" class="btn btn-primary btn-link btn-wd btn-lg" data-dismiss="modal">cancelar</button>
            <button type="button" class="btn btn-primary btn-link btn-wd btn-lg" data-dismiss="modal" v-on:click="removerFilme">remover filme</button>
        </div>
      </div>
    </div>
</div>`
});