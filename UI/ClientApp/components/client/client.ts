import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import * as Api from '../../api'

interface Client {
    Name: string;
}

@Component
export default class ClientComponent extends Vue {
    clients: Client[] = [];

    async mounted() {
        await Api.get<Client[]>('api/client/list')
            .then(response => this.clients = response.data);
    }
}
