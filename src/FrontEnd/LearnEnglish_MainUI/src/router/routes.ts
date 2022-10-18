import { RouteRecordRawExt } from '@/extension/RouteExt'
import Layout from '@/layout/index.vue'

const constantRoutes: RouteRecordRawExt[] = [
  {
    path: '/',
    name: 'main',
    redirect: '/home',
    component: Layout,
    children: [
      {
        path: '/home',
        name: 'home',
        component: () => import('@/views/home/index.vue'),
      },
      {
        path: '/search',
        name: 'search',
        component: () => import('@/views/search/index.vue'),
      },
    ],
  },
]

const asyncRoutes: RouteRecordRawExt[] = []

export { constantRoutes, asyncRoutes }
