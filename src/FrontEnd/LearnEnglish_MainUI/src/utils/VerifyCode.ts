export enum verifyType {
  blend, // 数字字母混合
  number, // 纯数字
  letter, // 纯字母
}

export interface IVerifyCodeOptions {
  /** 容器Id */
  cantainerId: string
  /**canvas的Id */
  canvasId: string
  /** 类型 */
  type: verifyType
}

export class VerifyCode {
  cantainerId: string
  canvasId: string
  type: verifyType
  width: number
  height: number
  code: string

  numArr = '0,1,2,3,4,5,6,7,8,9'.split(',')
  letterArr = getAllLetter()

  constructor(options: IVerifyCodeOptions) {
    this.cantainerId = options.cantainerId
    this.canvasId = options.canvasId
    this.type = options.type
  }

  /**
   * @description: 生成验证码画布
   * @return {*}
   */
  public GenerateVerifyImg() {
    this._init()
    this.refresh()
  }
  /**
   * @description: 验证验证码
   * @return {*}
   */
  public ValidateCode(inputCode: string): boolean {
    inputCode = inputCode.toLowerCase()
    const currentCode = this.code.toLowerCase()
    if (inputCode === currentCode) {
      return true
    }
    this.refresh()
    return false
  }

  private _init() {
    const con = document.getElementById(this.cantainerId)
    const canvas = document.createElement('canvas')
    this.width = con.offsetWidth > 0 ? con.offsetWidth : 100
    this.height = con.offsetHeight > 0 ? con.offsetHeight : 30
    canvas.id = this.canvasId
    canvas.width = this.width
    canvas.height = this.height
    canvas.style.cursor = 'pointer'
    canvas.innerHTML = '您的浏览器版本不支持canvas'
    con.appendChild(canvas)

    canvas.addEventListener('click', (e: Event) => {
      this.refresh()
    })
  }

  public refresh() {
    this.code = ''
    const canvas: any = document.getElementById(this.canvasId)
    let ctx: any = ''
    if (canvas.getContext) {
      ctx = canvas.getContext('2d')
    } else {
      return
    }
    ctx.textBaseline = 'middle'
    ctx.fillStyle = randomColor(180, 240)
    ctx.fillRect(0, 0, this.width, this.height)
    let txtArr: string[] = []
    if (this.type === verifyType.blend) {
      //判断验证码类型
      txtArr = this.numArr.concat(this.letterArr)
    } else if (this.type === verifyType.number) {
      txtArr = this.numArr
    } else {
      txtArr = this.letterArr
    }
    for (let i = 1; i <= 4; i++) {
      const txt = txtArr[randomNum(0, txtArr.length)]
      this.code += txt
      ctx.font = randomNum(this.height / 2, this.height) + 'px SimHei' //随机生成字体大小
      ctx.fillStyle = randomColor(50, 160) //随机生成字体颜色
      ctx.shadowOffsetX = randomNum(-3, 3)
      ctx.shadowOffsetY = randomNum(-3, 3)
      ctx.shadowBlur = randomNum(-3, 3)
      ctx.shadowColor = 'rgba(0, 0, 0, 0.3)'
      const x = (this.width / 5) * i
      const y = this.height / 2
      const deg = randomNum(-30, 30)
      /**设置旋转角度和坐标原点**/
      ctx.translate(x, y)
      ctx.rotate((deg * Math.PI) / 180)
      ctx.fillText(txt, 0, 0)
      /**恢复旋转角度和坐标原点**/
      ctx.rotate((-deg * Math.PI) / 180)
      ctx.translate(-x, -y)
    }
    /**绘制干扰线**/
    for (let i = 0; i < 2; i++) {
      ctx.strokeStyle = randomColor(40, 180)
      ctx.beginPath()
      ctx.moveTo(randomNum(0, this.width), randomNum(0, this.height))
      ctx.lineTo(randomNum(0, this.width), randomNum(0, this.height))
      ctx.stroke()
    }
    /**绘制干扰点**/
    for (let i = 0; i < 10; i++) {
      ctx.fillStyle = randomColor(0, 255)
      ctx.beginPath()
      ctx.arc(
        randomNum(0, this.width),
        randomNum(0, this.height),
        1,
        0,
        2 * Math.PI
      )
      ctx.fill()
    }
  }
}

/**生成字母数组**/
function getAllLetter() {
  const letterStr =
    'a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z'
  return letterStr.split(',')
}
/**生成一个随机数**/
function randomNum(min: number, max: number) {
  return Math.floor(Math.random() * (max - min) + min)
}
/**生成一个随机色**/
function randomColor(min: number, max: number) {
  const r = randomNum(min, max)
  const g = randomNum(min, max)
  const b = randomNum(min, max)
  return 'rgb(' + r + ',' + g + ',' + b + ')'
}
