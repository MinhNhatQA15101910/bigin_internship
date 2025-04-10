import LoginView from '@/auth/views/LoginView.vue'
import NotFoundView from '@/common/views/NotFoundView.vue'
import FacilitiesView from '@/facility-listings/views/FacilitiesView.vue'
import HomeView from '@/home/views/HomeView.vue'
import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/facilities',
      name: 'facilities',
      component: FacilitiesView,
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView,
    },
    {
      path: '/:catchAll(.*)',
      name: 'not-found',
      component: NotFoundView,
    },
  ],
})

export default router
