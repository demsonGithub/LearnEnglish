<template>
  <search-result></search-result>
</template>
<script lang="ts" setup name="search">
import SearchResult from './SearchResult.vue'
import { ElMessage } from 'element-plus'
import { computed, onMounted, watch } from 'vue'
import { useRoute } from 'vue-router'
import searchApi from '@/api/search'
import { apiCode } from '@/api/request'
import { useSearchStore } from '@/store/modules/SearchStore'

const store = useSearchStore()
const route = useRoute()
const keyword = computed(() => route.query.keyword as string)

const search = async (parameter: string) => {
  if (keyword.value.trim() === '') {
    ElMessage.error('无搜索关键字')
    return
  }
  let apiParams: ISearchParams = {
    keyword: parameter,
    pageIndex: 1,
    pageSize: 10,
  }

  const result = await searchApi.searchByKeyword(apiParams)

  console.log('1', result)
  // if (result.code == apiCode.success) {
  store.setSearchResult(result.episodes)
  // }
}

watch(
  () => route.query.keyword,
  (current, before) => {
    if (typeof keyword.value !== 'undefined') search(current as string)
  }
)
onMounted(() => {
  search(keyword.value)
})
</script>
<style lang="scss" scoped></style>
