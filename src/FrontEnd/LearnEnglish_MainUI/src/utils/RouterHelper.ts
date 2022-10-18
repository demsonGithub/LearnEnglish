import Layout from '@/layout/index.vue'
import NotFoundComponent from '@/views/error/NotFound.vue'
import { h, resolveComponent } from 'vue'
import router from '@/router'
import { isDef, isUnDef } from './IsExtension'
import { RouteRecordRawExt } from '@/extension/routeExt'

/** 权限过滤，判断菜单是否有权限 */
export function filterPermissionRoute(
  permissionRoutes: RouteRecordRawExt[],
  currentRoles: string[]
) {
  const arrResult: RouteRecordRawExt[] = []

  permissionRoutes.forEach(item => {
    const temp = { ...item }
    temp.children = []

    if (hasPermission(currentRoles, item.meta.roles)) {
      if (item.children) {
        temp.children = filterPermissionRoute(item.children, currentRoles)
      }

      arrResult.push(temp)
    }
  })

  return arrResult
}

function hasPermission(currentRoles: string[], needRoutes: string[]) {
  if (typeof needRoutes === 'undefined' || needRoutes.length === 0) {
    return true
  }

  let result = false
  const cRoutes = new Set(currentRoles)
  const nRoutes = new Set(needRoutes)
  try {
    cRoutes.forEach(item => {
      if (nRoutes.has(item)) {
        result = true
        // 抛出异常，跳出循环
        throw new Error('end')
      }
    })
  } catch (err) {
    if (err === 'end') {
    }
  }
  return result
}

/** vite动态导入加载组件，目录不能用@符号 */
const modules = import.meta.glob('../views/**/**.vue')
/**
 * @description: 将后台数据整理成需要的标注路由格式
 * @return {*}
 * @param {any} menulist
 */
export function dataToRouteRecordRaw(menulist: any[]) {
  const arrResult: RouteRecordRawExt[] = []
  menulist.forEach((item: any) => {
    // 是否需要重定向
    let targetObj: RouteRecordRawExt

    if (item.redirect && item.redirect !== 'noRedirect') {
      /** RouteRecordRaw */
      targetObj = {
        path: item.path,
        redirect: item.redirect,
      }
    } else {
      let targetComponent
      if (item.parentId === 0) {
        /** 根节点为Layout */
        targetComponent = Layout
      } else if (
        /**如果不是父级节点，并且有子节点 */
        item.parentId !== 0 &&
        item.children &&
        item.children.length > 0
      ) {
        targetComponent = {
          render: () => h(resolveComponent('router-view')),
        }
      } else {
        /** 目标组件 */
        targetComponent = modules[`../views${item.path}/index.vue`]
        /** 如果没有找到对应的组件，赋值一个提示组件 */
        if (isUnDef(targetComponent)) {
          targetComponent = NotFoundComponent
          // eslint-disable-next-line no-console
          console.warn(
            `message:the target component was not found,${item.path}`
          )
        }
      }
      /** RouteRecordNormalized */
      targetObj = {
        path: item.path,
        component: targetComponent,
      }
    }
    targetObj.name = isDef(item.name) ? item.name : ''
    /** 添加meta属性 */
    targetObj.meta = {
      title: isDef(item.title) ? item.title : '',
      hidden: isDef(item.hidden) ? item.hidden : false,
      icon: isDef(item.icon) ? item.icon : '',
    }
    /** 子节点递归 */
    if (item.children && item.children.length > 0) {
      targetObj.children = dataToRouteRecordRaw(item.children)
    }

    arrResult.push(targetObj)
  })

  return arrResult
}

/**
 * @description: 扁平化处理的路由数组
 * @return {*}
 * @param {RouteRecordRaw} accessRoutes
 */
export function generateFlatRoutes(accessRoutes: RouteRecordRawExt[]) {
  const flatRoutes: RouteRecordRawExt[] = []

  for (const item of accessRoutes) {
    let flatChildrenRoutes: RouteRecordRawExt[] = []
    if (item.children && item.children.length > 0) {
      flatChildrenRoutes = castToFlatRoute(item.children)
    }

    /** 不能直接操作item，因为其指向的是指针 */
    const newRoutesObj = { ...item }
    /** 一级路由是布局路由,需要处理的只是其子路由数据 */
    newRoutesObj.children = flatChildrenRoutes
    /** 如果没有Layout容器，则添加 */
    if (!item.component) {
      newRoutesObj.component = Layout
    }
    flatRoutes.push(newRoutesObj)
  }

  return flatRoutes
}

/**
 * @description: 递归将多维数组转换成只有子节点的一维数组
 * @return {*} 只有子节点的一维数组
 */
function castToFlatRoute(
  routes: RouteRecordRawExt[],
  flatRoutes: RouteRecordRawExt[] = []
): RouteRecordRawExt[] {
  for (const item of routes) {
    if (item.children && item.children.length > 0) {
      if (item.redirect && item.redirect !== 'noRedirect') {
        flatRoutes.push(item)
      }
      castToFlatRoute(item.children, flatRoutes)
    } else {
      flatRoutes.push(item)
    }
  }
  return flatRoutes
}

// 添加路由
export function appendRoutes(accessRoutes: RouteRecordRawExt[]) {
  accessRoutes.forEach(item => {
    if (!router.hasRoute(item.name)) {
      router.addRoute(item)
    }
  })
  // tips:如果，在静态路由里没有添加pathMatch，那需要最后添加404页面
}
