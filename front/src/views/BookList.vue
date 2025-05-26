<template>
  <div>
    <h2>Livros</h2>
    <ul class="book-grid">
      <li v-for="book in books" :key="book.id">
        <div class="book-card">
          <router-link :to="`/books/${book.id}`">
            <img
              :src="book.imageUrl || '/test.jpg'"
              alt="Capa do livro"
            />
            <p>{{ book.title }} ({{ book.year }})</p>
          </router-link>
        </div>
      </li>
    </ul>
  </div>
</template>


<script setup>
import { ref, onMounted } from 'vue'
import API from '../services/api'
import useAuth from '../components/useAuth'

const books = ref([])
const { userId } = useAuth()

const fetchBooks = async () => {
  const res = await API.get('/books')
  books.value = res.data
}

onMounted(fetchBooks)
</script>

<style scoped>
.book-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
  gap: 20px;
  padding: 0;
  margin-top: 20px;
  list-style: none;
}

.book-card {
  background-color: #1e1e1e;
  padding: 15px;
  border-radius: 10px;
  text-align: center;
  color: #fff;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.4);
}

.book-card img {
  width: 100%;
  height: auto;
  border-radius: 4px;
  margin-bottom: 10px;
}

.book-card p {
  margin: 0;
  font-size: 14px;
  line-height: 1.4;
}

.book-card a {
  text-decoration: none;
  color: inherit;
}

.book-card a:hover {
  text-decoration: underline;
}

</style>