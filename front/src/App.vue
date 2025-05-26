<template>
  <div id="app">
    

    <nav>
      <h1>BookReviews</h1>
      <div class="nav-container">
      <router-link v-if="!isAuthenticated" to="/login">Login</router-link> 
      <router-link v-if="!isAuthenticated" to="/register">Registrar</router-link> 
      <router-link v-if="isAuthenticated" to="/books">Livros</router-link> 
      <router-link v-if="isAuthenticated" to="/mybooks">Perfil</router-link>
      <router-link v-if="isAuthenticated && isAdmin" to="/admin/books">Gerenciar Livros</router-link>
      </div> 
      <div class="nav-container">
      <span v-if="isAuthenticated">Olá, {{ userName }}!</span>
      <button class="logout" v-if="isAuthenticated" @click="logout">Sair</button>
      </div>
    </nav>

    <router-view />
  </div>
</template>

<script setup>
import useAuth from './components/useAuth'
import { useRouter } from 'vue-router'
import { computed } from 'vue'

const router = useRouter()
const {auth, } = useAuth()

const isAuthenticated = computed(() => auth.isAuthenticated.value)
const isAdmin = computed(() => auth.isAdmin.value)
const userName = computed(() => auth.userName.value) 

const logout = () => {
  auth.clearToken()
  router.push('/login')
}
</script>

<style>

body {
  margin: 0;
  padding: 1rem;
  font-family: 'Poppins', sans-serif;
  background-color: #121212;
  color: #e0e0e0;
}

h1, h2, h3 {
  color: #f5f5f5;
  font-weight: 600;
}

nav {
  background-color: #1c1c1c;
  padding: 10px 20px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  gap: 15px;
  border-bottom: 1px solid #333;
}

nav a {
  color: #f5c518;
  text-decoration: none;
  font-weight: 500;
  margin: 0 10px;
}

nav a.router-link-exact-active {
  border-bottom: 2px solid #f5c518;
}

.logout{
margin: 0 1rem;
}

button {
  background-color: black;
  border: 1px solid #f5c518;
  color: #f5c518;
  padding: 5px 10px;
  cursor: pointer;
  transition: 0.2s ease;
}

button:hover {
  background-color: #f5c518;
  color: #121212;
}

input, textarea {
  background-color: #1e1e1e;
  border: 1px solid #333;
  color: #fff;
  padding: 10px;
  width: 100%;
  margin: 10px 0;
}

button[type="submit"] {
  background-color: #f5c518;
  border:#f5c518;
  color: #000;
  border: none;
  padding: 10px 20px;
  font-weight: bold;
  cursor: pointer;
  transition: 0.2s ease;
}

button[type="submit"]:hover {
  background-color:rgb(156, 143, 93);
}

ul {
  list-style: none;
  padding: 0;
}

li {
  margin-bottom: 20px;
  background-color: #1c1c1c;
  padding: 10px;
  border-radius: 8px;
}

img {
  border-radius: 4px;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.5);
}

.book-card {
  background-color: #1e1e1e;
  padding: 15px;
  border-radius: 10px;
  display: flex;
  flex-direction: column;
  align-items: center;
  max-width: 200px;
}

</style>
