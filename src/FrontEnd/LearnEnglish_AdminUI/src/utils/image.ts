/**
 * @description: 获取assets/images下的静态图片资源
 * @return {*}
 * @param {string} name
 */
export function getImageUrl(name: string) {
  return new URL(
    `/src/assets/images/${name}`,
    import.meta.env.VITE_APP_RESOURCE_URL
  ).href
}
