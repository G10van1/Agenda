<template>
    <div>
      <h2>{{ Contact.id ? 'Editar Contato' : 'Inserir Contato' }}</h2>      
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
      <form @submit.prevent="saveContact">
        <div>
          <label for="name">Nome:</label>
          <input type="text" v-model="Contact.name" required />
        </div>
        <div>
          <label for="phone">Telefone:</label>
          <textarea v-model="Contact.phone"></textarea>
        </div>
        <div>
          <label for="email">Email:</label>
          <textarea v-model="Contact.email"></textarea>
        </div>
        
        <button style="border-radius: 10px;" type="submit">{{ Contact.id ? 'Atualizar' : 'Inserir' }}</button>
      </form>
    </div>
  </template>
  
  <script>
  import axios from '@/http-common';
  
  export default {
    data() {
      return {
        Contact: {
          id: 0,
          title: '',
          description: '',
          date: new Date().toISOString(),

          status: 0,
        },
        message: ''
      };
    },
    created() {
      if (this.$route.params.Contact) {
        this.Contact = { ...this.$route.params.Contact };
      }
    },
    methods: {
      goBack() {
        this.$router.go(-1);
      },
      goHome() {
        this.$router.push('/');
      },
      async saveContact() {
        try {
          if (this.Contact.id) {
            await axios.put(`/Contact/${this.Contact.id}`, this.Contact);
          } else {
            await axios.post('/Contact', this.Contact);
          }
          this.$router.push({ name: 'ContactList' });
        } catch (error) {
            let msg = 'Erro ao salvar contato: ';
            console.error(msg, error);
            this.message = msg + "Erro " + error.code + " " + error.message;
        }
      },
    },
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
  