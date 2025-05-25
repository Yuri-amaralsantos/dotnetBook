import axios from 'axios';

const API = axios.create({
    baseURL: 'http://localhost:5167/api', // use HTTP sem o S
});

// Adicionar token JWT nas requisições (se necessário futuramente)
API.interceptors.request.use(config => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export default API;
