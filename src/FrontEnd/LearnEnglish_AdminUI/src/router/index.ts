import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'

import { routes } from './routes'

const router = createRouter({
  history: createWebHistory(),
  routes: routes,
})

export default router
