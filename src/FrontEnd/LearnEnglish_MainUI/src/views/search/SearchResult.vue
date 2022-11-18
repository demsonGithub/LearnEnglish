<template>
  <div>
    <el-row v-for="(item, index) in listResult">
      <el-col>
        <div>
          <router-link :to="{ path: 'Episode', query: { id: item.episodeId } }">
            {{ item.title }}
          </router-link>
        </div>
        <div v-html="item.subtitles"></div>
        <el-divider></el-divider>
      </el-col>
    </el-row>
  </div>
</template>

<script lang="ts" setup>
import { useSearchStore } from '@/store/modules/SearchStore'
import { computed } from 'vue'

const store = useSearchStore()

const listResult = computed(() => store.getSearchList)
</script>
<style lang="scss" scoped>

</style>
