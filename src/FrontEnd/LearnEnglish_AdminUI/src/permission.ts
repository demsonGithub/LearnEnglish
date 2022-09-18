import router from './router/index'
import Nprogress from 'nprogress'
import { getCookie } from './utils/jsCookie'
import { tokenKey, whiteList } from './constant'
router.beforeEach((to, from, next) => {
  Nprogress.start()

  if (getCookie(tokenKey)) {
    next()
  } else if (whiteList.indexOf(to.path) !== -1) {
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
