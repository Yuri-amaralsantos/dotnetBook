<template>
  <div id="app">
    <h1>Autenticação com JWT</h1>

    <nav>
      <router-link v-if="!isAuthenticated" to="/login">Login</router-link> 
      <router-link v-if="!isAuthenticated" to="/register">Registrar</router-link> 
      <router-link v-if="isAuthenticated" to="/books">Livros</router-link> 
      <router-link v-if="isAuthenticated" to="/mybooks">Meus Livros</router-link>
      <router-link v-if="isAuthenticated && isAdmin" to="/admin/books">Gerenciar Livros</router-link> 
      <button v-if="isAuthenticated" @click="logout">Sair</button>
    </nav>

    <router-view />
  </div>
</template>

<script setup>
import useAuth from './components/useAuth'
import { useRouter } from 'vue-router'
import { computed } from 'vue'

const router = useRouter()
const auth = useAuth()

const isAuthenticated = computed(() => auth.isAuthenticated.value)
const isAdmin = computed(() => auth.isAdmin.value)

const logout = () => {
  auth.clearToken()
  router.push('/login')
}
</script>

<style>
body {
  font-family: Arial, sans-serif;
  padding: 20px;
}

nav a {
  margin: 0 10px;
  text-decoration: none;
  color: #42b983;
}

nav a.router-link-exact-active {
  font-weight: bold;
  color: #35495e;
}
</style>
