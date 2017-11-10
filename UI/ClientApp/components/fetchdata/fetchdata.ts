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
export default class FetchDataComponent extends Vue {
    forecasts: WeatherForecast[] = [];

    mounted() {
        //fetch('api/SampleData/WeatherForecasts')
        //this.$http.get('api/SampleData/WeatherForecasts')
        //axios.get('http://localhost:5000/api/SampleData/WeatherForecasts')
        //    .then(response => axios.all<WeatherForecast>(response.data))
        //    .then(data => {
        //        //for (let d of data)
        //        //    console.debug(d.dateFormatted);
        //        this.forecasts = data
        //    });

        Api.getAuth<WeatherForecast[]>('api/SampleData/WeatherForecasts')
            .then(response => this.forecasts = response.data);
    }
}
