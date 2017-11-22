import Vue from 'vue'
import { Component, Watch, Prop, Model } from 'vue-property-decorator'
import { datepicker, modal, dropdown, select } from 'vue-strap'
import { ClientInfo, Project } from '../../common'
import Moment from 'moment';
import * as Api from '../../api'

interface TimeTrackingSummary {
    WorkingHours: number;
    Client: string;
    ClientID: string;
    StartDate: Date;
    EndDate: Date;
}

interface TimeTrackingDetails {
    ProjectName: string;
    WorkingHours: number;
    Date: Date;
}

interface TimeTrackingSummaryRequest {
    DateFrom?: Date,
    DateTo?: Date
}

interface TimeTrackingDetailRequest {
    ClientID?: string;
    StartDate?: Date;
    EndDate?: Date;
}

interface LogWork {
    DateFormat: string;
    Date?: Date;
    WorkingHours: number;
    ClientId: string;
    ProjectId: string;
}

@Component({
    components: {
        datepicker: datepicker,
        modal: modal,
        dropdown: dropdown,
        vselect: select
    }
})
export default class Dashboard extends Vue {
    model: LogWork = { DateFormat: Vue.filter('formatDate')(new Date()), WorkingHours: 0, ClientId: '', ProjectId: '' };

    projects: Project[] = [];
    client: string; // client id
    clients: ClientInfo[] = [];
    summaryRequest: TimeTrackingSummaryRequest;
    detailRequest: TimeTrackingDetailRequest = {};
    timeTrackings: TimeTrackingSummary[] = [];
    timeTrackingDetails: TimeTrackingDetails[] = [];
    dateFrom: string = Vue.filter('formatDate')(Moment(new Date()).add(-7, 'd'));
    dateTo: string = Vue.filter('formatDate')(new Date());
    showDetails: boolean = false;
    showLogWork: boolean = false;
    loaded: boolean = false;

    get totalWorkingTime(): number {
        return this.timeTrackingDetails
            .map(m => m.WorkingHours)
            .reduce((sum, current) => sum + current);
    }
    
    @Watch('model', { immediate: false, deep: true })
    onModelChanged(changed: LogWork) {
        if (changed != null && changed.ClientId != null)
        {
            let list = this.clients.find(c => c.Id == changed.ClientId)!.Projects;
            this.projects = list;

            if (list.length)
                this.model.ProjectId = list[0].Id;
        }
        else
            this.projects = [];
    }
    
    async mounted() {
        await Api.get<ClientInfo[]>('api/client/list', { projects: true })
            .then(response => this.clients = response.data);

        this.onSearch();
    }

    onSearch() {
        this.summaryRequest = {
            DateFrom: new Date(Date.parse(this.dateFrom)),
            DateTo: new Date(Date.parse(this.dateTo))
        };

        this.load();
    }

    load() {
        this.loaded = false;
        this.timeTrackings = [];
        Api.get<TimeTrackingSummary[]>('api/timetracking/summary', this.summaryRequest)
            .then(response => {
                this.loaded = true;
                this.timeTrackings = response.data;
            });
    }
    
    onCloseDetails() {
        this.showDetails = false;
        this.detailRequest = {};
    }

    onDetails(summary: TimeTrackingSummary) {
        this.showDetails = true;
        this.detailRequest.ClientID = summary.ClientID;
        this.detailRequest.StartDate = summary.StartDate;
        this.detailRequest.EndDate = summary.EndDate;

        Api.get<TimeTrackingDetails[]>('api/timetracking/details', this.detailRequest)
            .then(response => this.timeTrackingDetails = response.data);
    }

    onShowLogWork() {
        this.showLogWork = true;
    }

    onCloseLogWork() {
        this.showLogWork = false;
    }

    onWorkItemDelete() {

    }

    async onSaveLogWork() {
        this.model.Date = Vue.filter('toDate')(this.model.DateFormat); // control holds date in string format so we have to cast (change UI control)

        await Api.post<LogWork>('api/timetracking/save', this.model)
            .then(response => {
                this.showLogWork = false;
                this.load();
            });
    }

}