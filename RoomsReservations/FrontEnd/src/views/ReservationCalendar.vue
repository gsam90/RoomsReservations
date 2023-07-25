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
import { ref, onMounted, computed, reactive, watch } from 'vue';
import axios from 'axios';
import FullCalendar from '@fullcalendar/vue3';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction'


const reservations = ref([]);
let selectedDate = [];

onMounted(async () => 
{
  try 
  {
    const response = await axios.get('https://localhost:7183/Reservation');
    reservations.value = response.data;
    console.log(reservations.value);
    
  } 
  catch (error) 
  {
    console.error('Error fetching reservations:', error);
  }
});

// Format reservations as events for FullCalendar
const formattedReservations = computed(() => {
  // if (!selectedDate) {
  //   return []; // Return an empty array if selectedDate is null or its value is null
  // }
  
    const filteredReservations = reservations.value.filter(reservation => {
      const checkInDate = new Date(reservation.checkInDate);
      const checkOutDate = new Date(reservation.checkOutDate);
      console.log(checkInDate, checkOutDate)

      const formattedcheckInDateString = formattedDate(checkInDate);
      const formattedcheckOutDateString = formattedDate(checkOutDate);
console.log(formattedcheckInDateString, formattedcheckOutDateString);


    // Check if the selectedDate is within the reservation range
    // return (
    //   selectedDate.value >= checkInDate && 
    //   selectedDate.value < checkOutDate
      
    // );
  });

  return filteredReservations.map(reservation => ({
    title: reservation.reservationName,
    start: new Date(reservation.checkInDate),
    end: new Date(reservation.checkOutDate),
    allDay: true,
    editable: true,
    selectedDate: selectedDate.value,
    selectable: true
  }));
});

const formattedDate = (date) => {
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, '0');
  const day = String(date.getDate()).padStart(2, '0');
  
  return `${year}-${month}-${day}`;
};

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
  
  const calendarOptions = reactive({
    plugins: [dayGridPlugin, interactionPlugin],
    initialView: 'dayGridMonth',
    events: [],
    headerToolbar: {
      left: 'today prev,next',
      center: 'title',
      right: 'dayGridMonth,timeGridWeek,timeGridDay'
    },
    editable: true,
    selectable: true,
    weekends: true,
    
  });

  watch(formattedReservations, (newEvents) => {
  calendarOptions.events = newEvents;
});

</script>

<style>
    :root {
      --fc-border-color: black;
      --fc-daygrid-event-dot-width: 5px;
      --fc-small-font-size: .85em;
    }
</style>
