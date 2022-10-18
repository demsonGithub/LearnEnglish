module.exports = {
  root: true,
  env: {
    browser: true,
    node: true,
    es2021: true,
  },
  parser: 'vue-eslint-parser',
  extends: [
    'eslint:recommended',
    'plugin:vue/vue3-recommended',
    'plugin:@typescript-eslint/recommended',
    'plugin:prettier/recommended',
    // eslint-config-prettier 的缩写
    'prettier',
  ],
  parserOptions: {
    ecmaVersion: 12,
    parser: '@typescript-eslint/parser',
    sourceType: 'module',
    ecmaFeatures: {
      jsx: true,
    },
  },
  // eslint-plugin-vue @typescript-eslint/eslint-plugin eslint-plugin-prettier的缩写
  plugins: ['vue', '@typescript-eslint', 'prettier'],
  rules: {
    '@typescript-eslint/ban-ts-ignore': 'off',
    '@typescript-eslint/no-unused-vars': 'off',
    '@typescript-eslint/explicit-function-return-type': 'off',
    '@typescript-eslint/no-explicit-any': 'off',
    '@typescript-eslint/no-var-requires': 'off',
    '@typescript-eslint/no-empty-function': 'off',
    '@typescript-eslint/no-empty-interface': 'off',
    '@typescript-eslint/no-use-before-define': 'off',
    '@typescript-eslint/ban-ts-comment': 'off',
    '@typescript-eslint/ban-types': 'off',
    '@typescript-eslint/no-non-null-assertion': 'off',
    '@typescript-eslint/explicit-module-boundary-types': 'off',
    'prettier/prettier': 'error',
    // 添加组件命名忽略规则
    'vue/multi-word-component-names': [
      'error',
      {
        ignores: ['index'], //需要忽略的组件名
      },
    ],
    'no-unsafe-finally': 'off',
    // 禁止出现console
    'no-console': 'warn',
    // 禁用debugger
    'no-debugger': 'warn',
    // 禁止出现重复的 case 标签
    'no-duplicate-case': 'error',
    // 空语句块
    'no-empty': 'off',
    // 空函数
    'no-empty-function': 'off',
    // 不必要的括号
    'no-extra-parens': 'off',
    // 对 function 声明重新赋值
    'no-func-assign': 'warn',
    // 在 return、throw、continue 和 break 语句之后出现不可达代码
    'no-unreachable': 'warn',
    // 要求 switch 语句中有 default 分支
    'default-case': 'warn',
    // 要求使用 === 和 !==
    eqeqeq: 'warn',
    // if 语句中 return 语句之后有 else 块
    'no-else-return': 'warn',
    // 不必要的嵌套块
    'no-lone-blocks': 'warn',
    // 使用多个空格
    'no-multi-spaces': 'off',
    // 多次声明同一变量
    'no-redeclare': 'error',
    // 在 return 语句中使用赋值语句
    'no-return-assign': 'warn',
    // 不必要的 return await
    'no-return-await': 'off',
    // 自我赋值
    'no-self-assign': 'warn',
    // 自身比较
    'no-self-compare': 'warn',
    // 不必要的 catch 子句
    'no-useless-catch': 'warn',
    // 多余的 return 语句
    'no-useless-return': 'warn',
    // 变量声明与外层作用域的变量同名
    'no-shadow': 'off',
    // 允许delete变量
    'no-delete-var': 'off',
    // 数组方括号中使用一致的空格
    'array-bracket-spacing': 'warn',
    // 在代码块中使用一致的大括号风格
    'brace-style': 'warn',
    // 使用骆驼拼写法命名约定
    camelcase: 'off',
    // 强制使用一致的缩进
    indent: 'off',
    // 在 JSX 属性中一致地使用双引号或单引号
    'jsx-quotes': 'off',
    // 可嵌套的块的最大深度4
    'max-depth': 'off',
    //  if 作为唯一的语句出现在 else 语句中
    'no-lonely-if': 'warn',
    // 空格和 tab 的混合缩进
    'no-mixed-spaces-and-tabs': 'off',
    // 禁止出现多行空行
    'no-multiple-empty-lines': 'off',
    // 禁止出现分号 ;
    semi: ['warn', 'never'],
    // 使用单引号
    quotes: ['error', 'single'],
    // 强制在块之前使用一致的空格
    'space-before-blocks': 'warn',
    // 强制在圆括号内使用一致的空格
    'space-in-parens': 'warn',
    // 要求操作符周围有空格
    'space-infix-ops': 'warn',
    // 强制在一元操作符前后使用一致的空格
    'space-unary-ops': 'warn',
    // 强制在 switch 的冒号左右有空格
    'switch-colon-spacing': 'warn',
    // 强制箭头函数的箭头前后使用一致的空格
    'arrow-spacing': 'warn',
    'no-var': 'error',
    'prefer-const': 'warn',
    'prefer-rest-params': 'warn',
    'no-useless-escape': 'warn',
    'no-irregular-whitespace': 'warn',
    'no-prototype-builtins': 'warn',
    'no-fallthrough': 'warn',
    'no-extra-boolean-cast': 'warn',
    'no-case-declarations': 'warn',
    'no-async-promise-executor': 'warn',
  },
  globals: {
    defineProps: 'readonly',
    defineEmits: 'readonly',
    defineExpose: 'readonly',
    withDefaults: 'readonly',
  },
}
