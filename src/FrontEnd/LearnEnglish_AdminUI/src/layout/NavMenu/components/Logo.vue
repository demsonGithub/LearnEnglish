<template>
  <div class="sidebar-logo-container" :class="{ collapse: collapse }">
    <router-link key="expand" class="sidebar-logo-link" to="/">
      <img v-if="logoUrl" :src="logoUrl" class="sidebar-logo" />
      <span class="sidebar-title">{{ title }}</span>
    </router-link>
  </div>
</template>

<script lang="ts" setup>
import { getImageUrl } from '@/utils/image'

const props = defineProps({
  collapse: {
    type: Boolean,
    default: false,
  },
})
const logoUrl = getImageUrl('logo.png')
const title = 'LearnEnglish'
</script>
<style lang="scss" scope>
.sidebar-logo-container {
  height: $headerHeight;
  background-color: $logoBgColor;

  .sidebar-logo-link {
    width: 100%;
    height: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
    text-decoration: none;

    .sidebar-logo {
      border-radius: 50%;
      width: 50px;
    }
    .sidebar-title {
      font-size: 20px;
      font-weight: 700;
      color: rgb(255, 255, 255);
      margin-left: 10px;
    }
  }
}
</style>
