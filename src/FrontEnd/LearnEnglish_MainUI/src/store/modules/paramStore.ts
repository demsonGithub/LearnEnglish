import { getCookie, setCookie } from '@/utils/jsCookie'
import { defineStore } from 'pinia'
import { stringify } from 'querystring'

export const useParamStore = defineStore({
  id: 'sys-param',
  state: () => ({
    paramsValue: null,
  }),
  getters: {
    getParams: state => {
      if (state.paramsValue != null) {
        return state.paramsValue
      }
      try {
        state.paramsValue = JSON.parse(getCookie('route-params'))
      } catch (err) {
        state.paramsValue = null
      } finally {
        return state.paramsValue
      }
    },
  },
  actions: {
    setParams(value: any) {
      this.paramsValue = value
      setCookie('route-params', JSON.stringify(this.paramsValue))
    },
  },
})
