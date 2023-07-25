<template>
  <div>
    <h2>Reservation Form</h2>
    <form @submit.prevent="makeReservation">
      <!-- Replace the placeholders with the actual form fields -->
      <div>
        <label for="guestName">Guest Name:</label>
        <input type="text" id="guestName" v-model="state.guestName" />
      </div>
      <div>
        <label for="checkInDate">Check-in Date:</label>
        <input type="date" id="checkInDate" v-model="state.checkInDate" />
      </div>
      <div>
        <label for="checkOutDate">Check-out Date:</label>
        <input type="date" id="checkOutDate" v-model="state.checkOutDate" />
      </div>
      <!-- Add more form fields as needed -->

      <button type="submit">Make Reservation</button>
    </form>
  </div>
</template>

<script setup>
import { reactive } from 'vue';

const state = reactive({
  guestName: '',
  checkInDate: '',
  checkOutDate: '',
  // Add more form fields here
});

const makeReservation = () => {
  // You can access the form field values through the reactive state object
  const reservationData = {
    guestName: state.guestName,
    checkInDate: state.checkInDate,
    checkOutDate: state.checkOutDate,
    // Add more form field values to the reservationData object
  };

  // Handle the reservation logic here, e.g., submit the data to the backend API
  // using axios or fetch
  console.log('Reservation Data:', reservationData);

  // Clear the form fields after submission (optional)
  state.guestName = '';
  state.checkInDate = '';
  state.checkOutDate = '';
  // Clear more form fields here if needed
};
</script>
