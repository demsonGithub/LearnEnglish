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
          <el-input v-model="form.coverUrl" :readonly="true" />
          <el-upload
            :show-file-list="false"
            :http-request="uploadFile"
            :on-success="uploadSuccessHandle"
            :on-error="uploadError"
          >
            <el-button>上传图片</el-button>
          </el-upload>
        </div>
        <div class="progress">
          <el-progress :percentage="progressValue" :status="progressStatus" />
        </div>
      </el-form-item>
      <el-form-item>
        <el-button
          type="primary"
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
import { UploadFile, UploadProps, UploadRequestHandler } from 'element-plus'
import { onMounted, ref, watch } from 'vue'
import * as signalR from '@microsoft/signalr'

export interface IEditEpisodeOptions {
  id?: string
  title: string
  description: string
  coverUrl: string
  sequenceNumber: number
}

interface IDialogEpisodeProps {
  dialogVisible: boolean
  editData?: IEditEpisodeOptions
}
const form = ref<IEditEpisodeOptions>()

const progressValue = ref(0)
const progressStatus = ref()

const props = defineProps<IDialogEpisodeProps>()
const emits = defineEmits(['closeDialog', 'handleSubmit'])

watch(
  () => props.editData,
  () => {
    if (props.editData === null) {
      form.value = {
        title: '',
        description: '',
        coverUrl: '',
        sequenceNumber: null,
      }
    } else {
      form.value = props.editData
    }
  }
)

const handleSubmit = () => {
  send()
}

const handleCancel = () => {
  emits('closeDialog')
}

const uploadFile = async (params: any): Promise<UploadRequestHandler> => {
  const formData = new FormData()
  formData.append('file', params.file)

  const result = await fileOperationApi.uploadFile(formData)

  return result.data
}

const uploadSuccessHandle: UploadProps['onSuccess'] = (
  response,
  uploadFile
) => {
  form.value.coverUrl = response.remoteUrl
  progressValue.value = 100
  progressStatus.value = 'success'
}

const uploadError = (error: Error, uploadFile: UploadFile) => {
  progressStatus.value = 'exception'
}

const init = () => {
  const connection = new signalR.HubConnectionBuilder()
    .withUrl('http://localhost:8082/Hubs/FileUploadStatusHub', {
      skipNegotiation: true,
      transport: 1, // 强制WebSockets
    })
    .build()

  connection.on('RecieveMessage', data => {
    let [user, message] = data
    console.log(user, message)
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
