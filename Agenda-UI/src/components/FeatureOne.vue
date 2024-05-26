<!-- src/components/FeatureOne.vue -->
<template>
  <div class="feature feature-one" @click="openContacts">
    <h2>Contatos</h2>
    <p>Lista de contatos da agenda</p>
  </div>
</template>

<script>
export default {
  name: 'FeatureOne',
  methods: { openContacts() { this.$router.push({ name: 'ContactList'}); } }
}
</script>

<style scoped>
.feature-one {
  background-color: #e9ecef;
  padding: 20px;
  border-radius: 8px;
  transition: transform 0.3s ease-in-out;
}
.feature-one:hover {
  transform: scale(1.05);
}
</style>
