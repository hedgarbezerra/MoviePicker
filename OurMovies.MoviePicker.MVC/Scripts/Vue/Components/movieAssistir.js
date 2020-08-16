Vue.component('movie-assistir', {
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
        assistirFilme() {
            app.isLoading = true;
            fazerRequest(`${window.location.origin}/api/Filmes/Assistir`, REQUESTMETHOD.POST, this.filme).then(({ data, success, message }) => {
                toastMessage(message, TOASTMETHOD.SUCCESS, 'play_circle_outline');
                this.filme.Assistido = true;
            }).catch(err => {
                toastMessage(err.response.data.ExceptionMessage ?? 'Houve um problema ao assistir o filme.', TOASTMETHOD.SHOW, 'notification_important');
            }).finally(() => {
                app.isLoading = false;
            })
        }
    },
    template: `
    <div class="modal fade bd-example-modal-sm" :id="idRef" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <h4 class="modal-title">deseja realmente assistir "{{filme.Nome}}"?</h4>
        </div>
        <div class="modal-body">
            <span class="text-muted">pense bem...bom, você quem sabe...</span>
            <img src="/Content/img/shame-gif.gif" class="img-raised rounded img-fluid" alt="image background">
        </div>
        <div class="modal-footer justify-content-center">
            <button type="button" class="btn btn-primary btn-link btn-wd btn-lg" data-dismiss="modal">cancelar</button>
            <button type="button" class="btn btn-primary btn-link btn-wd btn-lg" data-dismiss="modal" v-on:click="assistirFilme">assistir filme</button>
        </div>
      </div>
    </div>
</div>`
});