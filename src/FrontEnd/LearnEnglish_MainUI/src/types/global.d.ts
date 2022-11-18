declare interface apiResult {
  code: apiResultCode
  msg: string
  data: any
}

declare interface IMockFormat {
  url: string
  requestType: string
  responseAction: any
}

declare interface IEpisode {
  episodeId: string
  title: string
  description: string
  subtitles: string
  albumId: string
}
