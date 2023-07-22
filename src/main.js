import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import { Calendar } from '@fullcalendar/core';
import '@fullcalendar/daygrid/index.js';
// import dayGridPlugin from '@fullcalendar/daygrid';
// import timeGridPlugin from '@fullcalendar/timegrid';
// import listPlugin from '@fullcalendar/list';

// let calendar = new Calendar(calendarEl, {
//   plugins: [ dayGridPlugin, timeGridPlugin, listPlugin ],
//   initialView: 'dayGridMonth',
//   headerToolbar: {
//     left: 'prev,next today',
//     center: 'title',
//     right: 'dayGridMonth,timeGridWeek,listWeek'
//   }
// });

createApp(App)
    .use(router)
    .component('FullCalendar', Calendar)
    .mount('#app');