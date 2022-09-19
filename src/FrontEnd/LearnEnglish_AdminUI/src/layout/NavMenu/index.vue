<template>
  <div class="menu-wrapper">
    <Logo />
    <el-scrollbar>
      <el-menu
        mode="vertical"
        :router="true"
        :default-active="(activeMenu as string)"
        :unique-opened="true"
        :collapse="isCollapse"
        :collapse-transition="false"
        background-color="rgb(49, 53, 65)"
        text-color="rgb(230,230,230)"
        active-text-color="rgb(57,157,255)"
      >
        <!-- 递归生成菜单对象 -->
        <menu-item
          v-for="item in menulist"
          :key="item.path"
          :item="item"
          :base-path="item.path"
        />
      </el-menu>
    </el-scrollbar>
  </div>
</template>

<script lang="ts" setup>
import { useAuthStore } from '@/store/modules/authStore'
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import Logo from './components/Logo.vue'
import MenuItem from './components/MenuItem.vue'

const router = useRouter()
const authStore = useAuthStore()

const activeMenu = computed(() => {
  const { meta, path } = router.currentRoute.value
  if (meta.activeMenu) {
    return meta.activeMenu
  }
  return path
})
const isCollapse = computed(() => false)
const menulist = computed(() => {
  return authStore.menulist
})
</script>
<style lang="scss" scope>
.menu-wrapper {
  height: 100%;
  background-color: $menuBgColor;
  .logo {
    height: $headerHeight;
  }
}
.el-scrollbar {
  overflow-x: hidden !important;
  height: calc(100% - $headerHeight - 0px);
}
.el-menu {
  border-right: 0;
}
</style>
