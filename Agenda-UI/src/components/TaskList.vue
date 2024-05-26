<template>
  <div>
    <h2>Tarefas</h2>    
    <p v-if="message" class="error-message">{{ message }}</p>
    <nav class="navigation">
      <button style="border-radius: 10px; padding: 10px 20px; margin: 0px 5px 5px 0px;" @click.stop="editTask(task)">
        Novo
      </button>
      <button style="border-radius: 10px; padding: 10px 20px; margin: 0px 5px 5px 0px;" @click="goBack">        
        Voltar
      </button>
      <button style="border-radius: 10px; padding: 10px 20px; margin: 0px 5px 5px 0px;" @click="goHome">
        <font-awesome-icon :icon="['fas', 'home']" />
        Início
      </button>
    </nav>
    <table>
      <thead>
        <tr>
          <th>Título</th>
          <th>Descrição</th>
          <th>Data</th>
          <th>Status</th>
          <th>Ações</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(task, index) in tasks"
          :key="task.id"
          :class="{ even: index % 2 === 0, odd: index % 2 !== 0 }"
          @click="editTask(task)"
        >
          <td>{{ task.title }}</td>
          <td>{{ task.description }}</td>
          <td>{{ new Date(task.date).toLocaleDateString() }}</td>
          <td>{{ statusOptions[task.status].text}}</td>
          <td>
            <button style="border-radius: 10px;" @click.stop="deleteTask(task.id)">Excluir</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import axios from '@/http-common';

export default {
  data() {
    return {
      tasks: [],
      statusOptions: [
        { value: 0, text: 'Pendente' },
        { value: 1, text: 'Concluído' },
        { value: 2, text: 'Cancelado' },
      ],
      message: ''
    };
  },
  created() {
    this.fetchTasks();
  },
  methods: {
    async fetchTasks() {
      try {
        const response = await axios.get('/Task/GetAll');
        this.tasks = response.data;
      } catch (error) {
        let msg = 'Erro buscando tarefas: ';
        console.error(msg, error);
        this.message = msg + "Erro " + error.code + " " + error.message;
      }
    },
    async deleteTask(id) {
      try {
        await axios.delete(`/Task/${id}`);
        this.fetchTasks();
      } catch (error) {
        let msg = 'Erro deletando tarefa: ';
        console.error(msg, error);
        this.message = msg + "Erro " + error.code + " " + error.message;
      }
    },
    editTask(task) {
      this.$router.push({ name: 'TaskForm', params: { task } });
    },
    goBack() {
      this.$router.go(-1);
    },
    goHome() {
      this.$router.push('/');
    }
  },
};
</script>

<style>
table {
  width: 100%;
  border-collapse: collapse;
}
th, td {
  border: 1px solid #ddd;
  padding: 8px;
}
tr.even {
  background-color: #f2f2f2;
}
tr.odd {
  background-color: #ffffff;
}
tr:hover {
  background-color: #ddd;
}
</style>
