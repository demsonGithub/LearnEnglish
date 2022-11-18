import { defineStore } from 'pinia'

export const useSearchStore = defineStore({
  id: 'sys-search',
  state: () => ({
    searchResult: [],
  }),
  getters: {
    getSearchList: (state): IEpisode[] => {
      return state.searchResult
    },
  },
  actions: {
    setSearchResult(value: IEpisode[]) {
      this.searchResult = value
    },
  },
})
