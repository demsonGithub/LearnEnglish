import { apiResult } from '../request'

export interface IQueryCategoryParams {
  title: string
}

export interface IAddCategoryParams {
  name: string
  coverUrl: string
  sequenceNumber: number
}

export interface ICategoryApi {
  queryCategoryList: (params: IQueryCategoryParams) => Promise<apiResult>
  AddCategory: (params: IAddCategoryParams) => Promise<apiResult>
}
