<template>
  <el-dialog
    v-model="dialogVisible"
    :title="editData === null ? '新增音频' : '修改音频'"
    width="50%"
    draggable
    :close-on-click-modal="false"
    :show-close="true"
    @close="handleCancel"
  >
    <el-form :model="form">
      <el-form-item label="标&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;题：">
        <el-input v-model="form.title" />
      </el-form-item>
      <el-form-item label="描&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;述：">
        <el-input v-model="form.description" />
      </el-form-item>
      <el-form-item label="音频地址：">
        <div class="coverUpload">
          <el-input v-model="form.audioUrl" :readonly="true" />
          <el-upload
            :show-file-list="false"
            :http-request="uploadFile"
            :on-success="uploadSuccessHandle"
          >
            <el-button>上传音频</el-button>
          </el-upload>
        </div>
        <div class="progress">
          <el-progress :percentage="progressValue" :status="progressStatus" />
        </div>
      </el-form-item>
      <el-form-item label="音频时长：">
        <el-input v-model="form.audioDuration" />
      </el-form-item>
      <el-form-item label="字&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;幕：">
        <div style="display: flex; width: 100%">
          <el-input v-model="form.subtitles" type="textarea" :rows="5" />

          <el-upload
            :show-file-list="false"
            :http-request="analysisSubtitlesFile"
            :on-success="analysisSubtitlesSuccessHandle"
          >
            <el-button type="primary">选择文件</el-button>
          </el-upload>
        </div>
      </el-form-item>
      <el-form-item label="排序编号：">
        <el-input v-model="form.sequenceNumber" />
      </el-form-item>
      <el-form-item>
        <el-button
          type="primary"
          :disabled="form.audioUrl === ''"
          @click="handleSubmit"
          v-text="editData === null ? '添加' : '修改'"
        ></el-button>
        <el-button @click="handleCancel">取消</el-button>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script lang="ts" setup>
import fileOperationApi from '@/api/fileOperation'
import { UploadProps, UploadRequestHandler } from 'element-plus'
import { computed, onMounted, ref } from 'vue'
import * as signalR from '@microsoft/signalr'
import { apiResultCode } from '@/api/request'
import { episodeApi } from '@/api/audio'

export interface IEditEpisodeOptions {
  id?: string
  title: string
  description: string
  sequenceNumber: number
  audioUrl: string
  audioDuration: number
  subtitles: string
}

interface IDialogEpisodeProps {
  dialogVisible: boolean
  editData?: IEditEpisodeOptions
}

const progressValue = ref(0)
const progressStatus = ref()

const props = defineProps<IDialogEpisodeProps>()
const emits = defineEmits(['closeDialog', 'handleSubmit'])

const form = computed(() => props.editData)

const analysisSubtitlesFile = async (params: any) => {
  const subtitlesData = new FormData()
  subtitlesData.append('file', params.file)
  const result = await episodeApi.analysisSubtitles(subtitlesData)

  return result.data
}
const analysisSubtitlesSuccessHandle = (response: any) => {
  form.value.subtitles = response
}

const handleSubmit = () => {
  emits('handleSubmit', form.value)
}

const handleCancel = () => {
  emits('closeDialog')
}

const signalrId = ref('')

const uploadFile = async (params: any): Promise<UploadRequestHandler> => {
  progressStatus.value = null
  progressValue.value = 0
  const formData = new FormData()
  formData.append('file', params.file)
  formData.append('identityId', signalrId.value)

  const result = await fileOperationApi.uploadFile(formData)
  getAudioDuration(params.file)

  if (result.code === apiResultCode.fail) {
    progressStatus.value = 'exception'
  }

  return result.data
}

const uploadSuccessHandle: UploadProps['onSuccess'] = (
  response,
  uploadFile
) => {
  form.value.audioUrl = response.remoteUrl
  progressValue.value = 100
  progressStatus.value = 'success'
}

//获取时长的函数
const getAudioDuration = (file: Blob | MediaSource) => {
  const url = URL.createObjectURL(file)
  const audioElement = new Audio(url)

  audioElement.addEventListener('loadedmetadata', function () {
    form.value.audioDuration = audioElement.duration
  })
}

const init = async () => {
  const connection = new signalR.HubConnectionBuilder()
    .withUrl('http://localhost:5051/Gateway/UploadFileHub', {
      skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets, // 强制WebSockets
      logger: signalR.LogLevel.Error,
    })
    .build()

  // 连接回调函数
  connection.on('ConnectCallback', connId => {
    console.log(connId)

    signalrId.value = connId
  })

  connection.on('RecieveMessage', data => {
    progressValue.value = parseInt(data)
  })

  connection.start()
}

onMounted(() => {
  init()
})
</script>
<style lang="scss" scope>
.coverUpload {
  display: flex;
  width: 100%;
}
.progress {
  margin-bottom: 15px;
  margin-top: 10px;
  width: 100%;
}
</style>
