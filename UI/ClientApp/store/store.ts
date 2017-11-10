import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export const store = new Vuex.Store({
    state: {
        /**
         * The current API version used by the client.
         */
        apiVer: '1.0',

        /**
         * Info used to display a model error dialog.
         */
        error: {
            dialogShown: false,
            message: ''
        },

        /**
         * current user.
         */
        username: '',

        /**
         * JWT token.
         */
        access_token: ''
    }
})