<template>
  <el-dialog v-model="dialogVisible" title="Shipping address">
    <el-form :model="form">
      <el-form-item label="标题">
        <el-input v-model="form.title" />
      </el-form-item>
      <el-form-item label="描述">
        <el-input v-model="form.description" />
      </el-form-item>
      <el-form-item label="音频地址：">
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
    </el-form>
  </el-dialog>
</template>

<script lang="ts" setup>
import { ref } from 'vue'

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

const props = defineProps<IDialogEpisodeProps>()

const form = ref<IEditEpisodeOptions>()
</script>
<style lang="scss" scope>
.coverUpload {
  display: flex;
  width: 100%;
}
</style>
