<template>
  <div>
    <h2>Login</h2>
    <form @submit.prevent="login">
      <input v-model="username" placeholder="Usuário" required />
      <input v-model="password" type="password" placeholder="Senha" required />
      <button type="submit">Entrar</button>
    </form>
    <p v-if="message">{{ message }}</p>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import API from '../services/api'
import useAuth from '../components/useAuth'


const router = useRouter()

const username = ref('')
const password = ref('')
const message = ref('')
const { setToken } = useAuth()

const login = async () => {
  try {
    const res = await API.post('/auth/login', {
      username: username.value,
      passwordHash: password.value,
    })
    setToken(res.data.token)
    message.value = 'Login realizado!'
    router.push('/books')
  } catch (error) {
    console.error('Erro no login:', error.response?.data || error.message)
    message.value = 'Usuário ou senha inválidos'
  }
}
</script>

<style scoped>
input {
  max-width:400px;
  margin: 20px;
}
</style>
