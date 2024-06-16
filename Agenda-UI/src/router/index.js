import Vue from 'vue';
import VueRouter from 'vue-router';
import HomePage from '@/components/HomePage.vue';
import TaskList from '@/components/TaskList.vue';
import TaskForm from '@/components/TaskForm.vue';
import ContactList from '@/components/ContactList.vue';
import ContactForm from '@/components/ContactForm.vue';
import Login from '@/components/LoginForm.vue';
import UserProfile from '@/components/UserProfile.vue';
import AuthService from '../services/AuthService';

Vue.use(VueRouter);

const routes = [
  {
    path: '/',
    name: 'HomePage',
    component: HomePage,
  },
  { path: '/login',
    name: 'Login',
    component: Login 
  },
  { 
    path: '/profile',
    name: 'UserProfile',
    component: UserProfile,
    beforeEnter: (to, from, next) => {
      const user = AuthService.getCurrentUser();
      if (!user) {
        next('/login');
      } else {
        next();
      }
    }
  },
  {
    path: '/tasks',
    name: 'TaskList',
    component: TaskList,
  },
  {
    path: '/contacts',
    name: 'ContactList',
    component: ContactList,
  },
  {
    path: '/create',
    name: 'TaskForm',
    component: TaskForm,
  },
  {
    path: '/createContact',
    name: 'ContactForm',
    component: ContactForm,
  },
];

const router = new VueRouter({
  mode: 'history',
  routes,
});

export default router;
