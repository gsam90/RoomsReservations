import { createRouter, createWebHistory } from 'vue-router';
import RoomList from '../views/RoomList.vue';
import ReservationForm from '../views/ReservationForm.vue';
import BookingDetails from '../views/BookingDetails.vue';
import ReservationPage from '../views/ReservationPage.vue';


const routes = [
  { path: '/', component: RoomList },
  { path: '/reservation', component: ReservationForm },
  { path: '/reservationList', component: ReservationPage },
  { path: '/booking/:id', component: BookingDetails },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
