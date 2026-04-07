import { createApp } from 'vue'
import { createPinia } from 'pinia'

import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap-icons/font/bootstrap-icons.css'

import App from './App.vue'
import router from './router'

// Inicializa la aplicación Vue.
const app = createApp(App)

app.use(createPinia())
app.use(router)

app.mount('#app')
