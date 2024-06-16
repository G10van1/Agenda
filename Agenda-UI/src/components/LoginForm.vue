<template>
    <div>
      <h1>Login</h1>
      <p v-if="message" class="error-message">{{ message }}</p>
      <nav class="navigation">
        <button style="border-radius: 10px; padding: 10px 20px; margin: 0px 5px 5px 0px;" @click="goBack">        
          Voltar
        </button>
        <button style="border-radius: 10px; padding: 10px 20px; margin: 0px 5px 5px 0px;" @click="goHome">
          <font-awesome-icon :icon="['fas', 'home']" />
          Início
        </button>
      </nav>
      <form @submit.prevent="login">
        <div>
          <label for="username">Usuário:</label>
          <input type="text" v-model="username" required>
        </div>
        <div>
          <label for="password">Senha:</label>
          <input type="password" v-model="password" required>
        </div>
        <button type="submit">Login</button>
      </form>
    </div>
  </template>
  
  <script>
  import AuthService from '../services/AuthService';
  
  export default {
    data() {
      return {
        username: '',
        password: '',
        message: ''
      };
    },
    methods: {
      goBack() {
        this.$router.go(-1);
      },
      goHome() {
        this.$router.push('/');
      },
      login() {
        AuthService.login({ username: this.username, password: this.password })
          .then(() => {
            this.$router.go(-1);
          })
          .catch(error => {
            console.error(error);
            this.message = "Falha no Login, verifique se usuário e senha estão corretos!";
          });
      }
    }
  };
  </script>

<style>
form {
  display: flex;
  flex-direction: column;
}
div {
  margin-bottom: 10px;
}
label {
  margin-bottom: 5px;
}
input, textarea, select {
  width: 100%;
  padding: 8px;
  box-sizing: border-box;
}
button {
  padding: 10px;
  background-color: #4CAF50;
  color: white;
  border: none;
  cursor: pointer;
}
button:hover {
  background-color: #45a049;
}
</style>