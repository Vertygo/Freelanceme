import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import * as Api from '../../api'

interface WeatherForecast {
    DateFormatted: string;
    TemperatureC: number;
    TemperatureF: number;
    Summary: string;
}

@Component
export default class Client extends Vue {
    forecasts: WeatherForecast[] = [];

    async mounted() {
        await Api.get<WeatherForecast[]>('api/SampleData/WeatherForecasts')
            .then(response => this.forecasts = response.data);
    }
}
