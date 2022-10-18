<template>
  <div class="layout-wrapper">
    <el-container class="main-wrapper">
      <el-header class="header-wrapper">
        <header-search></header-search>
      </el-header>
      <el-main>
        <router-view></router-view>
      </el-main>
    </el-container>
  </div>
</template>

<script lang="ts" setup>
import HeaderSearch from './Header/index.vue'
</script>
<style lang="scss" scope>
.layout-wrapper {
  height: 100%;
}
.main-wrapper {
  width: 100%;
  height: 100%;
}
.header-wrapper {
  height: 170px;
}
</style>
