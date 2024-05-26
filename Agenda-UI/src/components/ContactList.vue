<template>
  <div>
    <h2>Contatos</h2>
    <p v-if="message" class="error-message">{{ message }}</p>
    <nav class="navigation">
      <button style="border-radius: 10px; padding: 10px 20px; margin: 0px 5px 5px 0px;" @click.stop="editContact(contact)">
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
          <th>Nome</th>
          <th>Telefone</th>
          <th>Email</th>
          <th>Ações</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(contact, index) in contacts"
          :key="contact.id"
          :class="{ even: index % 2 === 0, odd: index % 2 !== 0 }"
          @click="editContact(contact)"
        >
          <td>{{ contact.name }}</td>
          <td>{{ contact.phone }}</td>
          <td>{{ contact.email }}</td>
          <td>
            <button style="border-radius: 10px;" @click.stop="deleteContact(contact.id)">Excluir</button>
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
      contacts: [],
      message: ''
    };
  },
  created() {
    this.fetchContacts();
  },
  methods: {
    async fetchContacts() {
      try {
        const response = await axios.get('/Contact/GetAll');
        this.contacts = response.data;
      } catch (error) {
        let msg = 'Erro buscando Contatos: ';
        console.error(msg, error);
        this.message = msg + "Erro " + error.code + " " + error.message;
      }
    },
    async deleteContact(id) {
      try {
        await axios.delete(`/Contact/${id}`);
        this.fetchContacts();
      } catch (error) {
        let msg = 'Erro deletando Contato: ';
        console.error(msg, error);
        this.message = msg + "Erro " + error.code + " " + error.message;
      }
    },
    editContact(contact) {
      this.$router.push({ name: 'ContactForm', params: { contact } });
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
