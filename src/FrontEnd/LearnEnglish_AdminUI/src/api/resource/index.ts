import { apiResult, request } from '../request'
import * as IService from './typing'

const categoryApi: IService.ICategoryApi = {
  queryCategoryList: async function (
    params: IService.IQueryCategoryParams
  ): Promise<apiResult> {
    const result = await request({
      method: 'get',
      url: '/api/Category/GetCategoryListByCondiations',
      baseURL: '/Listen',
      params: params,
    })

    return result.data
  },
  AddCategory: async function (
    params: IService.IAddCategoryParams
  ): Promise<apiResult> {
    const result = await request({
      method: 'post',
      url: '/api/Category/AddCategory',
      baseURL: '/Listen',
      data: params,
    })

    return result.data
  },
}

export { categoryApi }
