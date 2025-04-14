import './assets/main.css'
import 'primeicons/primeicons.css'
import 'leaflet/dist/leaflet.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import Toast from 'vue-toastification'
import 'vue-toastification/dist/index.css'
import { createPinia } from 'pinia'

const app = createApp(App)

app.use(router)
app.use(Toast)
app.use(createPinia())

app.mount('#app')
