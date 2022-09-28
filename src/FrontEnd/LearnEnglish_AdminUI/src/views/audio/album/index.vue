<template>
  <div>
    <el-button type="primary" @click="handleAddAlbum()">新增专辑</el-button>
  </div>
  <div>
    <el-table :data="albumData">
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
</template>

<script lang="ts" setup>
import { useParamStore } from '@/store/modules/paramStore'
import { onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import { albumApi } from '@/api/audio'

//#region 接口定义
interface IAlbum {
  id: string
  title: string
  coverUrl: string
  sequenceNumber: number
  createTime: Date
}
//#endregion

const paramStore = useParamStore()
const router = useRouter()
const currentCategoryId = ref<string>('')

//#region 查询

const albumData = ref<IAlbum[]>()

const queryAlbumList = async (categoryId: string) => {
  let resultList: IAlbum[] = []
  let apiParams: IQueryAlbumParams = {
    categoryId: currentCategoryId.value,
    title: '',
  }
  const apiResult = await albumApi.queryAlbumList(apiParams)
  apiResult.data.forEach((item: any) => {
    const albumObj: IAlbum = {
      id: item.id,
      title: item.title,
      coverUrl: item.coverUrl,
      sequenceNumber: item.sequenceNumber,
      createTime: item.createTime,
    }
    resultList.push(albumObj)
  })
  albumData.value = resultList
}

//#endregion

//#region 新增

const handleAddAlbum = () => {}
//#endregion

//#region 修改

const handleUpdateAlbum = (row: IAlbum) => {}
//#endregion

//#region 管理音频
const handleManageAlbum = (row: IAlbum) => {}
//#endregion

onMounted(() => {
  if (paramStore.getParams === null) {
    router.push({ name: 'category' })
    return
  }
  currentCategoryId.value = paramStore.getParams.categoryId
  queryAlbumList(currentCategoryId.value)
})
</script>
<style lang="scss" scope></style>
