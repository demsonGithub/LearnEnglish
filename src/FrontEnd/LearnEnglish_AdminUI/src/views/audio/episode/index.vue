<template>
  <div class="episode-wrapper">
    <div>
      <el-button type="success" @click="handleAddEpisode">添加音频</el-button>
    </div>
    <div style="margin-top: 15px">
      <el-table :data="episodeData" border style="width: 100%">
        <el-table-column
          prop="title"
          label="标题"
          width="120"
        ></el-table-column>
        <el-table-column
          prop="description"
          label="描述"
          width="240"
        ></el-table-column>
        <el-table-column
          prop="sequenceNumber"
          label="排序"
          width="60"
        ></el-table-column>
        <el-table-column prop="audioUrl" label="音频源地址"></el-table-column>
        <el-table-column
          prop="durationInSecond"
          label="时长(秒)"
          width="120"
        ></el-table-column>
        <el-table-column label="操作" width="140">
          <template #default="scope">
            <el-button size="small" @click="handleEdit(scope.row)">
              编辑
            </el-button>
            <el-button
              size="small"
              type="danger"
              @click="handleDelete(scope.row)"
            >
              删除
            </el-button>
          </template>
        </el-table-column>
      </el-table>
    </div>
    <dialog-episode
      :dialog-visible="dialogVisible"
      :edit-data="editData"
      @close-dialog="closeDialog"
      @handle-submit="handleSubmitEpisode"
    ></dialog-episode>
  </div>
</template>

<script lang="ts" setup>
import { episodeApi } from '@/api/audio'
import { apiResultCode } from '@/api/request'
import { useParamStore } from '@/store/modules/paramStore'
import { ElMessage } from 'element-plus'
import { onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import DialogEpisode, {
  IEditEpisodeOptions,
} from './component/DialogEpisode.vue'

interface IEpisodeDetial {
  id: number
  title: string
  description: string
  sequenceNumber: number
  audioUrl: string
  durationInSecond: number
  subtitles: string
  isVisible: boolean
  albumId: number
}

const store = useParamStore()
const router = useRouter()

const currentAlbumId = ref()

const episodeData = ref<IEpisodeDetial[]>()

//#region 查询
const queryEpisodeList = async (albumId: string) => {
  let apiParams: IQueryEpisodeParams = {
    albumId: albumId,
  }
  const result = await episodeApi.queryEpisodeList(apiParams)
  episodeData.value = result.data
}
//#endregion

const dialogVisible = ref<boolean>(false)
const editData = ref<IEditEpisodeOptions>()
const closeDialog = () => {
  dialogVisible.value = false
}

const handleSubmitEpisode = (params: IEditEpisodeOptions) => {
  if (typeof params.id === 'undefined') {
    handleAddSubmit(params)
  } else {
    handleUpdateSubmit(params)
  }
}
//#region 新增
const handleAddEpisode = () => {
  editData.value = null
  dialogVisible.value = true
}

const handleAddSubmit = async (params: IEditEpisodeOptions) => {
  const apiParams: IAddEpisodeParams = {
    title: params.title,
    description: params.description,
    sequenceNumber: params.sequenceNumber,
    audioUrl: params.audioUrl,
    durationInSecond: params.audioDuration,
    subtitles: params.subtitles,
    albumId: currentAlbumId.value,
  }
  const result = await episodeApi.addEpisode(apiParams)

  if (result.code === apiResultCode.fail) {
    console.log(result)

    ElMessage.error(result.msg)
  }

  queryEpisodeList(currentAlbumId.value)
  dialogVisible.value = false
}
//#endregion

//#region 修改
const handleEdit = (row: IEpisodeDetial) => {
  console.log(row)
}

const handleUpdateSubmit = (params: IEditEpisodeOptions) => {}
//#endregion

//#region 删除

const handleDelete = (params: IEpisodeDetial) => {}
//#endregion

onMounted(() => {
  if (
    store.getParams === null ||
    typeof store.getParams.albumId === 'undefined'
  ) {
    router.push({ name: 'category' })
    return
  }

  currentAlbumId.value = store.getParams.albumId
  queryEpisodeList(currentAlbumId.value)
})
</script>
<style lang="scss" scope></style>
