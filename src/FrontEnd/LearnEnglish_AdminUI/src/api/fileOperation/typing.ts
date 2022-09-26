import { apiResult } from '../request'

export interface IUploadFileParams {
  file: any
}

export interface IFileOperationApi {
  uploadFile: (params: IUploadFileParams) => Promise<any>
}
