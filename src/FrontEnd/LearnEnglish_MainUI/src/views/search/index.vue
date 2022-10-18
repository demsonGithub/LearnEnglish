<template>搜索结果</template>
<script lang="ts" setup name="search">
import { ElMessage } from 'element-plus'
import { computed, onMounted, watch } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const keyword = computed(() => route.query.keyword as string)

const search = (parameter: string) => {
  if (keyword.value.trim() === '') {
    ElMessage.error('无搜索关键字')
    return
  }
  alert(parameter)
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
