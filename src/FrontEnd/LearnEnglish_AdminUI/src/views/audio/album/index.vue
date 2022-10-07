<template>
  <div>
    <el-button type="primary" @click="handleAddAlbum()">新增专辑</el-button>
  </div>
  <div>
    <el-table :data="albumData">
      <el-table-column label="Id" prop="id"></el-table-column>
      <el-table-column label="名称" prop="title"></el-table-column>
      <el-table-column label="分类" prop="categoryName"></el-table-column>
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
            @click="handleUpdateAlbum(scope.row)"
          >
            编辑
          </el-button>
          <el-button size="small" type="danger">删除</el-button>
          <el-button size="small" @click="handleManageAlbum(scope.row)">
            管理音频
          </el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>
  <dialog-album
    :dialog-visible="dialogAlbumVisible"
    :edit-data="editData"
    @close-dialog="handleCloseDialog"
    @handle-submit="handleSubmitAlbum"
  ></dialog-album>
</template>

<script lang="ts" setup>
import { useParamStore } from '@/store/modules/paramStore'
import { onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import { albumApi } from '@/api/audio'
import DialogAlbum, { IEditAlbumOptions } from './component/DialogAlbum.vue'

//#region 接口定义
interface IAlbum {
  id: string
  title: string
  coverUrl: string
  categoryName: string
  sequenceNumber: number
  createTime: Date
}
//#endregion

const store = useParamStore()
const router = useRouter()
const currentCategoryId = ref<string>('')

//#region 查询

const albumData = ref<IAlbum[]>()

const queryAlbumList = async (categoryId: string) => {
  const resultList: IAlbum[] = []
  const apiParams: IQueryAlbumParams = {
    categoryId: currentCategoryId.value,
    title: '',
  }
  const apiResult = await albumApi.queryAlbumList(apiParams)
  apiResult.data.forEach((item: any) => {
    const albumObj: IAlbum = {
      id: item.id,
      title: item.title,
      coverUrl: item.coverUrl,
      categoryName: item.categoryName,

      sequenceNumber: item.sequenceNumber,
      createTime: item.createTime,
    }
    resultList.push(albumObj)
  })
  albumData.value = resultList
}

//#endregion

const dialogAlbumVisible = ref(false)
const editData = ref<IEditAlbumOptions>()

const handleSubmitAlbum = (params: IEditAlbumOptions) => {
  if (typeof params.id === 'undefined') {
    handleAddSubmit(params)
  } else {
    handleUpdateSubmit(params)
  }
}
//#region 新增

const handleAddAlbum = () => {
  editData.value = null
  dialogAlbumVisible.value = true
}
const handleAddSubmit = async (params: IEditAlbumOptions) => {
  const apiParams: IAddAlbumParams = {
    title: params.title,
    coverUrl: params.coverUrl,
    sequenceNumber: params.sequenceNumber,
    categoryId: currentCategoryId.value,
  }
  const result = await albumApi.addAlbum(apiParams)

  dialogAlbumVisible.value = false
  await queryAlbumList(currentCategoryId.value)
}
//#endregion

//#region 修改

const handleUpdateAlbum = (row: IAlbum) => {
  editData.value = row
  dialogAlbumVisible.value = true
}

const handleUpdateSubmit = async (params: IEditAlbumOptions) => {
  const apiParams: IUpdateAlbumParams = {
    id: params.id,
    title: params.title,
    coverUrl: params.coverUrl,
    sequenceNumber: params.sequenceNumber,
  }
  const result = await albumApi.updateAlbum(apiParams)

  dialogAlbumVisible.value = false
  await queryAlbumList(currentCategoryId.value)
}

//#endregion

const handleCloseDialog = () => {
  dialogAlbumVisible.value = false
}

//#region 管理音频
const handleManageAlbum = (row: IAlbum) => {
  const params = {
    albumId: row.id,
  }
  store.setParams(params)
  router.push({ name: 'episode' })
}
//#endregion

onMounted(() => {
  if (
    store.getParams == null ||
    typeof store.getParams.categoryId === 'undefined'
  ) {
    router.push({ name: 'category' })
    return
  }
  currentCategoryId.value = store.getParams.categoryId
  queryAlbumList(currentCategoryId.value)
})
</script>
<style lang="scss" scope></style>
