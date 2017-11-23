import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { modal, checkbox, formValidator, input } from 'vue-strap'
import { ClientInfo } from '../../models/clientinfo.model'
import * as Api from '../../api'

interface Client {
    Id?: string
    IsCompany?: boolean;
    CompanyName?: string;
    Name?: string;
    Surname?: string;
    Website?: string;
    AddressId?: string;
    AddressCountry?: string;
    AddressCity?: string;
    AddressStreet?: string;
    IsActive?: boolean;
}

@Component({
    components: {
        modal: modal,
        checkbox: checkbox,
        formValidator: formValidator,
        bsInput: input
    }
})
export default class ClientComponent extends Vue {
    clients: ClientInfo[] = [];
    editClient: boolean = false;
    model: Client = {};
    loaded: boolean = false;
    isValidEntry: boolean = false;

    async mounted() {
        this.load();
    }

    async load() {
        this.loaded = false;
        await Api.get<ClientInfo[]>('api/client/list')
            .then(response => {
                this.loaded = true;
                this.clients = response.data;
            });
    }

    async onEditClient(id: string) {
        await Api.get<Client>('api/client/get', { Id: id })
            .then(response => {
                this.model = response.data;
                this.editClient = true;
            });
    }

    async saveClient() {
        await Api.post<Client>('api/client/save', this.model)
            .then(response => {
                this.model = response.data;
                this.editClient = false;
                this.load();
            });
    }

    onAddClient() {
        this.model = { IsCompany: true, IsActive: true };
        this.editClient = true;
    }
}
