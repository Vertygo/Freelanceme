import Vue from 'vue'
import { Component } from 'vue-property-decorator'
import * as Api from '../../api'
import * as Store from '../../store/store'

interface LoginRequest {
    Username: string;
    Password: string;
}

interface Token {
    token: string;
}

@Component
export default class Login extends Vue {

    model: LoginRequest = {
        Username: '',
        Password: ''
    };

    onCreateAccount() {
        this.$router.push('/register');
    }

    async onSubmit() {
        var response = await Api.post<Token>('api/auth/login', JSON.stringify(this.model))
            .then(response => {
                Store.store.state.access_token = response.data.token;
                this.$router.push('/dashboard');
            });
    }
}