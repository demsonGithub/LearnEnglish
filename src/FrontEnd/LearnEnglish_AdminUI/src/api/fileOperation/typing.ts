import { apiResult } from '../request'

export interface IFileOperationApi {
  uploadFile: (params: FormData) => Promise<apiResult>
}
