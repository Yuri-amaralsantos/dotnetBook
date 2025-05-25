<template>
  <div v-if="book">
    <h2>{{ book.title }}</h2>
    <img :src="book.imageUrl || '/test.jpg'" alt="Capa do livro" style="width: 200px; height: auto;" />
    <p><strong>Autor:</strong> {{ book.author }}</p>
    <p><strong>Ano:</strong> {{ book.year }}</p>

    <h3>Sua review:</h3>
    <div v-if="userReview">
      <p><strong>Nota:</strong> {{ userReview.rating }}/5</p>
      <p><strong>Comentário:</strong> {{ userReview.comment }}</p>
    </div>
    <div v-else>
      <form @submit.prevent="submitReview">
        <input type="number" v-model="rating" min="1" max="5" placeholder="Nota (1-5)" />
        <textarea v-model="comment" placeholder="Comentário"></textarea>
        <button type="submit">Enviar review</button>
      </form>
    </div>

    <h3>Outras reviews:</h3>
    <div v-if="book.reviews && book.reviews.length > 0">
      <ul>
        <li v-for="(review, index) in book.reviews" :key="index">
          <p><strong>Usuário:</strong> {{ review.userName }}</p>
          <p><strong>Nota:</strong> {{ review.rating }}/5</p>
          <p><strong>Comentário:</strong> {{ review.comment }}</p>
          <button
            v-if="userId && (review.userId === userId || isAdmin)"
            @click="removeReadBook(review.id)"
          >
            Remover
          </button>
        </li>
      </ul>
    </div>
    <p v-else>Nenhuma review ainda.</p>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import API from '../services/api'
import useAuth from '../components/useAuth'

const route = useRoute()
const book = ref(null)
const userReview = ref(null)
const rating = ref(0)
const comment = ref('')
const { token, userId, isAdmin } = useAuth()
const headers = { Authorization: `Bearer ${token.value}` }

async function fetchBookDetails() {
  const res = await API.get(`/books/${route.params.id}`)
  book.value = res.data

  try {
    const reviewRes = await API.get('/readbooks', { headers })
    userReview.value = reviewRes.data.find(r => r.bookId === Number(route.params.id)) || null
  } catch (err) {
    if (err.response?.status !== 404) console.error(err)
    userReview.value = null
  }
}

async function submitReview() {
  try {
    await API.post('/readbooks', {
      bookId: Number(route.params.id),
      rating: rating.value,
      comment: comment.value
    }, { headers })

    rating.value = 0
    comment.value = ''
    await fetchBookDetails()
  } catch (error) {
    console.error('Erro ao enviar review:', error.response?.data || error.message)
  }
}

async function removeReadBook(id) {
console.log("Removendo review com id =", id, "do usuário =", userId.value)
  if (!confirm('Tem certeza que deseja remover este comentário?')) return

  try {
    await API.delete(`/readbooks/${id}`, { headers })
    await fetchBookDetails()
  } catch (error) {
    console.error('Erro ao remover livro lido:', error.response?.data || error.message)
    alert('Erro ao remover livro.')
  }
}

onMounted(fetchBookDetails)
</script>
