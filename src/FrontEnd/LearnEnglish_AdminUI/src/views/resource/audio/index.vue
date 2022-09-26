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
        <template #default="scope">
          <el-button size="small" type="warning" @click="editHandle(scope.row)">
            编辑
          </el-button>
          <el-button size="small" type="danger">删除</el-button>
          <el-button size="small">管理专辑</el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>

  <edit-category
    :dialogEditCategoryVisible="dialogVisible"
    :title="dialogTitle"
    @submit-add-category="submitAddCategory"
    @close-dialog="closeDialog"
  ></edit-category>
</template>

<script lang="ts" setup name="sound">
import { apiResultCode } from '@/api/request'
import { categoryApi } from '@/api/resource'
import { IAddCategoryParams, IQueryCategoryParams } from '@/api/resource/typing'
import { onMounted, ref } from 'vue'
import EditCategory from './components/EditCategory.vue'
import { IEditCategoryOption } from './components/typing'

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

const dialogVisible = ref(false)
const dialogTitle = ref('')

const AddCategoryHandler = () => {
  dialogTitle.value = '新增分类'
  dialogVisible.value = true
}

const closeDialog = () => {
  dialogVisible.value = false
}

const submitAddCategory = async (params: IEditCategoryOption) => {
  let apiParams: IAddCategoryParams = {
    name: params.name,
    coverUrl: params.coverUrl,
    sequenceNumber: params.sequenceNum,
  }
  var result = await categoryApi.AddCategory(apiParams)

  dialogVisible.value = false

  await queryCategoryList()
}

const editHandle = params => {
  console.log('aa', params)
}

onMounted(() => {
  queryCategoryList()
})
</script>
<style lang="scss" scope></style>
