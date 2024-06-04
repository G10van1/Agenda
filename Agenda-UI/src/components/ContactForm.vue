<template>
    <div>
      <h2>{{ contact.id ? 'Editar Contato' : 'Inserir Contato' }}</h2>      
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
          <input type="text" v-model="contact.name" required />
        </div>
        <div>
          <label for="phone">Telefone:</label>
          <textarea v-model="contact.phone"></textarea>
        </div>
        <div>
          <label for="email">Email:</label>
          <textarea v-model="contact.email"></textarea>
        </div>
        
        <button style="border-radius: 10px;" type="submit">{{ contact.id ? 'Atualizar' : 'Inserir' }}</button>
      </form>
    </div>
  </template>
  
  <script>
  import axios from '@/http-common';
  
  export default {
    data() {
      return {
        contact: {
          id: 0,
          name: '',
          phone: '',
          email: '',
        },
        message: ''
      };
    },
    created() {
      if (this.$route.params.contact) {
        this.contact = { ...this.$route.params.contact };
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
          if (this.contact.id) {
            await axios.put(`/Contact/${this.contact.id}`, this.contact);
          } else {
            await axios.post('/Contact', this.contact);
          }
          this.$router.push({ name: 'ContactList' });
        } catch (error) {
          let msg = "Erro " + error.code + " ao salvar contato: ";
          this.message = msg + error.message;
          if (error.response && error.response.status === 400) {
            this.message = msg;              
            const serverErrors = error.response.data.errors;
            Object.keys(serverErrors).forEach(key => {
              this.message = this.message + " " + serverErrors[key][0] + "\n";
            });
          }
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
  