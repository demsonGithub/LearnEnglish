import Cookies from 'js-cookie'

export function getCookie(value: string) {
  return Cookies.get(value)
}

export function setCookie(key: string, value: string, expires_second = -1) {
  if (expires_second === -1) {
    Cookies.set(key, value)
  } else {
    const expires = new Date(new Date().getTime() + expires_second * 1000)
    Cookies.set(key, value, {
      expires: expires,
    })
  }
}

export function removeCookie(key: string) {
  Cookies.remove(key)
}
