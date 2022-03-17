import DashboardLayout from '@/views/Layout/DashboardLayout.vue';
import AuthLayout from '@/views/Pages/AuthLayout.vue';

import NotFound from '@/views/NotFoundPage.vue';

const routes = [
  {
    path: '/',
    redirect: 'login',
    component: DashboardLayout,
    children: [
      {
        path: '/dashboard',
        name: 'dashboard',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "demo" */ '../views/Dashboard.vue'),
        meta: {
          requiresAuth: true
        }
      },
      {
        path: '/icons',
        name: 'teste',
        component: () => import(/* webpackChunkName: "demo" */ '../views/Icons.vue')
      },
      {
        path: '/clientes',
        name: 'Clientes',
        component: () => import(/* webpackChunkName: "demo" */ '../views/Clientes.vue'),
        meta: {
          requiresAuth: true
        }
      },
      {
        path: '/profile',
        name: 'profile',
        component: () => import(/* webpackChunkName: "demo" */ '../views/Pages/UserProfile.vue')
      },
      {
        path: '/maps',
        name: 'maps',
        component: () => import(/* webpackChunkName: "demo" */ '../views/GoogleMaps.vue')
      },
      {
        path: '/orcamentos',
        name: 'Orçamentos',
        component: () => import(/* webpackChunkName: "demo" */ '../views/Orcamentos.vue'),
        meta: {
          requiresAuth: true
        }
      },
      {
        path: '/ordensServico',
        name: 'Ordem Serviço',
        component: () => import(/* webpackChunkName: "demo" */ '../views/OrdemServico.vue'),
        meta: {
          requiresAuth: true
        }
      },
      {
        path: '/funcionarios',
        name: 'Funcionários',
        component: () => import(/* webpackChunkName: "demo" */ '../views/Funcionarios.vue'),
        meta: {
          requiresAuth: true
        }
      },
      {
        path: '/produtos',
        name: 'Produtos',
        component: () => import(/* webpackChunkName: "demo" */ '../views/Produtos.vue'),
        meta: {
          requiresAuth: true
        }
      },
    ]
  },
  {
    path: '/',
    redirect: 'login',
    component: AuthLayout,
    children: [
      {
        path: '/login',
        name: 'login',
        component: () => import(/* webpackChunkName: "demo" */ '../views/Pages/Login.vue')
      },
      {
        path: '/register',
        name: 'register',
        component: () => import(/* webpackChunkName: "demo" */ '../views/Pages/Register.vue')
      },
      { path: '*', component: NotFound }
    ]
  }
];



export default routes;
