<!-- src/views/HomePage.vue -->
<template>
    <div class="home-page">
      <header class="hero">
        <h1>Bem Vindo a Aplicação Agenda</h1>
        <p>Gerencie Contatos e Tarefas </p>
      </header>
      <main class="features">
        <FeatureOne />
        <FeatureTwo />
      </main>
    </div>
  </template>
  
  <script>
  import FeatureOne from '@/components/FeatureOne.vue';
  import FeatureTwo from '@/components/FeatureTwo.vue';
  
  export default {
    name: 'HomePage',
    components: {
      FeatureOne,
      FeatureTwo
    }
  }
  </script>
  
  <style scoped>
  .home-page {
    font-family: 'Arial', sans-serif;
    text-align: center;
    color: #343a40;
  }
  .hero {
    background-color: #06ae58;
    color: #fff;    
    border-radius: 8px;
    padding: 50px 20px;
  }
  .hero h1 {
    font-size: 3rem;
    margin-bottom: 10px;
  }
  .hero p {
    font-size: 1.5rem;
  }
  .features {
    display: flex;
    justify-content: space-around;
    padding: 20px;
  }
  .feature {
    width: 45%;
  }
  @media (max-width: 768px) {
    .features {
      flex-direction: column;
      align-items: center;
    }
    .feature {
      width: 80%;
      margin-bottom: 20px;
    }
  }
  </style>
  