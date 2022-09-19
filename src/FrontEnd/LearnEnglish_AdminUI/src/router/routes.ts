import Layout from '@/layout/index.vue'
import { RouteRecordRawExt } from './typing'

const constantRoutes: RouteRecordRawExt[] = [
  {
    path: '/login',
    name: 'login',
    meta: { title: '登录', hidden: true },
    component: () => import('@/views/login/index.vue'),
  },
  {
    path: '/',
    name: 'main',
    redirect: '/home',
    component: Layout,
    meta: { title: '主页' },
    children: [
      {
        path: '/home',
        name: 'home',
        meta: { title: '主页', icon: 'home' },
        component: () => import('@/views/home/index.vue'),
      },
    ],
  },
  {
    path: '/404',
    name: '404',
    component: () => import('@/views/error/NotFound.vue'),
    meta: { title: '404', hidden: true },
  },
  {
    path: '/:pathMatch(.*)',
    name: 'allMatch',
    redirect: '/404',
    meta: { title: '全路由匹配', hidden: true },
  },
]

// 权限路由
const asyncRoutes: RouteRecordRawExt[] = [
  {
    path: '/系统管理',
    component: Layout,
    redirect: '/system/user',
    name: 'system',
    meta: { title: '系统管理', icon: 'system' },
    children: [
      {
        path: '/system/user',
        name: 'user',
        component: () => import('@/views/system/user/index.vue'),
        meta: { title: '用户管理', icon: 'user' },
      },
      {
        path: '/system/menu',
        name: 'menu',
        component: () => import('@/views/system/menu/index.vue'),
        meta: { title: '菜单管理', icon: 'menu', roles: ['admin'] },
      },
    ],
  },
  {
    path: '/资源管理',
    component: Layout,
    redirect: '/resource/sound',
    name: 'resource',
    meta: { title: '资源管理', icon: 'resource' },
    children: [
      {
        path: '/resource/sound',
        name: 'sound',
        component: () => import('@/views/resource/sound/index.vue'),
        meta: { title: '听力管理', icon: 'sound', roles: ['admin'] },
      },
    ],
  },
]

export { constantRoutes, asyncRoutes }
