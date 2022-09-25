import { apiResult, request } from '../request'
import * as IService from './typing'

const categoryApi: IService.ICategoryApi = {
  queryCategoryList: async function (
    params: IService.IQueryCategoryParams
  ): Promise<apiResult> {
    const result = await request({
      method: 'get',
      url: '/api/Category/GetCategoryListByCondiations',
      baseURL: '/Category',
      params: params,
    })

    return result.data
  },
}

export { categoryApi }
