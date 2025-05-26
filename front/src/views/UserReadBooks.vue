<template>
  <div>

    <div v-if="readBooks.length">
      <h3>Seus livros lidos:</h3>
      <ul>
        <li v-for="book in readBooks" :key="book.bookId">
          <strong>{{ book.bookTitle }}</strong> - Nota: {{ book.rating }}<br />
          Comentário: {{ book.comment }}
          <br />
          <button @click="removeReadBook(book.id)">Remover</button>
        </li>
      </ul>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import useAuth from '../components/useAuth'
import API from '../services/api' // o axios já está encapsulado aqui

const { token } = useAuth()
const books = ref([])
const readBooks = ref([])
const bookId = ref('')
const rating = ref(0)
const comment = ref('')

const headers = { Authorization: `Bearer ${token.value}` }

onMounted(async () => {
  try {
    const res = await API.get('/books')
    books.value = res.data
    console.log('Livros carregados:', books.value)

    const rbRes = await API.get('/readbooks', { headers }) // cuidado com letras maiúsculas
    readBooks.value = rbRes.data
  } catch (error) {
    console.error('Erro ao carregar dados:', error)
  }
})

async function addReadBook() {
  console.log('Enviando dados:', {
    bookId: bookId.value,
    rating: rating.value,
    comment: comment.value
  })

  try {
    await API.post('/readbooks', {
      bookId: Number(bookId.value),
      rating: rating.value,
      comment: comment.value
    }, { headers })

    const rbRes = await API.get('/readbooks', { headers })
    readBooks.value = rbRes.data

    bookId.value = ''
    rating.value = 0
    comment.value = ''
  } catch (error) {
    console.error('Erro ao adicionar livro lido:', error.response?.data || error.message)
    alert('Erro ao adicionar livro.')
  }
}

async function removeReadBook(id) {
  if (!confirm('Tem certeza que deseja remover este comentário?')) return

  try {
    await API.delete(`/readbooks/${id}`, { headers })
    readBooks.value = (await API.get('/readbooks', { headers })).data
  } catch (error) {
    console.error('Erro ao remover livro lido:', error.response?.data || error.message)
    alert('Erro ao remover livro.')
  }
}

</script>

<style scoped>
ul li {
  position: relative;
  padding-right: 80px; /* Espaço para o botão no canto */
  margin-bottom: 20px;
  background: #1e1e1e;
  border-radius: 6px;
  padding: 10px 15px;
  color: white;
}

ul li button {
  position: absolute;
  top: 5px;
  right: 5px;
  background-color: #c62828;
  color: #fff;
  border: none;
  padding: 5px 10px;
  font-size: 12px;
  cursor: pointer;
  border-radius: 4px;
  transition: background-color 0.2s ease;
}

ul li button:hover {
  background-color: #a91e1e;
}

</style>