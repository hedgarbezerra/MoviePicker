Vue.filter('categorias-concat', (arr) => {
    var categorias = arr.reduce((acc , curr) => `${acc} ${curr.Nome}`, "");
    return categorias.trim();
})