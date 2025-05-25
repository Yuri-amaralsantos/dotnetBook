import { ref, computed } from 'vue'

const token = ref(localStorage.getItem('token'))

// Sincroniza o token quando outra aba altera
window.addEventListener('storage', () => {
    token.value = localStorage.getItem('token')
})

// Define ou limpa o token
const setToken = (newToken) => {
    token.value = newToken
    if (newToken) {
        localStorage.setItem('token', newToken)
    } else {
        localStorage.removeItem('token')
    }
}

const clearToken = () => setToken(null)

// Decodifica o payload do JWT
const payload = computed(() => {
    if (!token.value) return null
    try {
        return JSON.parse(atob(token.value.split('.')[1]))
    } catch {
        return null
    }
})

// Estado derivado
const isAuthenticated = computed(() => !!payload.value)

const isAdmin = computed(() =>
    payload.value?.role === 'Admin' ||
    payload.value?.['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] === 'Admin'
)

const userId = computed(() => {
    const raw =
        payload.value?.userId ||
        payload.value?.['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']
    return raw ? Number(raw) : null
})

const userName = computed(() =>
    payload.value?.userName ||
    payload.value?.['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']
)

const userRole = computed(() =>
    payload.value?.role ||
    payload.value?.['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
)

// Função opcional para login que armazena o token e já atualiza
const setAuthData = (data) => {
    setToken(data.token)
}

// Exporta o hook
export default function useAuth() {
    return {
        token,
        userId,
        userName,
        userRole,
        isAuthenticated,
        isAdmin,
        auth: {
            token,
            userId,
            userName,
            userRole,
            isAuthenticated,
            isAdmin,
            setAuthData,
            clearToken
        }
    }
}
