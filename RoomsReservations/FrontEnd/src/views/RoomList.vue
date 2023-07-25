<template>
    <div>
      <h2>Room List</h2>
      <!-- Display a list of available rooms here -->
      <li v-for="room in rooms" :key="room.id">
        {{ room.roomName }}
      </li>
    </div>
  </template>
  
  <script setup>
  import { onMounted, ref } from 'vue';
  import axios from 'axios';
  
  const rooms = ref([]);

  onMounted(async () => {
  // Your code to fetch data or perform other actions
  try {
    const response = await axios.get('https://localhost:7183/api/Room');
    rooms.value = response.data;
    // Handle the response data and update reactive variables if needed
  } catch (error) {
    console.log(error);
  }
});
  </script>