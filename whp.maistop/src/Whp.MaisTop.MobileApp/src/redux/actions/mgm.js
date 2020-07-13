import client from 'utils/api';
import { unmaskCPF } from 'utils/masks';
import ACTIONSNAME from '../actionsName';

export function mgmReset() {
    return {
        type: ACTIONSNAME.MGM_RESET,
    };
}

// Get code MGM
export function mgmCodeRequest() {
    return {
        type: ACTIONSNAME.MGM_CODE_REQUEST,
    };
}

export function mgmCodeSuccess(data) {
    return {
        type: ACTIONSNAME.MGM_CODE_SUCCESS,
        data,
    };
}

export function mgmCodeFailure(error) {
    return {
        type: ACTIONSNAME.MGM_CODE_FAILURE,
        error,
    };
}

export function mgmCodeAction() {
    return async dispatch => {
        dispatch(mgmCodeRequest());
        return client
            .get(`FriendInvite/GetUserAccessCode`)
            .then(result => {
                console.log('mgmCodeAction Success> ', result);
                dispatch(mgmCodeSuccess(result.data));
            })
            .catch(error => {
                console.log('mgmCodeAction Error> ', error.response);
                dispatch(
                    mgmCodeFailure(error.response.data ? error.response.data : 'Error')
                );
            });
    };
}

// Send SMS MGM
export function mgmSmsRequest() {
    return {
        type: ACTIONSNAME.MGM_SMS_REQUEST,
    };
}

export function mgmSmsSuccess(data) {
    return {
        type: ACTIONSNAME.MGM_SMS_SUCCESS,
        data,
    };
}

export function mgmSmsFailure(error) {
    return {
        type: ACTIONSNAME.MGM_SMS_FAILURE,
        error,
    };
}

export function mgmSmsAction(data) {
    return async dispatch => {
        dispatch(mgmSmsRequest());
        return client
            .post(`FriendInvite/SendAccessCodeInvite`, data)
            .then(result => {
                console.log('mgmSmsAction Success> ', result);
                dispatch(mgmSmsSuccess(result.data));
            })
            .catch(error => {
                console.log('mgmSmsAction Error> ', error.response);
                dispatch(
                    mgmSmsFailure(error.response.data ? error.response.data : 'Error')
                );
            });
    };
}

// Get invited users
export function invitedUsersRequest() {
    return {
        type: ACTIONSNAME.INVITED_USERS_REQUEST,
    };
}

export function invitedUsersSuccess(data) {
    return {
        type: ACTIONSNAME.INVITED_USERS_SUCCESS,
        data,
    };
}

export function invitedUsersFailure(error) {
    return {
        type: ACTIONSNAME.INVITED_USERS_FAILURE,
        error,
    };
}

export function invitedUsersAction() {
    return async dispatch => {
        dispatch(invitedUsersRequest());
        return client
            .get(`FriendInvite/GetInvitedUsers`)
            .then(result => {
                console.log('invitedUsersAction Success> ', result);
                dispatch(invitedUsersSuccess(result.data));
            })
            .catch(error => {
                console.log('invitedUsersAction Error> ', error.response);
                dispatch(
                    invitedUsersFailure(error.response.data ? error.response.data : 'Erro')
                );
            });
    };
}

// User invited
export function userInvitedRequest() {
    return {
        type: ACTIONSNAME.USER_INVITED_REQUEST,
    };
}

export function userInvitedSuccess() {
    return {
        type: ACTIONSNAME.USER_INVITED_SUCCESS,
    };
}

export function userInvitedFailure(error) {
    return {
        type: ACTIONSNAME.USER_INVITED_FAILURE,
        error,
    };
}

export function userInvitedAction(data) {
    return async dispatch => {
        dispatch(userInvitedRequest());
        return client
            .post(`FriendInvite/DoCadUserInvited`, data)
            .then(result => {
                console.log('userInvitedAction Success> ', result);
                dispatch(userInvitedSuccess());
            })
            .catch(error => {
                console.log('userInvitedAction Error> ', error.response);
                dispatch(
                    userInvitedFailure(error.response.data ? error.response.data : 'Erro ao cadastrar')
                );
            });
    };
}
