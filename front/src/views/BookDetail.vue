<template>
  <div v-if="book" class="book-detail-container">
    <!-- Topo com imagem e dados -->
    <div class="book-header">
      <img :src="book.imageUrl || '/test.jpg'" alt="Capa do livro" />
      <div class="book-info">
        <h2>{{ book.title }}</h2>
        <p><strong>Autor:</strong> {{ book.author }}</p>
        <p><strong>Ano:</strong> {{ book.year }}</p>
      </div>
    </div>

    <!-- Review do usuário ou formulário -->
    <form @submit.prevent="submitReview" class="review-form">
    <div class="rating-circles">
      <span
        v-for="n in 5"
        :key="n"
        :class="['circle', { selected: rating === n }]"
        @click="rating = n"
      >
        {{ n }}
      </span>
    </div>

    <textarea v-model="comment" placeholder="Comentário"></textarea>
    <br>
    <button type="submit">Enviar review</button>
  </form>


    <!-- Outras reviews -->
    <div class="other-reviews">
      <h3>Outras reviews</h3>
      <ul class="review-list" v-if="book.reviews && book.reviews.length > 0">
        <li v-for="(review, index) in book.reviews" :key="index" class="review-card">
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
      <p v-else>Nenhuma review ainda.</p>
    </div>
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

<style scoped>
.book-detail-container {
  color: #fff;
  padding: 20px;
  max-width: 900px;
  margin: 0 auto;
}

.book-header {
  display: flex;
  gap: 20px;
  margin-bottom: 30px;
  align-items: flex-start;
}

.book-header img {
  width: 200px;
  height: auto;
  border-radius: 8px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.4);
}

.book-info {
  flex: 1;
}

.user-review-section,
.other-reviews {
  margin-top: 30px;
}

.review-form{

}


.review-form textarea {
  width: 100%;
  min-height: 200px; /* Garante altura maior */
  background-color: #1e1e1e;
  border: 1px solid #444;
  color: #fff;
  padding: 10px;
  margin-bottom: 10px;
  border-radius: 5px;
  resize: vertical; /* Permite o usuário redimensionar, se quiser */
  font-size: 14px;
}




.review-list {
  list-style: none;
  padding: 0;
}

.review-card {
  background-color: #1e1e1e;
  border: 1px solid #333;
  border-radius: 8px;
  padding: 15px;
  margin-bottom: 15px;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.3);
}

.review-card button {
  margin-top: 10px;
  background-color: #c62828;
  color: #fff;
  border: none;
  padding: 5px 10px;
  cursor: pointer;
  border-radius: 5px;
}

.review-card button:hover {
  background-color: #a91e1e;
}

.rating-circles {
  display: flex;
  justify-content: start;
  gap: 10px;
  margin-bottom: 15px;
}

.circle {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  background-color: #444;
  color: #fff;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  font-weight: bold;
  transition: 0.2s ease;
  border: 2px solid transparent;
}

.circle:hover {
  background-color: #666;
}

.circle.selected {
  background-color: #f5c518;
  color: #000;
  border-color: #fff;
}


</style>