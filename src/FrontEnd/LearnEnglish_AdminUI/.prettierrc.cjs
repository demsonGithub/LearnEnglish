module.exports = {
  printWidth: 80, //单行长度
  tabWidth: 2, //缩进长度
  jsxSingleQuote: true, // jsx中使用单引号
  jsxBracketSameLine: false, //多属性html标签的‘>’折行放置
  singleQuote: true, //使用单引号
  semi: false, //句末使用分号
  overrides: [
    {
      files: '*.json',
      options: {
        printWidth: 200,
      },
    },
  ],
  arrowParens: 'avoid', //单参数箭头函数参数周围使用圆括号-eg: (x) => x
  htmlWhitespaceSensitivity: 'ignore',
}
