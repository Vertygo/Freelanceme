import Vue from 'vue'
import { Component } from 'vue-property-decorator'
import * as Api from '../../api'

interface TimeTrackingSummary {
    WorkingHours: number;
    Client: string;
    ClientID: string;
    StartDate: Date;
    EndDate: Date;
}

interface TimeTrackingDetails {
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

@Component
export default class Dashboard extends Vue {
    detailRequest: TimeTrackingDetailRequest = {};
    summaryRequest: TimeTrackingSummaryRequest = {};
    timeTrackings: TimeTrackingSummary[] = [];
    timeTrackingDetails: TimeTrackingDetails[] = [];
    showDetails: boolean = false;

    get totalWorkingTime(): number {
        return this.timeTrackings
            .map(m => m.WorkingHours)
            .reduce((sum, current) => sum + current);
    }

    async mounted() {
        this.summaryRequest.DateFrom = new Date();
        this.summaryRequest.DateTo = new Date();

        await Api.get<TimeTrackingSummary[]>('api/client/summary', this.summaryRequest)
            .then(response => this.timeTrackings = response.data);
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

        Api.get<TimeTrackingDetails[]>('api/client/details', this.detailRequest)
            .then(response => this.timeTrackingDetails = response.data);
    }
}