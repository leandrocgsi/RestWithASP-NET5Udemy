import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:44300',
})

export default api;