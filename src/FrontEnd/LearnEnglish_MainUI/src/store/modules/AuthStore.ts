import { RouteRecordRawExt } from '@/extension/RouteExt'
import { asyncRoutes, constantRoutes } from '@/router/routes'
import {
  dataToRouteRecordRaw,
  filterPermissionRoute,
  generateFlatRoutes,
} from '@/utils/RouterHelper'
import { defineStore } from 'pinia'

export const useAuthStore = defineStore({
  id: 'sys-auth',
  state: () => ({
    roles: [],
    menulist: [],
  }),
  getters: {},
  actions: {
    /**
     * 获取用户的角色列表
     */
    async getCueentUserRoles() {
      this.roles = ['admin']
    },
    /**
     *  生成扁平化路由，菜单显示不受影响
     */
    async generateRoutes(): Promise<RouteRecordRawExt[]> {
      let formatResult: RouteRecordRawExt[] = []
      //#region 菜单数据从后台获取
      // const apiResult: any[] = []

      // // 将后台数据返回生成标准的RouteRecirdRaw数组格式
      // if (apiResult.length > 0) {
      //   formatResult = dataToRouteRecordRaw(apiResult)
      // }
      //#endregion

      const currentRoles = this.roles
      /** 菜单路由写在routes.ts中 */
      formatResult = filterPermissionRoute(asyncRoutes, currentRoles)

      /** 合并路由 */
      const mergeResult = [...constantRoutes, ...formatResult]

      this.menulist = mergeResult

      /** 返回一个扁平化的路由 */
      const finalRoutes = generateFlatRoutes(mergeResult)

      return finalRoutes
    },
  },
})
