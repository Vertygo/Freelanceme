import VueRouter, { Route, RouteConfig } from 'vue-router';

const routes = [
    {
        path: '', component: require('./layouts/auth.vue.html'),
        children: [
            { path: '/', component: require('./components/login/login.vue.html') },
            { path: '/register', component: require('./components/register/register.vue.html') },
        ]
    },
    {
        path: '', component: require('./layouts/default.vue.html'),
        children: [
            { path: '/dashboard', component: require('./components/dashboard/dashboard.vue.html') },
            { path: '/client', component: require('./components/client/client.vue.html') },
            { path: '/configuration', component: require('./components/configuration/configuration.vue.html') }]
    },
];

export const Router = new VueRouter({
    mode: 'history',
    routes,
    scrollBehavior(to, from, savedPosition) {
        if (savedPosition) {
            return savedPosition; // return to last place if using back/forward
        } else if (to.hash) {
            return { selector: to.hash }; // scroll to anchor if provided
        } else {
            return { x: 0, y: 0 }; // reset to top-left
        }
    }
});

export default Router;