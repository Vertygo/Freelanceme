import Vue from 'vue'
import { Component } from 'vue-property-decorator'
import * as Api from '../../api'

interface TimeTrackingSummary {
    WorkingTime: string;
    Client: string;
    StartDate: Date;
    EndDate: Date;
}

@Component
export default class Dashboard extends Vue {
    timetrackings: TimeTrackingSummary[] = [];

    async mounted() {
        await Api.get<TimeTrackingSummary[]>('api/client/summary')
            .then(response => this.timetrackings = response.data);
    }
}