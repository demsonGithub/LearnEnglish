import { apiResult, request } from '../request'

const loginApi = {
  login: async function (params: ILoginParams): Promise<apiResult> {
    const result = await request({
      method: 'post',
      url: '/System/User/LoginByAccountPassword',
      data: params,
    })

    return result.data
  },
}

export default loginApi
