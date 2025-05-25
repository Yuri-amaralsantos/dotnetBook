import { createRouter, createWebHistory } from 'vue-router'
import useAuth from '../components/useAuth'
import Login from '../views/Login.vue'
import Register from '../views/Register.vue'
import BookList from '../views/BookList.vue'
import BookAdmin from '../views/BookAdmin.vue'
import UserReadBooks from '../views/UserReadBooks.vue'
import BookDetail from '../views/BookDetail.vue'


const routes = [
    { path: '/', redirect: '/login' },
    { path: '/login', component: Login },
    { path: '/register', component: Register },
    { path: '/books', component: BookList, meta: { requiresAuth: true } },
    { path: '/books/:id', component: BookDetail, meta: { requiresAuth: true } },
    { path: '/mybooks', component: UserReadBooks, meta: { requiresAuth: true } },
    { path: '/admin/books', component: BookAdmin, meta: { requiresAuth: true, requiresAdmin: true } }
]



const router = createRouter({
    history: createWebHistory(),
    routes
})

router.beforeEach((to, from, next) => {
    const { isAuthenticated, isAdmin } = useAuth()
    if (to.meta.requiresAuth && !isAuthenticated.value) {
        next('/login')
    } else if (to.meta.requiresAdmin && !isAdmin.value) {
        next('/books')
    } else {
        next()
    }
})

export default router
