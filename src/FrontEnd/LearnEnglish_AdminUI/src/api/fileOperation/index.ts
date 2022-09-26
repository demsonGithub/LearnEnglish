import { apiResult, request } from '../request'
import { IFileOperationApi } from './typing'

const fileOperationApi: IFileOperationApi = {
  uploadFile: async function (params: FormData): Promise<apiResult> {
    const result = await request({
      method: 'post',
      headers: {
        'Content-Type': 'multipart/form-data',
      },
      url: '/api/Upload/UploadFile',
      baseURL: '/FileOperation',
      data: params,
    })

    return result.data
  },
}

export default fileOperationApi
