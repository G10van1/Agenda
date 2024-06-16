<template>
    <div>
      <h1>Usuário</h1>
      <div v-if="user">
        <p><strong>Username:</strong> {{ user.username }}</p>
        <p><strong>Email:</strong> {{ user.email }}</p>
      </div>
      <div v-else>
        <p>Loading...</p>
      </div>
    </div>
  </template>
  
  <script>
  import AuthService from '../services/AuthService';
  
  export default {
    data() {
      return {
        user: null
      };
    },
    created() {
      this.user = AuthService.getCurrentUser();
    }
  };
  </script>
  