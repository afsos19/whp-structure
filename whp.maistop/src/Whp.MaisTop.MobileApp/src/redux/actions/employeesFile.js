import client from 'utils/api';
import ACTIONSNAME from '../actionsName';

export function employeesFileReset() {
    return {
        type: ACTIONSNAME.EMPLOYEES_FILE_RESET,
    };
}

// Get employees file status
export function getEmployeesFileRequest() {
    return {
        type: ACTIONSNAME.GET_EMPLOYEES_FILE_REQUEST,
    };
}

export function getEmployeesFileSuccess(data) {
    return {
        type: ACTIONSNAME.GET_EMPLOYEES_FILE_SUCCESS,
        data,
    };
}

export function getEmployeesFileFailure(error) {
    return {
        type: ACTIONSNAME.GET_EMPLOYEES_FILE_FAILURE,
        error,
    };
}

export function getEmployeesFileAction(data) {
    return async dispatch => {
        dispatch(getEmployeesFileRequest());
        return client
            .post('Importation/GetHierarchyFileStatus', data)
            .then(result => {
                console.log('getEmployeesFileAction result>', result.data);
                dispatch(getEmployeesFileSuccess(result.data));
            })
            .catch(error => {
                console.log('getEmployeesFileAction Error>', error.response);
                dispatch(getEmployeesFileFailure('Error'));
            });
    };
}

// Send employees file
export function sendEmployeesFileRequest() {
    return {
        type: ACTIONSNAME.SEND_EMPLOYEES_FILE_REQUEST,
    };
}

export function sendEmployeesFileSuccess() {
    return {
        type: ACTIONSNAME.SEND_EMPLOYEES_FILE_SUCCESS,
    };
}

export function sendEmployeesFileFailure(error) {
    return {
        type: ACTIONSNAME.SEND_EMPLOYEES_FILE_FAILURE,
        error,
    };
}

export function sendEmployeesFileAction(data) {
    console.log('DATA> ', data);
    return async dispatch => {
        dispatch(sendEmployeesFileRequest());
        return client
            .post('Importation/SendHierarchyFile', data, {
                headers: { 'Content-Type': 'multipart/form-data' },
            })
            .then(result => {
                console.log('sendEmployeesFileAction result>', result.data);
                dispatch(sendEmployeesFileSuccess());
            })
            .catch(error => {
                console.log('sendEmployeesFileAction Error>', error.response);
                dispatch(sendEmployeesFileFailure('Error'));
            });
    };
}
