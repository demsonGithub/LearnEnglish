import { request } from '../request'

const testApi = {
  login: async function (params: IRequestParams): Promise<apiResult> {
    const result = await request({
      method: 'get',
      url: '/user/login',
      params,
    })

    return result.data
  },
  register: function (): Promise<apiResult> {
    throw new Error('Function not implemented.')
  },
}

export default testApi
