import client from 'utils/api';
import ACTIONSNAME from '../actionsName';

export function ponctuationReset() {
    return {
        type: ACTIONSNAME.PONCTUATION_RESET,
    };
}

// Extract
export function getPonctuationRequest() {
    return {
        type: ACTIONSNAME.GET_PONCTUATION_REQUEST,
    };
}

export function getPonctuationSuccess(data) {
    return {
        type: ACTIONSNAME.GET_PONCTUATION_SUCCESS,
        data,
    };
}

export function getPonctuationFailure(error) {
    return {
        type: ACTIONSNAME.GET_PONCTUATION_FAILURE,
        error,
    };
}

export function getPonctuationAction() {
    return async dispatch => {
        dispatch(getPonctuationRequest());
        return client
            .get('/Punctuation/GetUserExtract')
            .then(result => {
                dispatch(getPonctuationSuccess(result.data));
            })
            .catch(error => {
                console.log('getBalanceAction Error>', error.response);
                dispatch(getPonctuationFailure(error.response.data ? error.response.data : 'Ocorreu um erro inesperado. Tente novamente.'));
            });
    };
}

// Credits
export function getCreditsRequest() {
    return {
        type: ACTIONSNAME.GET_CREDITS_REQUEST,
    };
}

export function getCreditsSuccess(data) {
    return {
        type: ACTIONSNAME.GET_CREDITS_SUCCESS,
        data,
    };
}

export function getCreditsFailure(error) {
    return {
        type: ACTIONSNAME.GET_CREDITS_FAILURE,
        error,
    };
}

export function getCreditsAction(data) {
    return async dispatch => {
        dispatch(getCreditsRequest());
        return client
            .post('/Punctuation/GetUserCredits', data)
            .then(result => {
                console.log('getCreditsAction result>', result.data);
                dispatch(getCreditsSuccess(result.data));
            })
            .catch(error => {
                console.log('getCreditsAction Error>', error.response);
                dispatch(getCreditsFailure(error.response.data ? error.response.data : 'Ocorreu um erro inesperado. Tente novamente.'));
            });
    };
}
