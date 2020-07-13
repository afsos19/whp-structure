import client from 'utils/api';
import axios from 'axios';
import ACTIONSNAME from '../actionsName';

export function userReset() {
    return {
        type: ACTIONSNAME.USER_RESET,
    };
}

// Get user
export function getUserRequest() {
    return {
        type: ACTIONSNAME.GET_USER_REQUEST,
    };
}

export function getUserSuccess(data) {
    return {
        type: ACTIONSNAME.GET_USER_SUCCESS,
        data,
    };
}

export function getUserFailure(error) {
    return {
        type: ACTIONSNAME.GET_USER_FAILURE,
        error,
    };
}

export function getUserAction(id) {
    return async dispatch => {
        dispatch(getUserRequest());
        return client
            .get(`/User/GetUser/${id}`)
            .then(result => {
                console.log('getUserAction Result> ', result.data);
                dispatch(getUserSuccess(result.data));
            })
            .catch(error => {
                console.log('getUserAction Error>', error.response);
                dispatch(getUserFailure(error.response.data ? error.response.data : 'Ocorreu um erro ao atualizar seu perfil. Tente novamente mais tarde.`'));
            });
    };
}

// Get user whith token
export function getUserWithTokenRequest() {
    return {
        type: ACTIONSNAME.GET_USER_WITH_TOKEN_REQUEST,
    };
}

export function getUserWithTokenSuccess(data) {
    return {
        type: ACTIONSNAME.GET_USER_WITH_TOKEN_SUCCESS,
        data,
    };
}

export function getUserWithTokenFailure(error) {
    return {
        type: ACTIONSNAME.GET_USER_WITH_TOKEN_FAILURE,
        error,
    };
}

// Update User Phone
export function updateUserPhoneRequest() {
    return {
        type: ACTIONSNAME.UPDATE_USER_PHONE_REQUEST,
    };
}

export function updateUserPhoneSuccess(data) {
    return {
        type: ACTIONSNAME.UPDATE_USER_PHONE_SUCCESS,
        data,
    };
}

export function updateUserPhoneFailure(error) {
    return {
        type: ACTIONSNAME.UPDATE_USER_PHONE_FAILURE,
        error,
    };
}

export function updateUserPhoneAction(data) {
    console.log(data);
    return async dispatch => {
        dispatch(updateUserPhoneRequest());
        return client
            .post(`User/UpdateUser`, data, {
                headers: { 'Content-Type': 'multipart/form-data' },
            })
            .then(result => {
                dispatch(updateUserPhoneSuccess(result.data.user));
            })
            .catch(error => {
                console.log('updateUserAction Error>', error.response);
                dispatch(
                    updateUserPhoneFailure(
                        error.response.data ? error.response.data : 'Ocorreu um erro ao atualizar seu perfil. Tente novamente mais tarde.`'
                    )
                );
            });
    };
}

// Update User
export function updateUserRequest() {
    return {
        type: ACTIONSNAME.UPDATE_USER_REQUEST,
    };
}

export function updateUserSuccess(data) {
    return {
        type: ACTIONSNAME.UPDATE_USER_SUCCESS,
        data,
    };
}

export function updateUserFailure(error) {
    return {
        type: ACTIONSNAME.UPDATE_USER_FAILURE,
        error,
    };
}

export function updateUserAction(data) {
    console.log(data);
    return async dispatch => {
        dispatch(updateUserRequest());
        return client
            .post(`User/UpdateUser`, data, {
                headers: { 'Content-Type': 'multipart/form-data' },
            })
            .then(result => {
                console.log('updateUserAction Result> ', result.data.user);
                dispatch(updateUserSuccess(result.data.user));
            })
            .catch(error => {
                console.log('updateUserAction Error>', error.response);
                dispatch(
                    updateUserFailure(
                        error.response.data ? error.response.data : 'Ocorreu um erro ao atualizar seu perfil. Tente novamente mais tarde.`'
                    )
                );
            });
    };
}
