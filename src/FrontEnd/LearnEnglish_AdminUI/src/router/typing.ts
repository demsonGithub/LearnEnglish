import { RouteRecordRaw } from 'vue-router'

export interface metaOptions {
  title: string
  hidden?: boolean
  icon?: string
  roles?: Array<string>
}

export type RouteRecordRawExt = RouteRecordRaw & { meta?: metaOptions } & {
  children?: RouteRecordRawExt[]
}
