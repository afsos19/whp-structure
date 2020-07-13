import { AsyncStorage } from 'react-native';
import client from 'utils/api';
import { base } from '../../utils/api';
import axios from 'axios';
import { unmaskCPF } from '../../utils/masks';
import ACTIONSNAME from '../actionsName';

export function authReset() {
    return {
        type: ACTIONSNAME.AUTH_RESET,
    };
}

export function logout() {
    return {
        type: ACTIONSNAME.LOGOUT,
    };
}

export function logoutAction() {
    return async dispatch => {
        AsyncStorage.removeItem('token');
        dispatch(logout());
    };
}

export function loginRequest() {
    return {
        type: ACTIONSNAME.LOGIN_REQUEST,
    };
}

export function loginSuccess(data) {
    return {
        type: ACTIONSNAME.LOGIN_SUCCESS,
        data
    };
}

export function loginFailure(error) {
    return {
        type: ACTIONSNAME.LOGIN_FAILURE,
        error,
    };
}

function getIpAction() {
    return async dispatch => {
        return axios.get('https://api.ipify.org?format=json')
            .then(result => {
                console.log(result);
                dispatch(loginSuccess());
            })
            .catch(error => {
                console.log(error.response);
                dispatch(
                    loginFailure(
                        // Mesma mensagem de erro, pois o servidor sempre retorna 500
                        // tanto para credenciais erradas ou problema de servidor
                        error.response.data
                            ? 'Ocorreu um erro inesperado. Tente novamente.'
                            : 'Ocorreu um erro inesperado. Tente novamente.'
                    )
                );
            });
    };
}

export function loginAction(data) {
    return async dispatch => {
        dispatch(loginRequest());
        return axios.get('https://api.ipify.org?format=json')
            .then(resultIp => {
                const Ip = resultIp.data.ip;
                return client
                    .post('Authentication/Login', {
                        cpf: unmaskCPF(data.cpf),
                        password: data.password,
                        Ip: Ip
                    })
                    .then(result => {
                        if (result.data.message == 'Usuário encontrado com sucesso') {
                            AsyncStorage.setItem('token', result.data.token);
                            AsyncStorage.setItem('data', JSON.stringify(data));
                        }
                        dispatch(loginSuccess(result.data));
                    })
                    .catch(error => {
                        console.log(error.response);
                        dispatch(
                            loginFailure(
                                error.response.data
                                    ? error.response.data
                                    : 'Ocorreu um erro inesperado. Tente novamente.'
                            )
                        );
                    });
            })
            .catch(error => {
                console.log(error.response);
                dispatch(
                    loginFailure(
                        // Mesma mensagem de erro, pois o servidor sempre retorna 500
                        // tanto para credenciais erradas ou problema de servidor
                        error.response.data
                            ? error.response.data
                            : 'Ocorreu um erro inesperado. Tente novamente.'
                    )
                );
            });
    };
}

// Shop
export function getShopRequest() {
    return {
        type: ACTIONSNAME.GET_SHOP_REQUEST,
    };
}

export function getShopSuccess(data) {
    return {
        type: ACTIONSNAME.GET_SHOP_SUCCESS,
        data,
    };
}

export function getShopFailure(error) {
    return {
        type: ACTIONSNAME.GET_SHOP_FAILURE,
        error,
    };
}

export function getShopAction() {
    return async dispatch => {
        dispatch(getShopRequest());
        return client
            .get('Authentication/ShopAuthenticate')
            .then(result => {
                dispatch(getShopSuccess(result.data));
            })
            .catch(error => {
                console.log(error.response);
                dispatch(getShopFailure(
                    error.response.data
                        ? error.response.data
                        : 'Ocorreu um erro inesperado. Tente novamente.'
                        ));
                    }
                );
    };
}

// Academy
export function getAcademyRequest() {
    return {
        type: ACTIONSNAME.GET_ACADEMY_REQUEST,
    };
}

export function getAcademySuccess(data) {
    return {
        type: ACTIONSNAME.GET_ACADEMY_SUCCESS,
        data,
    };
}

export function getAcademyFailure(error) {
    return {
        type: ACTIONSNAME.GET_ACADEMY_FAILURE,
        error,
    };
}

export function getAcademyAction() {
    return async dispatch => {
        dispatch(getAcademyRequest());
        return client
            .get('Authentication/TrainingAcademyAuthenticate')
            .then(result => {
                dispatch(getAcademySuccess(result.data));
            })
            .catch(error => {
                console.log(error.response);
                dispatch(getShopFailure('Erro ao retornar url'));
            });
    };
}

// New Password
export function newPasswordRequest() {
    return {
        type: ACTIONSNAME.NEW_PASSWORD_REQUEST,
    };
}

export function newPasswordSuccess(data) {
    return {
        type: ACTIONSNAME.NEW_PASSWORD_SUCCESS,
        data,
    };
}

export function newPasswordFailure(error) {
    return {
        type: ACTIONSNAME.NEW_PASSWORD_FAILURE,
        error,
    };
}

export function newPasswordAction(data, token) {
    return async dispatch => {
        dispatch(newPasswordRequest());
        return axios
            .post(`${base}User/UpdateUserExpiredPassword`, data, {
                headers: {
                    Authorization: `Bearer ${token}`,
                }
              })
            .then(result => {
                console.log('newPasswordAction result>', result.data);
                AsyncStorage.setItem('token', token);
                dispatch(newPasswordSuccess(result.data));
            })
            .catch(error => {
                console.log('newPasswordAction Error>', error.response);
                dispatch(newPasswordFailure(error.response.data ? error.response.data : 'Ocorreu um erro inesperado. Tente novamente.'));
            });
    };
}