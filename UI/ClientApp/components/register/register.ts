import Vue from 'vue'
import { Component } from 'vue-property-decorator'
import * as Api from '../../api'
import Router from '../../router'

interface RegisterDto {
    Name?: string;
    Surname?: string;
    Email?: string;
    Username?: string;
    Password?: string;
}

@Component
export default class Register extends Vue {

    model: RegisterDto = {};

    errors: any;

    async onSubmit() {
        var response = await Api.post('api/auth/register', JSON.stringify(this.model))
            .then(response => this.$router.push('/'))
            .catch(err => this.errors = err)
    }
}