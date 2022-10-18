<template>
  <div
    v-if="isExternal"
    :style="styleExternalIcon"
    class="svg-external-icon svg-icon"
    :class="propClass"
  ></div>
  <svg
    v-else
    class="svg-icon"
    :class="propClass"
    v-bind="attrs"
    aria-hidden="true"
  >
    <use :xlink:href="iconName" />
  </svg>
</template>

<script lang="ts" setup>
import { isExternal as external } from '@/utils/Validate'
import { computed, useAttrs } from 'vue'

export interface SvgIconProps {
  /**
   * 图标名称或地址
   */
  propIcon: string
  /** 自定义样式 */
  propClass?: string
}

const props = withDefaults(defineProps<SvgIconProps>(), {
  propClass: '',
})

const attrs = useAttrs()

// 图标
const isExternal = computed(() => external(props.propIcon))

// 外部样式
const styleExternalIcon = computed(() => ({
  mask: `url(${props.propIcon}) no-repeat 50% 50%`,
  '-webkit-mask': `url(${props.propIcon}) no-repeat 50% 50%`,
}))

// 内部样式
const iconName = computed(() => `#icon-${props.propIcon}`)
</script>

<style lang="scss" scope>
.svg-icon {
  width: 1em;
  height: 1em;
  vertical-align: -0.15em;
  fill: currentColor;
  overflow: hidden;
}

.svg-external-icon {
  background-color: currentColor;
  mask-size: cover !important;
  display: inline-block;
}
</style>
