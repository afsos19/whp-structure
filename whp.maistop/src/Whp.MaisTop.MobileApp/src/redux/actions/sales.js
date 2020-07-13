import client from 'utils/api';
import ACTIONSNAME from '../actionsName';

export function salesReset() {
    return {
        type: ACTIONSNAME.SALES_RESET,
    };
}

// Extract
export function getSalesRequest() {
    return {
        type: ACTIONSNAME.GET_SALES_REQUEST,
    };
}

export function getSalesSuccess(data) {
    return {
        type: ACTIONSNAME.GET_SALES_SUCCESS,
        data,
    };
}

export function getSalesFailure(error) {
    return {
        type: ACTIONSNAME.GET_SALES_FAILURE,
        error,
    };
}

export function getSalesAction(data) {
    return async dispatch => {
        dispatch(getSalesRequest());
        return client
            .post('/Punctuation/GetUserCredits', data)
            .then(result => {
                console.log('getSalesAction result>', result.data);
                dispatch(getSalesSuccess(result.data));
            })
            .catch(error => {
                console.log('getSalesAction Error>', error.response);
                dispatch(getSalesFailure('Error'));
            });
    };
}
