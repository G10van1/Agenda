<template>
    <div>
      <h2>{{ task.id ? 'Editar Tarefa' : 'Inserir Tarefa' }}</h2>
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
      <form @submit.prevent="saveTask">
        <div>
          <label for="title">Título:</label>
          <input type="text" v-model="task.title" required />
        </div>
        <div>
          <label for="description">Descrição:</label>
          <textarea v-model="task.description"></textarea>
        </div>
        <div>
          <label for="date">Data:</label>
          <input type="datetime-local" v-model="task.date" required />
        </div>
        <div>
          <label for="status">Status:</label>
          <select v-model="task.status" required>
            <option value=0>Pendente</option>
            <option value=1>Concluído</option>
            <option value=2>Cancelado</option>

          </select>
        </div>
        <button style="border-radius: 10px;" type="submit">{{ task.id ? 'Atualizar' : 'Inserir' }}</button>
      </form>
    </div>
  </template>
  
  <script>
  import axios from '@/http-common';
  
  export default {
    data() {
      return {
        task: {
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
      if (this.$route.params.task) {
        this.task = { ...this.$route.params.task };
      }
    },
    methods: {
      goBack() {
        this.$router.go(-1);
      },
      goHome() {
        this.$router.push('/');
      },
      async saveTask() {
        try {
          if (this.task.id) {
            await axios.put(`/Task/${this.task.id}`, this.task);
          } else {
            await axios.post('/Task', this.task);
          }
          this.$router.push({ name: 'TaskList' });
        } catch (error) {
          let msg = "Erro " + error.code + " ao salvar tarefa: ";
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
  