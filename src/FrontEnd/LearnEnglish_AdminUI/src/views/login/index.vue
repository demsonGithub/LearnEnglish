<template>
  <div class="login-wrapper">
    <div class="login-form">
      <div class="login-title">
        <span>LearnEnglish后台管理</span>
      </div>
      <div class="login-box">
        <el-form ref="loginFormRef" :model="loginForm" :rules="rules">
          <el-form-item prop="username">
            <el-input
              v-model="loginForm.username"
              size="large"
              class="login-input"
              placeholder="请输入账号"
            >
              <template #prefix>
                <svg-icon prop-icon="account"></svg-icon>
              </template>
            </el-input>
          </el-form-item>
          <el-form-item prop="password">
            <el-input
              v-model="loginForm.password"
              size="large"
              class="login-input"
              placeholder="请输入密码"
            >
              <template #prefix>
                <svg-icon prop-icon="password"></svg-icon>
              </template>
            </el-input>
          </el-form-item>
          <el-form-item prop="verifyCode">
            <el-input
              v-model="loginForm.verifyCode"
              size="large"
              class="login-input input-verifycode"
              placeholder="验证码"
            >
              <template #prefix>
                <svg-icon prop-icon="verify-code"></svg-icon>
              </template>
            </el-input>
            <div id="verifyContainer" class="verifyContainer"></div>
          </el-form-item>
          <el-form-item>
            <el-button
              id="btnlogin"
              type="primary"
              @click="loginHandle(loginFormRef)"
            >
              登录
            </el-button>
          </el-form-item>
        </el-form>
      </div>
    </div>
  </div>
</template>
<script lang="ts" setup name="login">
import loginApi from '@/api/login'
import { apiResultCode } from '@/api/request'
import { Constant } from '@/constant'
import { setCookie } from '@/utils/jsCookie'
import { IVerifyCodeOptions, verifyType, VerifyCode } from '@/utils/verifyCode'
import { FormInstance, ElMessage } from 'element-plus'
import { onMounted, reactive, ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const loginFormRef = ref<FormInstance>()
const loginForm = reactive({
  username: '',
  password: '',
  verifyCode: '',
})
const rules = {
  username: [{ required: true, message: '请输入账号', tigger: 'blur' }],
  password: [{ required: true, message: '请输入密码', tigger: 'blur' }],
  verifyCode: [{ required: true, message: '请输入验证码', tigger: 'blur' }],
}
const verifyOptions: IVerifyCodeOptions = {
  cantainerId: 'verifyContainer',
  canvasId: 'verifyCanvas',
  type: verifyType.number,
}
const verifyCodeInstance = new VerifyCode(verifyOptions)
const initLogin = () => {
  verifyCodeInstance.GenerateVerifyImg()
}

const loginHandle = (formVal: FormInstance) => {
  if (!formVal) {
    return
  }
  if (!verifyCodeInstance.ValidateCode(loginForm.verifyCode)) {
    ElMessage.error('验证码错误')
    return false
  }
  formVal.validate(async (valid, fields) => {
    if (valid) {
      const apiParams: ILoginParams = {
        account: loginForm.username,
        password: loginForm.password,
      }

      const result = await loginApi.login(apiParams)

      if (result.code === apiResultCode.fail) {
        verifyCodeInstance.refresh()
      } else {
        setCookie(Constant.tokenKey, result.data.token)
        router.push({ path: '/' })
      }
    } else {
      console.debug(fields)
      verifyCodeInstance.refresh()
    }
  })
}
onMounted(() => {
  initLogin()
})
</script>
<style lang="scss" scoped>
.login-wrapper {
  width: 100%;
  height: 100%;
  user-select: none;
  background: url('@/assets/images/bg.png') no-repeat;
  background-attachment: fixed;
  background-size: cover;
}
.login-form {
  width: 500px;
  height: 650px;
  background-color: rgba(230, 230, 230, 0.5);
  border: 0;
  border-radius: 10px;
  box-shadow: 7px 15px 30px #52777e;
  position: relative;
  top: 260px;
  left: 60%;

  .login-title {
    text-align: center;

    img {
      width: 100px;
      height: 100px;
    }
    span {
      display: block;
      line-height: 100px;
      font-size: 28px;
      font-weight: 700;
      color: #777;
    }
  }
}
.login-box {
  width: 100%;
  height: 450px;
  position: relative;
  top: 50px;
  display: flex;
  justify-content: center;

  .login-input {
    width: 350px;

    :deep(.el-input__wrapper, .el-input__inner) {
      font-size: 18px !important;
      border: 1px solid rgb(120, 120, 120, 0.5) !important;
      background-color: transparent !important;
    }
  }
  .input-verifycode {
    width: 220px;
  }
  .verifyContainer {
    width: 130px;
    height: 40px;
  }

  #btnlogin {
    width: 100%;
  }
}
</style>
>
