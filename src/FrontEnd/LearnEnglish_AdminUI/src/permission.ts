import router from './router/index'
import Nprogress from 'nprogress'
import { getCookie } from './utils/jsCookie'
import { useAuthStore } from './store/modules/authStore'
import { appendRoutes } from './utils/routerHelper'
import { Constant } from './constant'

router.beforeEach(async (to, from, next) => {
  Nprogress.start()

  if (getCookie(Constant.tokenKey)) {
    if (to.path === '/login') {
      next()
    } else {
      const authStore = useAuthStore()
      if (authStore.menulist.length > 0) {
        next()
      } else {
        try {
          await authStore.getCueentUserRoles()
          const finalRoutes = await authStore.generateRoutes()
          if (finalRoutes.length > 0) {
            appendRoutes(finalRoutes)
          }

          /** 处理路由还未生成时，刷新出现not match的问题 */
          if (to.path === '/404' && to.redirectedFrom !== undefined) {
            next({ path: to.redirectedFrom?.fullPath, replace: true })
          } else {
            next({ ...to, replace: true })
          }
        } catch (err) {
          console.error('router.beforeEach:', err)
          next(`/login?redirect=${to.path}`)
        }
      }
    }
  } else if (Constant.whiteList.indexOf(to.path) !== -1) {
    // 在免登录白名单，直接进入
    next()
  } else {
    next(`/login?redirect=${to.fullPath}`) // 否则全部重定向到登录页
  }
  Nprogress.done()
})

router.afterEach(() => {
  Nprogress.done()
})
