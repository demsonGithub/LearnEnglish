import { apiResult } from '../request'

export interface ILoginParams {
  account: string
  password: string | number
}

export interface ILoginApi {
  login: (params: ILoginParams) => Promise<apiResult>
}
