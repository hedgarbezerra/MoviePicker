Vue.component('movie-card', {
    props: {
        filme: {
            type: Object,
            required: true
        }
    },
    methods: {
        diferencaDias(data){
            return moment(data).diff(moment(), 'days')
        }
    },
    template:`
    <div class="row" >
    <div class="col-sm-10 offset-sm-1 col-lg-8 offset-lg-2">
        <div class="card">
            <div class="card-header card-header-rose">
                <div class="card-icon">
                    <i class="material-icons" v-if="diferencaDias(filme.DtAdicionado) >= -2">new_releases</i>
                    <i class="material-icons" v-if="filme.Nota > 3">favorite</i>
                    <i class="material-icons">hd</i>
                    <i class="material-icons" v-if="filme.Assistido">visibility</i>
                </div>
            </div>
            <div class="card-body text-center">
                <h4> {{ filme.Nome }}</h4>
                <p class="card-text category"><strong>categoria(s):</strong>
                        <span v-if="filme.Categorias.length > 0">{{  filme.Categorias | categorias-concat }}</span>
                        <span v-else>esse filme não possuí categorias</span>
                    </p>
                <p class="card-text" v-if="!!filme.Descricao"><strong>descrição:</strong> {{ filme.Descricao }}</p>   

                <div class="text-center card-avaliacao" v-if="filme.Nota > 0">
                    <h4>avaliação:</h4>
                    <i class="material-icons" v-for="i in filme.Nota">favorite</i>
                </div>                    
                <div class="text-center" v-else> 
                    <h4>filme não avaliado</h4>
                </div>
                <br/>      
                <button type="button" class="btn btn-primary btn-round"  data-toggle="modal" :data-target="targetModalAssistir" v-bind:data-filme="filme.Id" v-bind:disabled="filme.Assistido">
                    <i class="material-icons">play_circle_outline</i>
                    assistir
                </button>
                <button  type="button" class="btn btn-primary btn-round"  data-toggle="modal" :data-target="targetModalAvaliacao" v-bind:data-filme="filme.Id" v-bind:disabled="filme.Nota > 0 || !filme.Assistido">
                    <i class="material-icons">edit</i>avaliar
                </button> 
                <button type="button" class="btn btn-primary btn-round"  data-toggle="modal" :data-target="targetModalRemover" v-bind:data-filme="filme.Id" v-bind:disabled="filme.Assistido">
                    <i class="material-icons">clear</i>
                    remover
                </button>  

                <div class="card-footer text-muted"> {{ filme.DtAdicionado | from-now}} </div>
            </div>
        </div>
    </div>  
    <movie-avaliacao v-if="filme.Assistido && filme.Nota <= 0" v-bind:idRef="idModalAvaliacao" v-bind:filme="filme"></movie-avaliacao>  
    <movie-assistir v-if="!filme.Assistido" v-bind:idRef="idModalAssistir"  v-bind:filme="filme"></movie-assistir> 
    <movie-remover v-if="!filme.Assistido" v-bind:idRef="idModalRemover"  v-bind:filme="filme"></movie-remover> 
</div>`,
    mounted() { },
    computed:{
        idModalAvaliacao(){
            return `avaliarModal${this.filme.Id}`;
        },
        idModalRemover(){
            return `removerModal${this.filme.Id}`;
        },
        idModalAssistir(){
            return `assistirModal${this.filme.Id}`;
        },
        targetModalAvaliacao(){
            return `#avaliarModal${this.filme.Id}`;
        },
        targetModalAssistir(){
            return `#assistirModal${this.filme.Id}`;
        },
        targetModalRemover(){
            return `#removerModal${this.filme.Id}`;
        }
    },
    beforeDestroy() {}
});