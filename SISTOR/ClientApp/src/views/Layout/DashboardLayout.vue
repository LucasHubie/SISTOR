<template>
  <div class="wrapper">
    <notifications></notifications>
    <side-bar>
      <template slot="links">
        <sidebar-item :link="{
            name: 'Painel inicial',
            path: '/dashboard',
            icon: 'ni ni-tv-2 text-blue',
          }">
        </sidebar-item>

        <sidebar-item :link="{
              name: 'Orçamentos',
              path: '/orcamentos',
              icon: 'ni ni-single-copy-04 text-blue'
              }">
        </sidebar-item>

        <sidebar-item :link="{
                name: 'Clientes',
                path: '/clientes',
                icon: 'ni ni-single-02 text-blue'
              }">
        </sidebar-item>

        <sidebar-item :link="{
                name: 'Ordens de Serviço',
                path: '/ordensServico',
                icon: 'ni ni-bullet-list-67 text-blue'
                }">
        </sidebar-item>

        <sidebar-item :link="{
                  name: 'Funcionários',
                  path: '/funcionarios',
                  icon: 'ni ni-circle-08 text-blue'
                }">
        </sidebar-item>

        <sidebar-item :link="{
                  name: 'Produtos',
                  path: '/produtos',
                  icon: 'ni ni-archive-2 text-blue'
                }">
        </sidebar-item>
      </template>

      <template slot="links-after">
        <hr class="my-3">
       

        <b-nav class="navbar-nav mb-md-3">
          <b-nav-item
               href=""
               >
              <i class="ni ni-spaceship"></i>
              <b-nav-text class="p-0">Sir</b-nav-text>
          </b-nav-item>

        </b-nav>
      </template>
    </side-bar>
    <div class="main-content">
      <dashboard-navbar :type="$route.meta.navbarType"></dashboard-navbar>

      <div @click="$sidebar.displaySidebar(false)">
        <fade-transition :duration="200" origin="center top" mode="out-in">
          <!-- your content here -->
          <router-view></router-view>
        </fade-transition>
      </div>
    </div>
  </div>
</template>
<script>
  /* eslint-disable no-new */
  import PerfectScrollbar from 'perfect-scrollbar';
  import 'perfect-scrollbar/css/perfect-scrollbar.css';

  function hasElement(className) {
    return document.getElementsByClassName(className).length > 0;
  }

  function initScrollbar(className) {
    if (hasElement(className)) {
      new PerfectScrollbar(`.${className}`);
    } else {
      // try to init it later in case this component is loaded async
      setTimeout(() => {
        initScrollbar(className);
      }, 100);
    }
  }

  import DashboardNavbar from './DashboardNavbar.vue';
  import ContentFooter from './ContentFooter.vue';
  import DashboardContent from './Content.vue';
  import { FadeTransition } from 'vue2-transitions';

  export default {
    components: {
      DashboardNavbar,
      ContentFooter,
      DashboardContent,
      FadeTransition
    },
    methods: {
      initScrollbar() {
        let isWindows = navigator.platform.startsWith('Win');
        if (isWindows) {
          initScrollbar('sidenav');
        }
      }
    },
    mounted() {
      this.initScrollbar()
    }
  };
</script>
<style lang="scss">
</style>
