import { apiResult, request } from '../request'
import { IFileOperationApi, IUploadFileParams } from './typing'

const fileOperationApi: IFileOperationApi = {
  uploadFile: async function (params: IUploadFileParams): Promise<any> {
    const result = await request({
      method: 'post',
      headers: {
        'Content-Type': 'multipart/form-data',
      },
      url: '/api/Upload/UploadFile',
      baseURL: '/FileOperation',
      data: params,
    })

    return result
  },
}

export default fileOperationApi
