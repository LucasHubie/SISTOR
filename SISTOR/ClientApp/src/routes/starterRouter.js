import Vue from 'vue';
import Router from 'vue-router';
import DashboardLayout from '../views/Starter/SampleLayout.vue';
import Starter from '../views/Starter/SamplePage.vue';

import VueMask from 'v-mask';

Vue.use(Router);

Vue.use(VueMask);


export default new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      redirect: '/login',
      component: DashboardLayout,
      children: [
        {
          path: 'login',
          name: 'login',
          components: { default: Starter }
        }
      ]
    }
  ],
  scrollBehavior: (to, from ,savedPosition) => {
    if (savedPosition) {
      return savedPosition;
    }
    if (to.hash) {
      return { selector: to.hash };
    }
    return { x: 0, y: 0 };
  }
});
