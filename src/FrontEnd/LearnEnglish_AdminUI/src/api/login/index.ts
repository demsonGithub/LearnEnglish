import { apiResult, request } from '../request'

const loginApi = {
  login: async function (params: ILoginParams): Promise<apiResult> {
    const result = await request({
      method: 'post',
      url: '/api/User/LoginByAccountPassword',
      baseURL: '/System',
      data: params,
    })

    return result.data
  },
}

export default loginApi
