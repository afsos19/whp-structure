import client from 'utils/api';
import ACTIONSNAME from '../actionsName';

export function ordersReset() {
    return {
        type: ACTIONSNAME.ORDERS_RESET,
    };
}

// Extract
export function getOrdersRequest() {
    return {
        type: ACTIONSNAME.GET_ORDERS_REQUEST,
    };
}

export function getOrdersSuccess(data) {
    return {
        type: ACTIONSNAME.GET_ORDERS_SUCCESS,
        data,
    };
}

export function getOrdersFailure(error) {
    return {
        type: ACTIONSNAME.GET_ORDERS_FAILURE,
        error,
    };
}

export function getOrdersAction(data) {
    return async dispatch => {
        dispatch(getOrdersRequest());
        return client
            .post('Order/GetUserOrder', data)
            .then(result => {
                console.log('getOrdersAction result>', result.data);
                dispatch(getOrdersSuccess(result.data));
            })
            .catch(error => {
                console.log('getOrdersAction Error>', error.response);
                dispatch(getOrdersFailure('Error'));
            });
    };
}
