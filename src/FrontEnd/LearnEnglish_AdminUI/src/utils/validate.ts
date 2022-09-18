//@description: 正则验证是否为外部资源
export function isExternal(path: string): boolean {
  return /^(https?:|mailto:|tel:)/.test(path)
}
