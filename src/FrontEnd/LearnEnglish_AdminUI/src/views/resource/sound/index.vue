<template>
  <div>
    <el-button type="primary" @click="AddCategoryHandler()">新增分类</el-button>
  </div>
  <div>
    <el-table :data="categoryData">
      <el-table-column label="名称" prop="title"></el-table-column>
      <el-table-column
        label="序号"
        prop="sequenceNumber"
        width="180"
      ></el-table-column>
      <el-table-column
        label="创建日期"
        prop="createTime"
        width="200"
      ></el-table-column>
      <el-table-column label="操作" width="300">
        <template>
          <el-button size="small">Edit</el-button>
          <el-button size="small" type="danger">Delete</el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script lang="ts" setup name="sound">
import { apiResultCode } from '@/api/request'
import { categoryApi } from '@/api/resource'
import { IQueryCategoryParams } from '@/api/resource/typing'
import { onMounted, ref } from 'vue'

interface Category {
  title: string
  sequenceNumber: number
  createTime: Date
}

const categoryData = ref<Category[]>([])

const queryCategoryList = async () => {
  const resultList: Category[] = []
  const apiParams: IQueryCategoryParams = {
    title: '',
  }
  const result = await categoryApi.queryCategoryList(apiParams)

  if (result.code === apiResultCode.fail) {
    categoryData.value = resultList
    return
  }

  result.data.forEach((item: any) => {
    const categoryObj: Category = {
      title: item.title,
      sequenceNumber: item.sequenceNumber,
      createTime: item.createTime,
    }
    resultList.push(categoryObj)
  })
  categoryData.value = resultList
}

const AddCategoryHandler = () => {}

onMounted(() => {
  queryCategoryList()
})
</script>
<style lang="scss" scope></style>
