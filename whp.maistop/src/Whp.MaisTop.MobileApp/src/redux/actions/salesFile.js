import client from 'utils/api';
import ACTIONSNAME from '../actionsName';

export function salesFileReset() {
    return {
        type: ACTIONSNAME.SALES_FILE_RESET,
    };
}

// Get sales file status
export function getSalesFileRequest() {
    return {
        type: ACTIONSNAME.GET_SALES_FILE_REQUEST,
    };
}

export function getSalesFileSuccess(data) {
    return {
        type: ACTIONSNAME.GET_SALES_FILE_SUCCESS,
        data,
    };
}

export function getSalesFileFailure(error) {
    return {
        type: ACTIONSNAME.GET_SALES_FILE_FAILURE,
        error,
    };
}

export function getSalesFileAction(data) {
    return async dispatch => {
        dispatch(getSalesFileRequest());
        return client
            .post('Importation/GetSaleFileStatus', data)
            .then(result => {
                console.log('getSalesFileAction result>', result.data);
                dispatch(getSalesFileSuccess(result.data));
            })
            .catch(error => {
                console.log('getSalesFileAction Error>', error.response);
                dispatch(getSalesFileFailure('Error'));
            });
    };
}

// Send sales file
export function sendSalesFileRequest() {
    return {
        type: ACTIONSNAME.SEND_SALES_FILE_REQUEST,
    };
}

export function sendSalesFileSuccess() {
    return {
        type: ACTIONSNAME.SEND_SALES_FILE_SUCCESS,
    };
}

export function sendSalesFileFailure(error) {
    return {
        type: ACTIONSNAME.SEND_SALES_FILE_FAILURE,
        error,
    };
}

export function sendSalesFileAction(data) {
    return async dispatch => {
        dispatch(sendSalesFileRequest());
        return client
            .post('Importation/SendSaleFile', data, {
                headers: { 'Content-Type': 'multipart/form-data' },
            })
            .then(result => {
                console.log('sendSalesFileAction result>', result.data);
                dispatch(sendSalesFileSuccess());
            })
            .catch(error => {
                console.log('sendSalesFileAction Error>', error.response);
                dispatch(sendSalesFileFailure('Error'));
            });
    };
}
