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
    case 0:
      return 'stateOne'
    case 1:
      return 'stateTwo'
    case -1:
      return 'stateThree'
  }
}

const StateString = (state: Number) => {
  switch (state) {
    case 0:
      return '开始转码'
    case 1:
      return '转码完成'
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
.stateOne {
  background-color: rgb(57, 157, 255);
}
.stateTwo {
  background-color: rgb(100, 200, 60);
}
.stateThree {
  background-color: rgb(250, 0, 0);
}
</style>
