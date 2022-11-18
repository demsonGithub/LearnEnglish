import { request } from '../request'

const searchApi = {
  searchByKeyword: async function (params: ISearchParams): Promise<apiResult> {
    const result = await request({
      method: 'get',
      url: '/api/search/SearchEpisodes',
      params,
    })

    return result.data
  },
}

export default searchApi
