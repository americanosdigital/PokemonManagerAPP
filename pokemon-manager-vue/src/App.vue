<template>
  <div class="container mx-auto px-4">
    <header class="py-6">
      <h1 class="text-3xl font-bold text-center">Pokemon Manager</h1>
    </header>
    <main>
      <PokemonGrid />
    </main>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import PokemonGrid from './components/PokemonGrid.vue';

export default defineComponent({
  name: 'App',
  components: {
    PokemonGrid
  }
});
</script>