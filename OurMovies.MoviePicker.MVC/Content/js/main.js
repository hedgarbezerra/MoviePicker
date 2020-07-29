moment.locale('pt-br');
VeeValidate.localize('pt_br', localeVee);
VeeValidate.setInteractionMode('eager');
Vue.component('ValidationProvider', VeeValidate.ValidationProvider);
Vue.component('ValidationObserver', VeeValidate.ValidationObserver);
Vue.config.devtools = true;