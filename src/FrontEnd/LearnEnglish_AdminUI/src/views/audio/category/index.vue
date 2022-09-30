<template>
  <div>
    <el-button type="primary" @click="handleAddCategory()">新增分类</el-button>
  </div>
  <div>
    <el-table :data="categoryData">
      <el-table-column label="Id" prop="id"></el-table-column>
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
          <el-button
            size="small"
            type="warning"
            @click="handleUpdateCategory(scope.row)"
          >
            编辑
          </el-button>
          <el-button size="small" type="danger">删除</el-button>
          <el-button size="small" @click="handleManageAlbum(scope.row)">
            管理专辑
          </el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>

  <edit-category
    :dialog-visible="dialogVisible"
    :edit-data="editData"
    @close-dialog="closeDialog"
    @handleEditCategory="handleEditCategory"
  ></edit-category>
</template>

<script lang="ts" setup name="sound">
import { apiResultCode } from '@/api/request'
import { onMounted, ref } from 'vue'
import { IEditCategoryOptions } from './components/DialogCategory.vue'
import EditCategory from './components/DialogCategory.vue'
import { useRouter } from 'vue-router'
import { useParamStore } from '@/store/modules/paramStore'
import { categoryApi } from '@/api/audio'

//#region  接口定义
interface ICategory {
  id: string
  title: string
  coverUrl: string
  sequenceNumber: number
  createTime: Date
}

//#endregion

const router = useRouter()
const store = useParamStore()

//#region 查询
const categoryData = ref<ICategory[]>([])

const queryCategoryList = async () => {
  const resultList: ICategory[] = []
  const apiParams: IQueryCategoryParams = {
    title: '',
  }
  const result = await categoryApi.queryCategoryList(apiParams)

  if (result.code === apiResultCode.fail) {
    categoryData.value = resultList
    return
  }

  result.data.forEach((item: any) => {
    const categoryObj: ICategory = {
      id: item.id,
      title: item.title,
      sequenceNumber: item.sequenceNumber,
      createTime: item.createTime,
      coverUrl: '',
    }
    resultList.push(categoryObj)
  })

  categoryData.value = resultList
}
//#endregion

const dialogVisible = ref<boolean>(false)
const editData = ref<IEditCategoryOptions>()

const closeDialog = () => {
  dialogVisible.value = false
}

const handleEditCategory = (params: IEditCategoryOptions) => {
  if (typeof params.id === 'undefined') {
    handleAddSubmit(params)
  } else {
    handleUpdateSubmit(params)
  }
}

//#region 新增
const handleAddCategory = () => {
  editData.value = null
  dialogVisible.value = true
}

const handleAddSubmit = async (params: IEditCategoryOptions) => {
  const apiParams: IAddCategoryParams = {
    title: params.title,
    coverUrl: params.coverUrl,
    sequenceNumber: params.sequenceNumber,
  }

  await categoryApi.AddCategory(apiParams)

  dialogVisible.value = false

  await queryCategoryList()
}
//#endregion

//#region 修改
const handleUpdateCategory = (params: ICategory) => {
  dialogVisible.value = true
  editData.value = params
}

const handleUpdateSubmit = async (params: IEditCategoryOptions) => {
  const apiParams: IUpdateCategoryParams = {
    id: params.id,
    title: params.title,
    coverUrl: params.coverUrl,
    sequenceNumber: params.sequenceNumber,
  }
  await categoryApi.UpdateCategory(apiParams)

  dialogVisible.value = false

  await queryCategoryList()
}

//#endregion

//#region 管理专辑
const handleManageAlbum = (row: ICategory) => {
  const params = {
    categoryId: row.id,
  }
  store.setParams(params)
  router.push({ name: 'album' })
}
//#endregion
onMounted(() => {
  queryCategoryList()
})
</script>
<style lang="scss" scope></style>
