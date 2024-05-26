<!-- src/components/FeatureTwo.vue -->
<template>
    <div class="feature feature-two" @click="openTasks">
      <h2>Tarefas</h2>
      <p>Lista de tarefas da agenda</p>
    </div>
  </template>
  
  <script>
  export default {
    name: 'FeatureTwo',
    methods: { openTasks() { this.$router.push({ name: 'TaskList'}); } }
  }
  </script>
  
  <style scoped>
  .feature-two {
    background-color: #e9ecef;
    padding: 20px;
    border-radius: 8px;
    transition: transform 0.3s ease-in-out;
  }
  .feature-two:hover {
    transform: scale(1.05);
  }
  </style>
  