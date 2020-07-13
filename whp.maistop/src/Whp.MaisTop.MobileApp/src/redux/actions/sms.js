import client from 'utils/api';
import axios from 'axios';
import ACTIONSNAME from '../actionsName';
import { base } from '../../utils/api';

export function smsReset() {
    return {
        type: ACTIONSNAME.SMS_RESET,
    };
}

export function getSMSRequest() {
    return {
        type: ACTIONSNAME.GET_SMS_REQUEST,
    };
}

export function getSMSSuccess(data) {
    return {
        type: ACTIONSNAME.GET_SMS_SUCCESS,
        data,
    };
}

export function getSMSFailure(error) {
    return {
        type: ACTIONSNAME.GET_SMS_FAILURE,
        error,
    };
}

export function getSMSAction(data) {
    console.log("SMS data> ", data);
    return async dispatch => {
        dispatch(getSMSRequest());
        return client
            .post('/Authentication/GenerateAccessCode', data)
            .then(result => {
                console.log('getSMSAction Result> ', result.data);
                dispatch(getSMSSuccess(result.data));
            })
            .catch(error => {
                console.log('getSMSAction Error>', error.response);
                dispatch(getSMSFailure(error.response.data ? error.response.data : 'Erro'));
            });
    };
}

// Sms expired password
export function getSMSPasswordRequest() {
    return {
        type: ACTIONSNAME.GET_SMS_PASSWORD_REQUEST,
    };
}

export function getSMSPasswordSuccess(data) {
    return {
        type: ACTIONSNAME.GET_SMS_PASSWORD_SUCCESS,
        data,
    };
}

export function getSMSPasswordFailure(error) {
    return {
        type: ACTIONSNAME.GET_SMS_PASSWORD_FAILURE,
        error,
    };
}

export function getSMSPasswordAction(data, token) {
    console.log("getSMSPasswordAction> ", data, token)
    return async dispatch => {
        dispatch(getSMSPasswordRequest());
        return axios
            .post(`${base}/User/SendAccessCodeExpiration`, data, {
                headers: {
                    Authorization: `Bearer ${token}`,
                }
              })
            .then(result => {
                console.log('getSMSPasswordAction Result> ', result);
                console.log('getSMSPasswordAction Result> ', result.data);
                dispatch(getSMSPasswordSuccess(result.data));
            })
            .catch(error => {
                console.log('getSMSPasswordAction Error>', error);
                console.log('getSMSPasswordAction Error>', error.response);
                dispatch(getSMSPasswordFailure(error.response.data ? error.response.data : 'Erro'));
            });
    };
}