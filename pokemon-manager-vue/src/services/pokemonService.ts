
import axios from 'axios';

const API_BASE_URL = 'http://localhost:5000/api'; // Adjust this to match your API URL

export const pokemonService = {
  async getPokemons() {
    const response = await axios.get(`${API_BASE_URL}/pokemon`);
    return response.data;
  },

  async getPokemonById(id: number) {
    const response = await axios.get(`${API_BASE_URL}/pokemon/${id}`);
    return response.data;
  }
};