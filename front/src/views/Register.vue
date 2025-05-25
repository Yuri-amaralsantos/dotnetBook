<template>
  <div>
    <h2>Registrar</h2>
    <form @submit.prevent="register">
      <input v-model="username" placeholder="Usuário" required />
      <input v-model="password" type="password" placeholder="Senha" required />
      <button type="submit">Registrar</button>
    </form>
    <p>{{ message }}</p>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import API from '../services/api'

const username = ref('')
const password = ref('')
const message = ref('')

const register = async () => {
  try {
    await API.post('/auth/register', {
      username: username.value,
      passwordHash: password.value,
    })
    message.value = 'Registrado com sucesso!'
  } catch (error) {
    message.value = 'Erro ao registrar'
  }
}
</script>
