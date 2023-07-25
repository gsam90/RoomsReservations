<template>
    <div>
      <h1>Reservations </h1>
      <div>
        <!-- Render the calendar here -->
        <ReservationCalendar :reservations="reservations" />
        
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref, onMounted } from 'vue';
  import axios from 'axios';
  import ReservationCalendar from './ReservationCalendar.vue';
  
  const reservations = ref([]);
  

  onMounted(async () => {
    try {
      const response = await axios.get('https://localhost:7183/Reservation');
      reservations.value = response.data;
      console.log(reservations.value);
    } catch (error) {
      console.error('Error fetching reservations:', error);
    }
  });

  
  </script>
  
  <style>
  /* Add any custom styles here */
  </style>
  