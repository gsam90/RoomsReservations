<template>
    <div>
      <h2>Booking Details</h2>
      <!-- Display the details of a specific booking here -->
    </div>
  </template>
  
  <script setup>
  import { onMounted, ref } from 'vue';
  import { useRouter } from 'vue-router';
  
  const route = useRouter();
  const bookingId = ref('');
  
  onMounted(() => {
    // Fetch booking details based on the bookingId parameter from the route
    bookingId.value = route.params.id;
  });
  </script>