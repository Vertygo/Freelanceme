import axios, { AxiosResponse, AxiosPromise, AxiosRequestConfig } from 'axios'
import * as Router from './router'
import { store } from './store/store'

export const urls = {
    apiUrl: "http://localhost:5000"
};


export function get<T>(relUrl: string, data?: any): AxiosPromise<T> {
    var config: AxiosRequestConfig = {
        baseURL: urls.apiUrl,
        params: data,
        headers: {
            Accept: 'application/json',
            Authorization: `Bearer ${store.state.access_token}`
        }
    };

    // Axios removes content-type from GET so we have to use query parameter
    return axios.get<T>(`${relUrl}?api-version=${store.state.apiVer}`, config)
        .then(response => {
            checkResponse(response);

            return response;
        }).catch(error => checkResponse(error.response));
}

export function post<T>(relUrl: string, body?: any): AxiosPromise<T> {
    var config: AxiosRequestConfig = {
        baseURL: urls.apiUrl,
        headers: {
            Accept: 'application/json',
            'Content-Type': `application/json; api-version=${store.state.apiVer}`,
            Authorization: `Bearer ${store.state.access_token}`
        }
    };

    return axios.post<T>(relUrl, body, config).then(response => {
        checkResponse(response);

        return response;
    }).catch(error => checkResponse(error.response));
}

function checkResponse(response: AxiosResponse<any>) {
    if (response.status == 401) {
        // Unauthorized back to login
        Router.Router.push({ path: '/' });
    }
}
