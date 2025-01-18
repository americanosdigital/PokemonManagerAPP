// UI/pokemon-manager-vue/src/components/PokemonGrid.vue
<template>
  <div class="pokemon-grid">
    <div class="filters">
      <input
        v-model="searchQuery"
        placeholder="Search Pokemon..."
        @input="filterPokemons"
        class="p-2 border rounded-lg mb-4 w-full"
      />
    </div>
    
    <table class="min-w-full">
      <thead>
        <tr>
          <th class="cursor-pointer p-2">Image</th>
          <th @click="sort('name')" class="cursor-pointer p-2">Name</th>
          <th @click="sort('height')" class="cursor-pointer p-2">Height</th>
          <th @click="sort('weight')" class="cursor-pointer p-2">Weight</th>
          <th @click="sort('types')" class="cursor-pointer p-2">Types</th>
          <th @click="sort('lastUpdated')" class="cursor-pointer p-2">Last Updated</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="pokemon in filteredPokemons" :key="pokemon.id" class="hover:bg-gray-100">
          <td class="p-2"><img :src="pokemon.imageUrl" :alt="pokemon.name" class="w-16 h-16" /></td>
          <td class="p-2">{{ pokemon.name }}</td>
          <td class="p-2">{{ pokemon.height }}</td>
          <td class="p-2">{{ pokemon.weight }}</td>
          <td class="p-2">{{ pokemon.types }}</td>
          <td class="p-2">{{ formatDate(pokemon.lastUpdated) }}</td>
        </tr>
      </tbody>
    </table>

    <div v-if="filteredPokemons.length === 0" class="text-center py-4">
      No Pokemon found matching your search criteria.
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue';
import { pokemonService } from '../services/pokemonService';

interface Pokemon {
  id: number;
  name: string;
  height: number;
  weight: number;
  types: string;
  imageUrl: string;
  lastUpdated: string;
}

// Add this type for the sort fields
type SortableFields = keyof Pokemon;

export default defineComponent({
  name: 'PokemonGrid',
  setup() {
    const pokemons = ref<Pokemon[]>([]);
    const filteredPokemons = ref<Pokemon[]>([]);
    const searchQuery = ref('');
    const sortField = ref<SortableFields>('name');
    const sortDirection = ref<'asc' | 'desc'>('asc');

    const loadPokemons = async () => {
      try {
        const data = await pokemonService.getPokemons();
        pokemons.value = data;
        filteredPokemons.value = data;
      } catch (error) {
        console.error('Error loading pokemons:', error);
      }
    };

    const filterPokemons = () => {
      filteredPokemons.value = pokemons.value.filter(pokemon =>
        pokemon.name.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
        pokemon.types.toLowerCase().includes(searchQuery.value.toLowerCase())
      );
      sortPokemons(sortField.value);
    };

    const sort = (field: SortableFields) => {
      if (sortField.value === field) {
        sortDirection.value = sortDirection.value === 'asc' ? 'desc' : 'asc';
      } else {
        sortField.value = field;
        sortDirection.value = 'asc';
      }
      sortPokemons(field);
    };

    const sortPokemons = (field: SortableFields) => {
      filteredPokemons.value.sort((a: Pokemon, b: Pokemon) => {
        const aValue = field === 'lastUpdated' 
          ? new Date(a[field]).getTime() 
          : a[field];
        const bValue = field === 'lastUpdated' 
          ? new Date(b[field]).getTime() 
          : b[field];

        if (typeof aValue === 'string') {
          return sortDirection.value === 'asc'
            ? aValue.localeCompare(bValue as string)
            : (bValue as string).localeCompare(aValue);
        }

        return sortDirection.value === 'asc'
          ? (aValue as number) - (bValue as number)
          : (bValue as number) - (aValue as number);
      });
    };

    const formatDate = (date: string) => {
      return new Date(date).toLocaleString();
    };

    onMounted(loadPokemons);

    return {
      filteredPokemons,
      searchQuery,
      filterPokemons,
      sort,
      formatDate
    };
  }
});
</script>

<style scoped>

</style>