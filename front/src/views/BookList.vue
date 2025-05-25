<template>
  <div>
    <h2>Livros</h2>
    <ul>
      <li v-for="book in books" :key="book.id">
        <router-link :to="`/books/${book.id}`">
        <img
      :src="book.imageUrl || '/test.jpg'"
      alt="Capa do livro"
      style="width: 200px; height: auto;"
    />
          {{ book.title }} - {{ book.author }} ({{ book.year }})
        </router-link>
      </li>
    </ul>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import API from '../services/api'

const books = ref([])

const fetchBooks = async () => {
  const res = await API.get('/books')
  books.value = res.data
}

onMounted(fetchBooks)
</script>
