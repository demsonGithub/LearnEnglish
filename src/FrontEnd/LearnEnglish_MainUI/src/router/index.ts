import { createRouter, createWebHistory } from 'vue-router'
import { constantRoutes } from './routes'

const routes = [...constantRoutes]

const router = createRouter({
  history: createWebHistory(),
  routes: routes,
})

export default router
