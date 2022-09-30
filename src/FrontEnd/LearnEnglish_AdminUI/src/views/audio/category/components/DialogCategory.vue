<template>
  <el-dialog
    v-model="dialogVisible"
    :title="editData === null ? '新增分类' : '修改分类'"
    width="50%"
    draggable
    :close-on-click-modal="false"
    :show-close="true"
    @close="handleCancel"
  >
    <el-form :model="form" label-width="120px">
      <el-form-item label="分类名称：">
        <el-input v-model="form.title" />
      </el-form-item>
      <el-form-item label="封面地址：">
        <div class="coverUpload">
          <el-input v-model="form.coverUrl" :readonly="true" />
          <el-upload
            :show-file-list="false"
            :http-request="uploadImg"
            :on-success="uploadSuccessHandle"
            :on-error="uploadError"
          >
            <el-button>上传图片</el-button>
          </el-upload>
        </div>
      </el-form-item>
      <el-form-item label="排序编号：">
        <el-input
          v-model="form.sequenceNumber"
          type="number"
          min="1"
          max="99"
        />
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
import type {
  UploadFile,
  UploadProps,
  UploadRequestHandler,
} from 'element-plus'
import { ref, watch } from 'vue'

export interface IEditCategoryOptions {
  id?: string
  title: string
  coverUrl: string
  sequenceNumber: number
}

interface IDialogCategoryProps {
  dialogVisible: boolean
  editData?: IEditCategoryOptions
}

const props = defineProps<IDialogCategoryProps>()
const emits = defineEmits(['closeDialog', 'handleEditCategory'])

const form = ref<IEditCategoryOptions>()

watch(
  () => props.editData,
  () => {
    reset()
    if (props.editData !== null) {
      form.value = props.editData
    }
  }
)

const reset = () => {
  if (props.editData === null) {
    form.value = {
      title: '',
      coverUrl: '',
      sequenceNumber: null,
    }
  }
}

const handleCancel = () => {
  emits('closeDialog')
}

const handleSubmit = () => {
  emits('handleEditCategory', form.value)
}

//#region 处理上传图片
const uploadImg = async (params: any): Promise<UploadRequestHandler> => {
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
}

const uploadError = (error: Error, uploadFile: UploadFile) => {}
//#endregion
</script>
<style lang="scss" scope>
.coverUpload {
  display: flex;
  width: 100%;
}
</style>
