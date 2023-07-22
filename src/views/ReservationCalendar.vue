<!-- ReservationCalendar.vue -->
<template>
  <li>
    {{ reservations[0] }}
  </li> 
  <div>
    <FullCalendar :events="formattedReservations" 
                  :options="calendarOptions" 
                  @select="handleDateSelect"
                  @eventClick="handleEventClick"
                  /> 
  </div>
</template>

<script setup>
import { ref, onMounted, computed, reactive } from 'vue';
import axios from 'axios';
import FullCalendar from '@fullcalendar/vue3';
import dayGridPlugin from '@fullcalendar/daygrid';

const reservations = ref([]);
let selectedDate = null;

onMounted(async () => 
{
  try 
  {
    const response = await axios.get('https://localhost:7183/Reservation');
    reservations.value = response.data;
    
  } 
  catch (error) 
  {
    console.error('Error fetching reservations:', error);
  }
});

// Format reservations as events for FullCalendar
const formattedReservations = computed(() => {
  if (!selectedDate) {
    return []; // Return an empty array if selectedDate is null or its value is null
  }

    const filteredReservations = reservations.value.filter(reservation => {
      const checkInDate = new Date(reservation.checkInDate);
      const checkOutDate = new Date(reservation.checkOutDate);

    // Check if the selectedDate is within the reservation range
    return (
      selectedDate.value >= checkInDate && 
      selectedDate.value < checkOutDate
    );
  });

  return filteredReservations.map(reservation => ({
    title: reservation.reservationName,
    start: reservation.checkInDate,
    end: reservation.checkOutDate,
    allDay: true,
    backgroundColor: 'rgb(255, 99, 132)',
    borderColor: 'rgb(255, 99, 132)',
    textColor: 'rgb(255, 99, 132)'
  }));
});


const handleDateSelect = (arg) => {
    selectedDate.value = arg.start;
  }
  
  const handleEventClick = (arg) => {
    const reservation = reservations.value.find(reservation => reservation.checkInDate === arg.event.start);
    if (reservation) {
      // Do something with the selected reservation, e.g., show a modal with reservation details
      console.log('Selected Reservation:', reservation);
    }
  };
  
  const calendarOptions = reactive( {
    plugins: [dayGridPlugin],
    initialView: 'dayGridMonth',
    events: formattedReservations, 
    headerToolbar: {
      left: 'today prev,next',
      center: 'title',
      right: 'dayGridMonth,timeGridWeek,timeGridDay'
    },
    editable: true,
    selectable: true,
    weekends: true,
    
  });

</script>

<style>
    :root {
      --fc-border-color: black;
      --fc-daygrid-event-dot-width: 5px;
      --fc-small-font-size: .85em;
    }
</style>
