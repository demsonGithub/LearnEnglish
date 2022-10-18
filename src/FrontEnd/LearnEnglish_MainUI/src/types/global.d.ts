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
