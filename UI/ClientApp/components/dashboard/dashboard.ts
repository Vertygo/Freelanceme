import Vue from 'vue'
import { Component, Watch, Prop, Model } from 'vue-property-decorator'
import { datepicker, modal, dropdown, select, formValidator, input } from 'vue-strap'
import { ClientInfo } from '../../models/clientinfo.model'
import { Project } from '../../models/project.model'

import { TimeTrackingSummary } from '../../models/timetrackingsummary.model'
import { TimeTrackingDetails } from '../../models/timetrackingdetails.model'
import { TimeLogRequest } from '../../models/request/timelogrequest.model'
import { TimeTrackingSummaryRequest } from '../../models/request/timetrackingsummaryrequest.model'
import { TimeTrackingDetailRequest } from '../../models/request/timetrackingdetailrequest.model'

import Moment from 'moment';
import * as Api from '../../api'

@Component({
    components: {
        datepicker: datepicker,
        modal: modal,
        dropdown: dropdown,
        vselect: select,
        formValidator: formValidator,
        bsInput: input
    }
})
export default class Dashboard extends Vue {
    model: TimeLogRequest = { DateFormat: Vue.filter('formatDate')(new Date()), WorkingHours: 0, ClientId: '', ProjectId: '' };

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
    isValidEntry: boolean = false;


    get totalWorkingTime(): number {
        return this.timeTrackingDetails
            .map(m => m.WorkingHours)
            .reduce((sum, current) => sum + current);
    }
    
    @Watch('model', { immediate: false, deep: true })
    onModelChanged(changed: TimeLogRequest) {
        if (changed != null && changed.ClientId != null) {
            let selectedClient = this.clients.find(c => c.Id == changed.ClientId);

            if(selectedClient != null){
                this.projects = selectedClient.Projects;
            }

            if (this.projects.length)
                this.model.ProjectId = this.projects[0].Id;
        }
        else{
            this.projects = [];
        }
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

        await Api.post<TimeLogRequest>('api/timetracking/save', this.model)
            .then(response => {
                this.showLogWork = false;
                this.load();
            });
    }

}