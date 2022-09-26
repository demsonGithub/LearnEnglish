<template>
  <el-dialog
    v-model="dialogEditCategoryVisible"
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
          <!-- action="https://run.mocky.io/v3/9d059bf9-4660-45f2-925d-ce80ad6c4d15" -->

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
        <el-button type="primary" @click="AddCategoryHandler">添加</el-button>
        <el-button @click="CancelHandler">取消</el-button>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script lang="ts" setup>
import fileOperationApi from '@/api/fileOperation'
import { IUploadFileParams } from '@/api/fileOperation/typing'
import type { UploadProps, UploadRequestHandler } from 'element-plus'
import { reactive } from 'vue'
import { IEditCategoryOption } from './typing'

interface IEditCategoryOptions {
  dialogEditCategoryVisible: boolean
  title: string
}

const props = defineProps<IEditCategoryOptions>()
const emits = defineEmits(['submitAddCategory', 'closeDialog'])

const form = reactive<IEditCategoryOption>({
  name: '',
  coverUrl: '',
  sequenceNum: 1,
})

const AddCategoryHandler = () => {
  emits('submitAddCategory', form)
}

const CancelHandler = () => {
  emits('closeDialog')
}

const uploadImg = async (params: any): Promise<UploadRequestHandler> => {
  console.log('222', params)
  let formData = new FormData()
  formData.append('file', params.file)

  let apiParams: IUploadFileParams = {
    file: formData,
  }

  var result = await fileOperationApi.uploadFile(apiParams)

  console.log('333', result)

  return result
}

const uploadSuccessHandle: UploadProps['onSuccess'] = (
  response,
  uploadFile
) => {}
</script>
<style lang="scss" scope>
.coverUpload {
  display: flex;
  width: 100%;
}
</style>
