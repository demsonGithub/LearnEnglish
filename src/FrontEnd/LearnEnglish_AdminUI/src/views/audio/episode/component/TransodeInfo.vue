<template>
  <div>
    <el-table :data="transcodeData" style="width: 100%" max-height="300">
      <el-table-column prop="title" label="标题" />
      <el-table-column prop="createTime" label="创建时间" />
      <el-table-column label="当前状态">
        <template #default="{ row }">
          <p class="transcode-state" :class="StateStyle(row.currentStatus)">
            <span>{{ StateString(row.currentStatus) }}</span>
          </p>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { TranscodeStatusEnum } from '../index'

export interface ITranscodeOptions {
  title: string
  createTime: string
  currentStatus: string
}

interface ITranscodeData {
  transcodeData: ITranscodeOptions[]
}

const props = defineProps<ITranscodeData>()

const transcodeData = computed(() => props.transcodeData)

const StateStyle = (state: Number) => {
  switch (state) {
    case TranscodeStatusEnum.ready:
      return 'state-ready'
    case TranscodeStatusEnum.start:
      return 'state-start'
    case TranscodeStatusEnum.completed:
      return 'state-completed'
    case TranscodeStatusEnum.failed:
      return 'state-failed'
  }
}

const StateString = (state: Number) => {
  switch (state) {
    case TranscodeStatusEnum.ready:
      return '等待中...'
    case TranscodeStatusEnum.start:
      return '正在转码...'
    case TranscodeStatusEnum.completed:
      return '已完成'
    case TranscodeStatusEnum.failed:
      return '转码失败'
  }
}
</script>
<style lang="scss" scope>
.transcode-state {
  width: 150px;
  border-radius: 15px;
  text-align: center;
  line-height: 30px;
  color: rgb(255, 255, 255);
}
.state-ready {
  background-color: rgb(240, 200, 60);
}
.state-start {
  background-color: rgb(57, 157, 255);
}
.state-completed {
  background-color: rgb(20, 210, 100);
}
.state-failed {
  background-color: rgb(250, 0, 0);
}
</style>
