Vue.component('movie-card', {
    props: {
        nome: {
            type: String,
            required: true
        },
        descricao: {
            type: String,
            required: true
        },
        nota: {
            type: Number,
            required: false
        },
        dtAdicionado: {
            type: Date,
            required: true
        },
        categorias: {
            type: Array,
            required: false
        }
    },
    template: ``,
    methods: {
        diferencaDias(data){
            return moment(data).diff(moment(), 'days')
        },

    },
    mounted() { },
    beforeDestroy() {}
});