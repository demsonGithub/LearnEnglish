import { apiResult, request } from '../request'

const categoryApi = {
  queryCategoryList: async function (
    params: IQueryCategoryParams
  ): Promise<apiResult> {
    const result = await request({
      method: 'get',
      url: '/Listen/Category/GetCategoryListByCondiations',
      params: params,
    })

    return result.data
  },
  AddCategory: async function (params: IAddCategoryParams): Promise<apiResult> {
    const result = await request({
      method: 'post',
      url: '/Listen/Category/AddCategory',
      data: params,
    })

    return result.data
  },
  UpdateCategory: async function (
    params: IUpdateCategoryParams
  ): Promise<apiResult> {
    const result = await request({
      method: 'post',
      url: '/Listen/Category/UpdateCategory',
      data: params,
    })
    return result.data
  },
  DeleteCategory: async function (
    params: IDeleteCategoryParams
  ): Promise<apiResult> {
    const result = await request({
      method: 'post',
      url: '/Listen/Category/DeleteCategory',
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
      url: '/Listen/Album/GetAlbumList',
      params: params,
    })

    return result.data
  },
  addAlbum: async function (params: IAddAlbumParams): Promise<apiResult> {
    const result = await request({
      method: 'post',
      url: '/Listen/Album/AddNewAlbum',
      data: params,
    })
    return result.data
  },
  updateAlbum: async function (params: IUpdateAlbumParams): Promise<apiResult> {
    const result = await request({
      method: 'post',
      url: '/Listen/Album/UpdateAlbum',
      data: params,
    })
    return result.data
  },
}

const episodeApi = {
  queryEpisodeList: async function (
    params: IQueryEpisodeParams
  ): Promise<apiResult> {
    const result = await request({
      method: 'get',
      url: '/Listen/Episode/GetEpisodeList',
      params: params,
    })

    return result.data
  },
  addEpisode: async function (params: IAddEpisodeParams): Promise<apiResult> {
    const result = await request({
      method: 'post',
      url: '/Listen/Episode/AddEpisode',
      data: params,
    })
    return result.data
  },
  analysisSubtitles: async function (params: FormData): Promise<apiResult> {
    const result = await request({
      method: 'post',
      headers: {
        'Content-Type': 'multipart/form-data',
      },
      url: '/Listen/Episode/AnalysisSubtitles',
      data: params,
    })
    return result.data
  },
}

export { categoryApi, albumApi, episodeApi }
