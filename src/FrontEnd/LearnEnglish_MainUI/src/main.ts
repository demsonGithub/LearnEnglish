import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import piniaStore from './store'
import { SvgIcon } from '@/components/SvgIcon/index'

import 'normalize.css/normalize.css'

const app = createApp(App)
app.component('SvgIcon', SvgIcon)

app.use(router)
app.use(piniaStore)

app.mount('#app')
