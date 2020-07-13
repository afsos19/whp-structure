import client from 'utils/api';
import ACTIONSNAME from '../actionsName';

export function teamReset() {
    return {
        type: ACTIONSNAME.TEAM_RESET,
    };
}

// Team Users
export function getTeamUsersRequest() {
    return {
        type: ACTIONSNAME.GET_TEAM_USERS_REQUEST,
    };
}

export function getTeamUsersSuccess(data) {
    return {
        type: ACTIONSNAME.GET_TEAM_USERS_SUCCESS,
        data,
    };
}

export function getTeamUsersFailure(error) {
    return {
        type: ACTIONSNAME.GET_TEAM_USERS_FAILURE,
        error,
    };
}

export function getTeamUsersAction(data) {
    return async dispatch => {
        dispatch(getTeamUsersRequest());
        return client
            .post('User/GetTeamUsers', data)
            .then(result => {
                console.log('getTeamUsersAction Result> ', result.data);
                dispatch(getTeamUsersSuccess(result.data));
            })
            .catch(error => {
                console.log('getTeamUsersAction Error>', error.response);
                dispatch(getTeamUsersFailure('Error'));
            });
    };
}

// Team Users Details
export function getTeamUsersDetailRequest() {
    return {
        type: ACTIONSNAME.GET_TEAM_USERS_DETAIL_REQUEST,
    };
}

export function getTeamUsersDetailSuccess(data) {
    return {
        type: ACTIONSNAME.GET_TEAM_USERS_DETAIL_SUCCESS,
        data,
    };
}

export function getTeamUsersDetailFailure(error) {
    return {
        type: ACTIONSNAME.GET_TEAM_USERS_DETAIL_FAILURE,
        error,
    };
}

export function getTeamUsersDetailAction(data) {
    return async dispatch => {
        dispatch(getTeamUsersDetailRequest());
        return client
            .post('User/GetTeamUsers', data)
            .then(result => {
                console.log('getTeamUsersDetailAction Result> ', result.data);
                dispatch(getTeamUsersDetailSuccess(result.data));
            })
            .catch(error => {
                console.log('getTeamUsersDetailAction Error>', error.response);
                dispatch(getTeamUsersDetailFailure('Error'));
            });
    };
}

// Team Sales
export function getTeamSalesRequest() {
    return {
        type: ACTIONSNAME.GET_TEAM_SALES_REQUEST,
    };
}

export function getTeamSalesSuccess(data) {
    return {
        type: ACTIONSNAME.GET_TEAM_SALES_SUCCESS,
        data,
    };
}

export function getTeamSalesFailure(error) {
    return {
        type: ACTIONSNAME.GET_TEAM_SALES_FAILURE,
        error,
    };
}

export function getTeamSalesAction(data) {
    return async dispatch => {
        dispatch(getTeamSalesRequest());
        return client
            .post('Sale/GetTeamSales', data)
            .then(result => {
                console.log('getTeamSalesAction Result> ', result.data);
                dispatch(getTeamSalesSuccess(result.data));
            })
            .catch(error => {
                console.log('getTeamSalesAction Error>', error.response);
                dispatch(getTeamSalesFailure('Error'));
            });
    };
}

// Team Sales Detail
export function getTeamSalesDetailRequest() {
    return {
        type: ACTIONSNAME.GET_TEAM_SALES_DETAIL_REQUEST,
    };
}

export function getTeamSalesDetailSuccess(data) {
    return {
        type: ACTIONSNAME.GET_TEAM_SALES_DETAIL_SUCCESS,
        data,
    };
}

export function getTeamSalesDetailFailure(error) {
    return {
        type: ACTIONSNAME.GET_TEAM_SALES_DETAIL_FAILURE,
        error,
    };
}

export function getTeamSalesDetailAction(data) {
    return async dispatch => {
        dispatch(getTeamSalesDetailRequest());
        return client
            .post('Sale/GetTeamSales', data)
            .then(result => {
                console.log('getTeamSalesDetailAction Result> ', result.data);
                dispatch(getTeamSalesDetailSuccess(result.data));
            })
            .catch(error => {
                console.log('getTeamSalesDetailAction Error>', error.response);
                dispatch(getTeamSalesDetailFailure('Error'));
            });
    };
}

// Team Training
export function getTeamTrainingRequest() {
    return {
        type: ACTIONSNAME.GET_TEAM_TRAINING_REQUEST,
    };
}

export function getTeamTrainingSuccess(data) {
    return {
        type: ACTIONSNAME.GET_TEAM_TRAINING_SUCCESS,
        data,
    };
}

export function getTeamTrainingFailure(error) {
    return {
        type: ACTIONSNAME.GET_TEAM_TRAINING_FAILURE,
        error,
    };
}

export function getTeamTrainingAction(data) {
    return async dispatch => {
        dispatch(getTeamTrainingRequest());
        return client
            .post('Training/GetTeamSales', data)
            .then(result => {
                console.log('getTeamTrainingAction Result> ', result.data);
                dispatch(getTeamTrainingSuccess(result.data));
            })
            .catch(error => {
                console.log('getTeamTrainingAction Error>', error.response);
                dispatch(getTeamTrainingFailure('Error'));
            });
    };
}

// Team Training Detail
export function getTeamTrainingDetailRequest() {
    return {
        type: ACTIONSNAME.GET_TEAM_TRAINING_DETAIL_REQUEST,
    };
}

export function getTeamTrainingDetailSuccess(data) {
    return {
        type: ACTIONSNAME.GET_TEAM_TRAINING_DETAIL_SUCCESS,
        data,
    };
}

export function getTeamTrainingDetailFailure(error) {
    return {
        type: ACTIONSNAME.GET_TEAM_TRAINING_DETAIL_FAILURE,
        error,
    };
}

export function getTeamTrainingDetailAction(data) {
    return async dispatch => {
        dispatch(getTeamTrainingDetailRequest());
        return client
            .post('Training/GetTeamSales', data)
            .then(result => {
                console.log('getTeamTrainingDetailAction Result> ', result.data);
                dispatch(getTeamTrainingDetailSuccess(result.data));
            })
            .catch(error => {
                console.log('getTeamTrainingDetailAction Error>', error.response);
                dispatch(getTeamTrainingDetailFailure('Error'));
            });
    };
}
