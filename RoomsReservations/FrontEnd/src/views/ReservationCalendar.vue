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
  <div id="reservationModal" class="modal">
  <div class="modal-content">
    <span class="close">&times;</span>
    <!-- Reservation details will be displayed here -->
    <div id="reservationDetails"></div>
  </div>
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

    // Close the modal when the user clicks on the close button (x)
    document.querySelector('.close').addEventListener('click', () => {
    const modal = document.getElementById('reservationModal');
    modal.style.display = 'none';
  });
    

    //console.log(reservations.value);
    
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
      //console.log(checkInDate, checkOutDate)

      const formattedcheckInDateString = formattedDate(checkInDate);
      const formattedcheckOutDateString = formattedDate(checkOutDate);
//console.log(formattedcheckInDateString, formattedcheckOutDateString);


    // Check if the selectedDate is within the reservation range
    // return (
    //   selectedDate.value >= checkInDate && 
    //   selectedDate.value < checkOutDate
      
    // );
    return {
      title: reservation.reservationName,
      start: formattedcheckInDateString,
      end: formattedcheckOutDateString,
      allDay: true,
      editable: true,
      selectedDate: selectedDate.value,
      selectable: true
    }
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
    try {
    const reservation = reservations.value.find(reservation => reservation.checkInDate === arg.event.start);
    if (reservation) {
       // Show the modal
      const modal = document.getElementById('reservationModal');
      modal.style.display = 'block';

       // Display the reservation details in the modal
      const reservationDetails = document.getElementById('reservationDetails');
      reservationDetails.innerHTML = `
        <h3>Reservation Details</h3>
        <p><strong>Check-in Date:</strong> ${reservation.checkInDate}</p>
        <p><strong>Other Details:</strong> ${reservation.reservationName}</p>
        
        <!-- Add more reservation details here as needed -->`;
        console.log('Selected Reservation:', reservation);
    }
  } catch (error) {
    console.error('Error fetching reservations:', error);
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

    .modal {
  display: none;
  position: fixed;
  z-index: 1;
  left: 0;
  top: 0;
  width: 100%;
  height: 100%;
  overflow: auto;
  background-color: rgba(0, 0, 0, 0.4);
}

.modal-content {
  background-color: #fefefe;
  margin: 15% auto;
  padding: 20px;
  border: 1px solid #888;
  width: 80%;
}

.close {
  color: #aaaaaa;
  float: right;
  font-size: 28px;
  font-weight: bold;
}

.close:hover,
.close:focus {
  color: #000;
  text-decoration: none;
  cursor: pointer;
}
</style>
