<template>
  <div class="wrapper">
    <el-input
      v-model="searchInput"
      class="input-search"
      placeholder="请输入搜索内容"
    />
    <el-button class="btn-search" type="primary" round @click="handleSearch">
      搜索
    </el-button>
    <el-button class="btn-home" type="success" round @click="handleGotoHome">
      首页
    </el-button>
  </div>
</template>
<script lang="ts" setup name="SearchHeader">
import { ElMessage } from 'element-plus'
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const searchInput = ref('')

const handleSearch = () => {
  if (searchInput.value.trim() === '') {
    ElMessage({
      message: '请输入关键字',
      type: 'warning',
    })
    return
  }

  
  router.push({ path: '/search', query: { keyword: searchInput.value } })
}

const handleGotoHome = () => {
  router.push({ path: '/' })
}
</script>
<style lang="scss" scoped>
.wrapper {
  text-align: center;
}
.input-search {
  width: 80%;
  height: 50px;

  margin-top: 30px;
  margin-bottom: 10px;
}
.btn-search {
  width: 40%;
  height: 50px;
}
.btn-home {
  width: 40%;
  height: 50px;
}
</style>
