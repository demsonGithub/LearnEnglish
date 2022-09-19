<template>
  <div class="layout-box">
    <el-container class="main-wrapper">
      <el-aside>
        <nav-menu />
      </el-aside>
      <el-container>
        <el-header>Header</el-header>
        <el-main>
          <router-view v-slot="{ Component }">
            <keep-alive :include="includeList">
              <component :is="Component" :key="route.name" />
            </keep-alive>
          </router-view>
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>

<script lang="ts" setup>
import { ref } from 'vue'
import { useRoute } from 'vue-router'
import NavMenu from './NavMenu/index.vue'

const route = useRoute()
const includeList = ref([])
</script>
<style lang="scss" scope>
.layout-box {
  height: 100%;
}

.main-wrapper {
  width: 100%;
  height: 100%;

  .el-aside {
    width: 200px;
  }
  .el-header {
    background-color: $headerBgColor;
    height: $headerHeight;
    width: 100%;
    color: rgb(255, 255, 255);
  }
}
</style>
