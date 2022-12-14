import { Constant } from '@/constant'
import { getCookie } from '@/utils/jsCookie'
import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse } from 'axios'

class RequestInstance {
  public instance: AxiosInstance | undefined

  constructor(config: AxiosRequestConfig) {
    // 创建axios的实例
    this.instance = axios.create(config)
    // 添加请求和响应拦截器
    this.initRequestInterceptors()
    this.initResponseInterceptors()
  }

  /**
   * @description: 初始化请求拦截器
   * @return {*}
   */
  public initRequestInterceptors() {
    this.instance.interceptors.request.use(
      (config: AxiosRequestConfig) => {
        // 请求时携带token
        const token = getCookie(Constant.tokenKey)

        if (token) {
          config.headers.Authorization = 'Bearer ' + token
        }

        return config
      },
      (error: any) => {
        console.error(error)
      }
    )
  }

  /**
   * @description: 初始化响应拦截器
   * @return {*}
   */
  public initResponseInterceptors() {
    this.instance.interceptors.response.use(
      // 请求成功
      (response: AxiosResponse) => {
        if (response.status === 200) {
          // 成功直接返回data内容
          // return Promise.resolve(response.data)

          return Promise.resolve(response)
        }

        this.errorHandle(response)
        return response
      },
      // 请求失败
      (err: any) => {
        const { response } = err
        if (response) {
          // 请求已发出，不是2xx
          this.errorHandle(response)
          return Promise.reject(response.data)
        }
        // 处理请求超时,断网的情况
        console.log('网络连接异常,请稍后再试!')
      }
    )
  }

  /**
   * @description: http请求错误
   * @return {*}
   * @param {any} res 响应回调，根据不同状态进行不同的操作
   */
  private errorHandle(res: any) {
    switch (res.status) {
      case 401:
        break
      case 403:
        break
      case 404:
        console.log('请求的资源不存在')
        break
      default:
        console.log('连接错误')
    }
  }

  /**
   * @description: 请求超时处理
   * @return {*}
   * @param {any} error
   */
  private timeoutHandle(error: any) {
    const config = error.config
    if (!config || !config.retry) {
      return Promise.reject(error)
    }
  }
}

/**
 * 请求的配置
 */
const requestConfig: AxiosRequestConfig = {
  baseURL: import.meta.env.VITE_APP_WEBAPI_URL, //请求地址
  //timeout: 5000, //超时时间，毫秒
  // 设置请求头，如果还有415错误，可能是get或post方法传参错误
  headers: {
    'Content-Type': 'application/json',
  },
}
/**
 * 请求的实例,用class的方式可以实例化多个请求地址
 */
const request = new RequestInstance(requestConfig).instance

enum apiResultCode {
  fail = 0,
  success = 1,
}

interface apiResult {
  code: apiResultCode
  msg: string
  data: any
}

export { request, apiResultCode, type apiResult }
