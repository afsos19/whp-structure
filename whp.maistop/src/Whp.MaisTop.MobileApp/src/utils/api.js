import axios from 'axios';
import { AsyncStorage } from 'react-native';
import NavigationService from './NavigationService';
import ROUTENAMES from '../navigation/routeName';
// import { unmaskCPF } from './masks';
// import createAuthRefreshInterceptor from 'axios-auth-refresh';

/* 
Endereços para cada URL de api:

** Dev:              https://localhost:44341
** Homologação:      http://novoprogramamaistop.fullbarhomologa.com.br/api
** Produção:         https://programamaistop.com.br/api

*/

export const base = 'https://programamaistop.com.br/api/';

const client = axios.create({
    baseURL: base,
});

client.interceptors.request.use(
    config =>
        AsyncStorage.getItem('token').then(token => {
            // eslint-disable-next-line no-param-reassign
            config.headers.Authorization = `Bearer ${token}`;
            return Promise.resolve(config);
        }),
    error => Promise.reject(error)
);

// client.interceptors.request.use(
//     AsyncStorage.getItem('data').then(value => {
//         console.log("data> ", JSON.parse(value));
//     })
// );

// const refreshAuthLogic = failedRequest => 
//     axios.get('https://api.ipify.org?format=json')
//         .then(resultIp => {
//             const Ip = resultIp.data.ip;
//             return AsyncStorage.getItem('data').then(value => {
//                 const data = JSON.parse(value);

//                 return axios
//                     .post(base + 'Authentication/Login', {
//                         cpf: unmaskCPF(JSON.parse(value).cpf),
//                         password: JSON.parse(value).password,
//                         Ip: Ip
//                     })
//                     .then(result => {
//                         if (result.data.message == 'Usuário encontrado com sucesso') {
//                             AsyncStorage.setItem('token', result.data.token);
//                             failedRequest.response.config.headers['Authentication'] = 'Bearer ' + result.data.token;
//                             return Promise.resolve();
//                         } else {
//                             NavigationService.navigate(ROUTENAMES.UNAUTHORIZED);
//                         }
//                     })
//                     .catch(error => {
//                         console.log("refreshAuthLogic error> ", error);
//                         console.log("refreshAuthLogic error.response> ", error.response);
//                         // NavigationService.navigate(ROUTENAMES.UNAUTHORIZED);
//                     });
//             });
//         })

client.interceptors.response.use(function (response) {
    return response;
}, function (error) {
    if (401 === error.response.status) {
        NavigationService.navigate(ROUTENAMES.UNAUTHORIZED);
    } else {
        return Promise.reject(error);
    }
});


export default client;
// export default createAuthRefreshInterceptor(client, refreshAuthLogic);
