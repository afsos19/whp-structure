import client from 'utils/api';
import ACTIONSNAME from '../actionsName';

export function getNewsRequest() {
    return {
        type: ACTIONSNAME.GET_NEWS_REQUEST,
    };
}

export function getNewsSuccess(data) {
    return {
        type: ACTIONSNAME.GET_NEWS_SUCCESS,
        data,
    };
}

export function getNewsFailure(error) {
    return {
        type: ACTIONSNAME.GET_NEWS_FAILURE,
        error,
    };
}

export function getNewsAction() {
    return async dispatch => {
        dispatch(getNewsRequest());
        return client
            .get('/News/GetNews')
            .then(result => {
                console.log('getNewsAction Result> ', result.data);
                dispatch(getNewsSuccess(result.data));
            })
            .catch(error => {
                console.log('getNewsAction Error>', error.response);
                dispatch(getNewsFailure(error.response.data ? error.response.data : 'Ocorreu um erro inesperado. Tente novamente.'));
            });
    };
}
