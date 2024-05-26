<template>
  <div id="app">
    <router-view />
  </div>
</template>

<script>
export default {
  name: 'App',
};
</script>

<style>
/* Styles */
.error-message {
    color: red;
    background-color: #f8d7da;
    border-color: #f5c6cb;
    padding: 10px;
    margin: 10px 0;
    border: 1px solid transparent;
    border-radius: 4px;
}
</style>
