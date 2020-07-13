import client from 'utils/api';
import ACTIONSNAME from '../actionsName';

export function networkReset() {
    return {
        type: ACTIONSNAME.NETWORK_RESET,
    };
}

export function getNetworkRequest() {
    return {
        type: ACTIONSNAME.GET_NETWORK_REQUEST,
    };
}

export function getNetworkSuccess(data) {
    return {
        type: ACTIONSNAME.GET_NETWORK_SUCCESS,
        data,
    };
}

export function getNetworkFailure(error) {
    return {
        type: ACTIONSNAME.GET_NETWORK_FAILURE,
        error,
    };
}

export function getNetworkAction() {
    return async dispatch => {
        dispatch(getNetworkRequest());
        return client
            .get('/Shop/GetShop')
            .then(result => {
                console.log('getNetworkAction Result> ', result.data);
                dispatch(getNetworkSuccess(result.data));
            })
            .catch(error => {
                console.log('getNetworkAction Error>', error.response);
                dispatch(getNetworkFailure(error.response.data ? error.response.data : 'Ocorreu um erro inesperado. Tente novamente.'));
            });
    };
}
