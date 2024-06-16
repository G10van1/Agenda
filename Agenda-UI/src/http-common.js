import axios from 'axios';
import AuthService from './services/AuthService';
import router from './router';

const instance = axios.create({
  baseURL: 'https://localhost:7266', 
  headers: {
    'Content-Type': 'application/json'
  }
});

instance.interceptors.request.use(config => {
  const user = AuthService.getCurrentUser();
  if (user && user.token) {
    config.headers.Authorization = `Bearer ${user.token}`;
  }
  return config;
}, error => {
  return Promise.reject(error);
});

instance.interceptors.response.use(response => {
  return response;
}, error => {
  if (error.response && error.response.status === 401) {
    AuthService.logout();
    router.push('/login');
  }
  return Promise.reject(error);
});

export default instance;
