<template>
  <el-dialog
    v-model="dialogVisible"
    :title="title"
    width="30%"
    draggable
    :before-close="CancelHandler"
    :show-close="false"
  >
    <el-form :model="form" label-width="120px">
      <el-form-item label="分类名称：">
        <el-input v-model="form.name" />
      </el-form-item>
      <el-form-item label="封面地址：">
        <div class="coverUpload">
          <el-input v-model="form.coverUrl" />
          <el-upload
            :show-file-list="false"
            :http-request="uploadImg"
            :on-success="uploadSuccessHandle"
          >
            <el-button>选择图片</el-button>
          </el-upload>
        </div>
      </el-form-item>
      <el-form-item label="排序编号：">
        <el-input v-model="form.sequenceNum" />
      </el-form-item>
      <el-form-item>
        <el-button
          type="primary"
          @click="AddCategoryHandler"
          v-text="btnContent"
        ></el-button>
        <el-button @click="CancelHandler">取消</el-button>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script lang="ts" setup>
import fileOperationApi from '@/api/fileOperation'
import type { UploadProps, UploadRequestHandler } from 'element-plus'
import { computed, reactive, watch } from 'vue'
import { IEditCategoryOption } from './typing'

interface IEditCategoryOptions {
  dialogVisible: boolean
  title: string
  editObj?: IEditCategoryOption
}

const props = defineProps<IEditCategoryOptions>()
const emits = defineEmits(['submitAddCategory', 'closeDialog'])

const form = reactive<IEditCategoryOption>({
  name: props.editObj?.name,
  coverUrl: props.editObj?.coverUrl,
  sequenceNum: props.editObj?.sequenceNum,
})

watch(
  () => props.editObj,
  (newValue, oldValue) => {
    form.name = newValue?.name
    form.coverUrl = newValue?.coverUrl
    form.sequenceNum = newValue?.sequenceNum
  }
)

const btnContent = computed(() =>
  typeof props.editObj === 'undefined' ? '新增' : '修改'
)

const AddCategoryHandler = () => {
  emits('submitAddCategory', form)
}

const CancelHandler = () => {
  emits('closeDialog')
}

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
  form.coverUrl = response.remoteUrl
}
</script>
<style lang="scss" scope>
.coverUpload {
  display: flex;
  width: 100%;
}
</style>
