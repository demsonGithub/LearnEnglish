//#region login
declare interface ILoginParams {
  account: string
  password: string | number
}
//#endregion

//#region fileOperation

//#endregion

//#region audio
declare interface IQueryCategoryParams {
  title: string
}

declare interface IAddCategoryParams {
  title: string
  coverUrl: string
  sequenceNumber: number
}

declare interface IUpdateCategoryParams {
  id: string
  title: string
  coverUrl: string
  sequenceNumber: number
}

declare interface IDeleteCategoryParams {
  id: string
}

declare interface IQueryAlbumParams {
  categoryId: string
  title: string
}

declare interface IAddAlbumParams {
  title: string
  coverUrl?: string
  sequenceNumber: number
  categoryId: string
}

declare interface IUpdateAlbumParams {
  id: string
  title: string
  coverUrl: string
  sequenceNumber: number
}

declare interface IQueryEpisodeParams {
  albumId: string
}

declare interface IAddEpisodeParams {
  title: string
  description: string
  sequenceNumber: number
  audioUrl: string
  durationInSecond: number
  subtitles: string
  albumId: string
}
//#endregion
