import { RouteRecordRaw } from 'vue-router'
import Layout from '@/layout/index.vue'

const constantRoutes: RouteRecordRaw[] = [
  {
    path: '/',
    name: 'main',
    component: Layout,
    redirect: '/home',
    children: [
      {
        path: '/home',
        name: 'home',
        component: () => import('@/views/home/index.vue'),
      },
    ],
  },
]

export { constantRoutes }
