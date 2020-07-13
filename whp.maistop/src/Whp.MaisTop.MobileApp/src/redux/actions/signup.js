import { AsyncStorage } from 'react-native';
import client from 'utils/api';
import ACTIONSNAME from '../actionsName';

export function signupReset() {
    return {
        type: ACTIONSNAME.SIGNUP_RESET,
    };
}

export function preRegistrationRequest() {
    return {
        type: ACTIONSNAME.PRE_REGISTRATION_REQUEST,
    };
}

export function preRegistrationSuccess() {
    return {
        type: ACTIONSNAME.PRE_REGISTRATION_SUCCESS,
    };
}

export function preRegistrationFailure(error) {
    return {
        type: ACTIONSNAME.PRE_REGISTRATION_FAILURE,
        error,
    };
}

export function preRegistrationAction(data) {
    return async dispatch => {
        dispatch(preRegistrationRequest());
        return client
            .post(`/Authentication/PreRegistration`, data, {
                headers: { 'Content-Type': 'multipart/form-data' },
            })
            .then(result => {
                console.log("result >>>> ", result);
                AsyncStorage.setItem('token', result.data.token);
                dispatch(preRegistrationSuccess());
            })
            .catch(error => {
                dispatch(
                    preRegistrationFailure(error.response.data ? error.response.data : 'Ocorreu um erro.')
                );
            });
    };
}
