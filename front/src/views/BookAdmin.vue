<template>
  <div>
    <h2>Gerenciar Livros</h2>

    <form @submit.prevent="createBook">
      <input v-model="newBook.title" placeholder="Título" required />
      <input v-model="newBook.author" placeholder="Autor" required />
      <input v-model.number="newBook.year" placeholder="Ano" required />
      <input v-model="newBook.imageUrl" placeholder="Link da imagem (opcional)" />
      <button type="submit">Adicionar</button>
    </form>

    <ul>
      <li v-for="book in books" :key="book.id">
        {{ book.title }} - {{ book.author }} ({{ book.year }})
        <button @click="editBook(book)">Editar</button>
        <button @click="deleteBook(book.id)">Excluir</button>
      </li>
    </ul>

    <div v-if="editingBook">
      <h3>Editando livro</h3>
      <form @submit.prevent="updateBook">
        <input v-model="editingBook.title" />
        <input v-model="editingBook.author" />
        <input v-model.number="editingBook.year" />
        <input v-model="editingBook.imageUrl" placeholder="Link da imagem (opcional)" />
        <button type="submit">Salvar</button>
        <button @click="editingBook = null">Cancelar</button>
      </form>
    </div>
  </div>
</template>

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
