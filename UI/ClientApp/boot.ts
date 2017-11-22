// Add support for older IE versions https://github.com/aspnet/JavaScriptServices/wiki/Supporting-Internet-Explorer-11-(or-older)
import 'core-js/fn/promise';
import 'core-js/fn/object/assign';

import 'core-js/es6/symbol';
import 'core-js/es6/object';
import 'core-js/es6/function';
import 'core-js/es6/parse-int';
import 'core-js/es6/parse-float';
import 'core-js/es6/number';
import 'core-js/es6/math';
import 'core-js/es6/string';
import 'core-js/es6/date';
import 'core-js/es6/array';
import 'core-js/es6/regexp';
import 'core-js/es6/map';
import 'core-js/es6/weak-map';
import 'core-js/es6/set';

import './css/site.css';
import 'bootstrap';
import Vue from 'vue';
import * as Router from './router';
import VueRouter from 'vue-router';
import Moment from 'moment';
import VueStrap from 'vue-strap'

Vue.use(VueRouter);
Vue.use(Moment);
Vue.use(VueStrap);

Vue.filter('formatDate', function (value: Date) {
    if (value) {
        return Moment(value).format('l');
    }
});

Vue.filter('toDate', function (value: string) {
    if (value) {
        return Moment.parseZone(value, 'l').toDate();
    }
});

new Vue({
    el: '#app-root',
    router: Router.default,
    render: h => h(require('./app/app.vue.html'))
});
