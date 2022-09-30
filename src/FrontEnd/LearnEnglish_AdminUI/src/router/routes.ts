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
    path: '/听力管理',
    component: Layout,
    redirect: '/audio/category',
    name: 'audio',
    meta: { title: '听力管理', icon: 'audio' },
    children: [
      {
        path: '/audio/category',
        name: 'category',
        component: () => import('@/views/audio/category/index.vue'),
        meta: { title: '听力资源', icon: 'audio', roles: ['admin'] },
      },
      {
        path: '/audio/album',
        name: 'album',
        component: () => import('@/views/audio/album/index.vue'),
        meta: {
          title: '专辑管理',
          icon: 'album',
          hidden: true,
          roles: ['admin'],
        },
      },
      {
        path: '/audio/episode',
        name: 'episode',
        component: () => import('@/views/audio/episode/index.vue'),
        meta: {
          title: '音频管理',
          icon: 'episode',
          hidden: true,
          roles: ['admin'],
        },
      },
    ],
  },
]

export { constantRoutes, asyncRoutes }
