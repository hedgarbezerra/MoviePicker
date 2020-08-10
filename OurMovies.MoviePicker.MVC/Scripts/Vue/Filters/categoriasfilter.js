Vue.filter('categorias-concat', (arr) => {
    var categorias = arr.reduce((acc , curr) => `${acc}${curr.Nome}, `, "");
    var categoriasStr = categorias.trim();
    if(categoriasStr[categoriasStr.length - 1] == ',')
        categoriasStr = categoriasStr.slice(0, categoriasStr.length - 1);

    return categoriasStr;
})