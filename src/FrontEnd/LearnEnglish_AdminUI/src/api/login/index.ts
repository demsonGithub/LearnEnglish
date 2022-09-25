import * as IService from './typing'
import { apiResult, request } from '../request'

const loginApi: IService.ILoginApi = {
  login: async function (params: IService.ILoginParams): Promise<apiResult> {
    const result = await request({
      method: 'post',
      url: '/api/User/LoginByAccountPassword',
      baseURL: '/User',
      data: params,
    })

    return result.data
  },
}

export default loginApi
