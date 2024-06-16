import Vue from 'vue';
import App from './App.vue';
import router from './router';
import './plugins/font-awesome';
import AuthService from './services/AuthService';

Vue.config.productionTip = false;

router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (!AuthService.isTokenValid()) {
      next('/login');
    } else {
      next();
    }
  } else {
    next();
  }
});

new Vue({
  router,
  render: h => h(App),
}).$mount('#app');
