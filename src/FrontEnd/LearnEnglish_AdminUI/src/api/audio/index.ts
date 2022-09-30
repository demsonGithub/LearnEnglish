import { apiResult, request } from '../request'

const categoryApi = {
  queryCategoryList: async function (
    params: IQueryCategoryParams
  ): Promise<apiResult> {
    const result = await request({
      method: 'get',
      url: '/api/Category/GetCategoryListByCondiations',
      baseURL: '/Listen',
      params: params,
    })

    return result.data
  },
  AddCategory: async function (params: IAddCategoryParams): Promise<apiResult> {
    const result = await request({
      method: 'post',
      url: '/api/Category/AddCategory',
      baseURL: '/Listen',
      data: params,
    })

    return result.data
  },
  UpdateCategory: async function (
    params: IUpdateCategoryParams
  ): Promise<apiResult> {
    const result = await request({
      method: 'post',
      url: '/api/Category/UpdateCategory',
      baseURL: '/Listen',
      data: params,
    })
    return result.data
  },
}

const albumApi = {
  queryAlbumList: async function (
    params: IQueryAlbumParams
  ): Promise<apiResult> {
    const result = await request({
      method: 'get',
      url: '/api/Album/GetAlbumList',
      baseURL: '/Listen',
      params: params,
    })

    return result.data
  },
  addAlbum: async function (params: IAddAlbumParams): Promise<apiResult> {
    const result = await request({
      method: 'post',
      url: '/api/Album/AddNewAlbum',
      baseURL: '/Listen',
      data: params,
    })
    return result.data
  },
  updateAlbum: async function (params: IUpdateAlbumParams): Promise<apiResult> {
    const result = await request({
      method: 'post',
      url: '/api/Album/UpdateAlbum',
      baseURL: '/Listen',
      data: params,
    })
    return result.data
  },
}

export { categoryApi, albumApi }
