<template>
  <div
    v-if="isExternal"
    :style="styleExternalIcon"
    class="svg-external-icon svg-icon"
    :class="addClass"
  ></div>
  <svg
    v-else
    class="svg-icon"
    :class="addClass"
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
  icon: string
  addClass?: string
}

const props = withDefaults(defineProps<SvgIconProps>(), {
  addClass: '',
})

const attrs = useAttrs()

// 图标
const isExternal = computed(() => external(props.icon))

// 外部样式
const styleExternalIcon = computed(() => ({
  mask: `url(${props.icon}) no-repeat 50% 50%`,
  '-webkit-mask': `url(${props.icon}) no-repeat 50% 50%`,
}))

// 内部样式
const iconName = computed(() => `#icon-${props.icon}`)
</script>

<style lang="scss" scope>
.svg-icon {
  width: 1em;
  height: 1em;
  vertical-align: middle;
  fill: currentColor;
  overflow: hidden;
}

.svg-external-icon {
  background-color: currentColor;
  mask-size: cover !important;
  display: inline-block;
}
</style>
