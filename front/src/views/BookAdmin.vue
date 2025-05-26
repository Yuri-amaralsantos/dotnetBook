<template>
  <div class="container">
    <h2>Gerenciar Livros</h2>

    <form @submit.prevent="createBook" class="book-form">
      <fieldset>
        <h3>Adicionar novo livro</h3>

        <label>
          Título:
          <input v-model="newBook.title" required />
        </label>

        <label>
          Autor:
          <input v-model="newBook.author" required />
        </label>

        <label>
          Ano:
          <input v-model.number="newBook.year" type="number" required />
        </label>

        <label>
          URL da imagem:
          <input v-model="newBook.imageUrl" placeholder="https://..." />
        </label>

        <div v-if="newBook.imageUrl" class="image-preview">
          <img :src="newBook.imageUrl" alt="Prévia da imagem" />
        </div>

        <button type="submit">Adicionar</button>
      </fieldset>
    </form>

    <ul class="book-list">
      <li v-for="book in books" :key="book.id">
        <div>
          <strong>{{ book.title }}</strong> - {{ book.author }} ({{ book.year }})
        </div>
        <div class="actions">
          <button @click="editBook(book)">Editar</button>
          <button @click="deleteBook(book.id)">Excluir</button>
        </div>
      </li>
    </ul>

    <div v-if="editingBook" class="edit-form">
      <h3>Editando livro: {{ editingBook.title }}</h3>
      <form @submit.prevent="updateBook">
        <label>
          Título:
          <input v-model="editingBook.title" />
        </label>

        <label>
          Autor:
          <input v-model="editingBook.author" />
        </label>

        <label>
          Ano:
          <input v-model.number="editingBook.year" type="number" />
        </label>

        <label>
          URL da imagem:
          <input v-model="editingBook.imageUrl" />
        </label>

        <div v-if="editingBook.imageUrl" class="image-preview">
          <img :src="editingBook.imageUrl" alt="Prévia da imagem" />
        </div>

        <button type="submit">Salvar</button>
        <button type="button" @click="editingBook = null">Cancelar</button>
      </form>
    </div>
  </div>
</template>

<style scoped>
.container {
  max-width: 600px;
  margin: auto;
  padding: 1rem;
}

.book-form,
.edit-form {
  background-color: #1e1e1e;
  padding: 15px;
  border-radius: 10px;
  text-align: center;
  color: #fff;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.4);
}

.book-form fieldset {
  border: none;
  padding: 0;
}

label {
  display: block;
  margin-bottom: 1rem;
}

input {
  width: 100%;
  padding: 6px;
  box-sizing: border-box;
  margin-top: 4px;
}

.image-preview img {
  margin-top: 10px;
  max-width: 150px;
  height: auto;
  border: 1px solid #ddd;
  border-radius: 4px;
}

.book-list {
  list-style: none;
  padding: 0;
}

.book-list li {
  display: flex;
  justify-content: space-between;
  padding: 0.5rem;
}

.actions button {
  margin-left: 8px;
}
</style>


<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import API from '../services/api'
import useAuth from '../components/useAuth'

const { isAdmin } = useAuth()
const router = useRouter()

if (!isAdmin.value) {
  router.push('/books') // redireciona não-admins
}

const books = ref([])
const newBook = ref({ title: '', author: '', year: null })
const editingBook = ref(null)

const fetchBooks = async () => {
  const res = await API.get('/books')
  books.value = res.data
}

const createBook = async () => {
  await API.post('/books', newBook.value)
  newBook.value = { title: '', author: '', year: null }
  fetchBooks()
}

const deleteBook = async (id) => {
  await API.delete(`/books/${id}`)
  fetchBooks()
}

const editBook = (book) => {
  editingBook.value = { ...book }
}

const updateBook = async () => {
  await API.put(`/books/${editingBook.value.id}`, editingBook.value)
  editingBook.value = null
  fetchBooks()
}

onMounted(fetchBooks)
</script>
