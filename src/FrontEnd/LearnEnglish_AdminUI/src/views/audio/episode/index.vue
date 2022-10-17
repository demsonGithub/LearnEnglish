<template>
  <div class="episode-wrapper">
    <div>
      <el-button type="success" @click="handleAddEpisode">添加音频</el-button>
    </div>
    <div v-show="transcodeData.length > 0">
      <transode-info :transcode-data="transcodeData" />
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
    <div v-if="dialogVisible">
      <dialog-episode
        :dialog-visible="dialogVisible"
        :edit-data="editData"
        @close-dialog="closeDialog"
        @handle-submit="handleSubmitEpisode"
      ></dialog-episode>
    </div>
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
import TransodeInfo, { ITranscodeOptions } from './component/TransodeInfo.vue'
import * as signalR from '@microsoft/signalr'
import { TranscodeStatusEnum } from './index'

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
  const apiParams: IQueryEpisodeParams = {
    albumId: albumId,
  }
  const result = await episodeApi.queryEpisodeList(apiParams)
  episodeData.value = result.data
}
//#endregion

const dialogVisible = ref(false)
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
  editData.value = {
    title: '',
    description: '',
    audioUrl: '',
    audioDuration: null,
    sequenceNumber: null,
    subtitles: '',
  }
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

const transcodeData = ref<ITranscodeOptions[]>([])

const init = () => {
  if (
    store.getParams === null ||
    typeof store.getParams.albumId === 'undefined'
  ) {
    router.push({ name: 'category' })
    return
  }

  currentAlbumId.value = store.getParams.albumId
  queryEpisodeList(currentAlbumId.value)
}

const signalrId = ref('')

const initSignalR = () => {
  const connection = new signalR.HubConnectionBuilder()
    .withUrl('http://localhost:8083/Hubs/TranscodeFileHub', {
      skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets, // 强制WebSockets
      logger: signalR.LogLevel.Error,
    })
    .build()

  // 连接回调函数
  connection.on('ConnectCallback', connId => {
    signalrId.value = connId
  })

  connection.on('RecieveMessage', data => {
    const { transcodeStatus, title, createTime } = data
    const entity = transcodeData.value.find(item => item.title === title)

    switch (transcodeStatus) {
      case TranscodeStatusEnum.ready:
        transcodeData.value.push({
          title: title,
          createTime: createTime,
          currentStatus: transcodeStatus,
        })

        break
      case TranscodeStatusEnum.start:
        entity.currentStatus = transcodeStatus

        break
      case TranscodeStatusEnum.completed:
        entity.currentStatus = transcodeStatus
        queryEpisodeList(currentAlbumId.value)

        break
      case TranscodeStatusEnum.failed:
        entity.currentStatus = transcodeStatus
        console.log('TranscodeStatus Error', data)
        break
      default:
        console.log('TranscodeStatus Not Found', data)
        break
    }
  })

  connection.start()
}

onMounted(() => {
  init()
  initSignalR()
})
</script>
<style lang="scss" scope></style>
