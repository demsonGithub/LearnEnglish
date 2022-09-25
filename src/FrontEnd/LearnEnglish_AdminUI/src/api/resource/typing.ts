import { apiResult } from '../request'

export interface IQueryCategoryParams {
  title: string
}

export interface ICategoryApi {
  queryCategoryList: (params: IQueryCategoryParams) => Promise<apiResult>
}
