import Vue from 'vue'
import { Component } from 'vue-property-decorator'
import * as Api from '../../api'
import { RegisterRequest } from '../../models/request/registerrequest.model'
import Router from '../../router'

@Component
export default class Register extends Vue {

    model: RegisterRequest = {};

    errors: any;

    async onSubmit() {
        var response = await Api.post('api/auth/register', JSON.stringify(this.model))
            .then(response => this.$router.push('/'))
            .catch(err => this.errors = err)
    }
}