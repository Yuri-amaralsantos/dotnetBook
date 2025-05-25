import { ref, computed } from 'vue'

const token = ref(localStorage.getItem('token'))

// Sincroniza o token quando outra aba altera
window.addEventListener('storage', () => {
    token.value = localStorage.getItem('token')
})

const setToken = (newToken) => {
    token.value = newToken
    if (newToken) {
        localStorage.setItem('token', newToken)
    } else {
        localStorage.removeItem('token')
    }
}

const clearToken = () => setToken(null)

const payload = computed(() => {
    if (!token.value) return null
    try {
        return JSON.parse(atob(token.value.split('.')[1]))
    } catch {
        return null
    }
})

const isAuthenticated = computed(() => !!payload.value)

const isAdmin = computed(() =>
    payload.value?.role === 'Admin' ||
    payload.value?.['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] === 'Admin'
)

const userId = computed(() => payload.value?.nameid || null)

export default function useAuth() {
    return {
        token,
        payload,
        isAuthenticated,
        isAdmin,
        userId,
        setToken,
        clearToken,
    }
}
