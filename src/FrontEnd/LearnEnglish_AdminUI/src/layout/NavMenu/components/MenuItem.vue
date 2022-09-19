<template>
  <div v-if="!item.meta.hidden">
    <!-- 只有一个子节点 -->
    <template v-if="hasOneShowChild(item.children)">
      <el-menu-item
        :key="lastChild.path"
        :index="lastChild.path"
        :route="lastChild.path"
      >
        <i><svg-icon :prop-icon="lastChild.meta?.icon"></svg-icon></i>
        <template #title>
          <span>{{ lastChild.meta.title }}</span>
        </template>
      </el-menu-item>
    </template>
    <el-sub-menu v-else ref="subMenu" :index="item.path">
      <template #title>
        <i><svg-icon :prop-icon="item.meta.icon"></svg-icon></i>
        <span>{{ item.meta.title }}</span>
      </template>
      <!-- 还有children递归调用自身 -->
      <menu-item
        v-for="childItem in item.children"
        :key="childItem.path"
        :item="childItem"
      />
    </el-sub-menu>
  </div>
</template>

<script lang="ts" setup>
import { RouteRecordRawExt } from '@/router/typing'
import { ref } from 'vue'

interface propOption {
  item: RouteRecordRawExt
}

const props = defineProps<propOption>()

const lastChild = ref<RouteRecordRawExt>()

const hasOneShowChild = (children: RouteRecordRawExt[] = []) => {
  const showChild = children.filter(item => {
    if (item.meta.hidden) {
      return false
    }
    lastChild.value = item
    return true
  })

  /** 当只有一个子路由,直接显示，隐藏根目录 */
  if (showChild.length === 1) {
    return true
  }

  if (showChild.length === 0) {
    lastChild.value = props.item
    return true
  }
  return false
}
</script>
<style lang="scss" scope>
.svg-icon {
  margin-right: 15px;
}
</style>
