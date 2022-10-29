import { apiResult, request } from '../request'

const fileOperationApi = {
  uploadFile: async function (params: FormData): Promise<apiResult> {
    const result = await request({
      method: 'post',
      headers: {
        'Content-Type': 'multipart/form-data',
      },
      url: '/FileOperation/Upload/UploadFile',
      data: params,
    })

    return result.data
  },

  getFileHost: async function (params: any): Promise<apiResult> {
    const result = await request({
      method: 'get',
      url: '/FileOperation/Upload/GetFileHost',
      params: params,
    })

    return result.data
  },
}

export default fileOperationApi
