import client from 'utils/api';
import ACTIONSNAME from '../actionsName';

export function occurrenceReset() {
    return {
        type: ACTIONSNAME.OCCURRENCE_RESET,
    };
}

// New Occurrence
export function newOccurrenceRequest() {
    return {
        type: ACTIONSNAME.NEW_OCCURRENCE_REQUEST,
    };
}

export function newOccurrenceSuccess() {
    return {
        type: ACTIONSNAME.NEW_OCCURRENCE_SUCCESS,
    };
}

export function newOccurrenceFailure(error) {
    return {
        type: ACTIONSNAME.NEW_OCCURRENCE_FAILURE,
        error,
    };
}

export function newOccurrenceAction(data) {
    return async dispatch => {
        dispatch(newOccurrenceRequest());
        return client
            .post('Occurrence/SaveOccurrence', data, {
                headers: { 'Content-Type': 'multipart/form-data' },
            })
            .then(result => {
                console.log('newOccurrenceAction Result>', result.data);

                dispatch(newOccurrenceSuccess());
            })
            .catch(error => {
                console.log('newOccurrenceAction Error>', error.response);
                dispatch(newOccurrenceFailure('Error'));
            });
    };
}

// Get occurrence
export function getOccurrenceRequest() {
    return {
        type: ACTIONSNAME.GET_OCCURRENCE_REQUEST,
    };
}

export function getOccurrenceSuccess(open, close) {
    return {
        type: ACTIONSNAME.GET_OCCURRENCE_SUCCESS,
        open,
        close,
    };
}

export function getOccurrenceFailure(error) {
    return {
        type: ACTIONSNAME.GET_OCCURRENCE_FAILURE,
        error,
    };
}

export function getOccurrenceAction() {
    return async dispatch => {
        dispatch(getOccurrenceRequest());
        return client
            .get('Occurrence/GetOccurrenceByUser')
            .then(result => {
                console.log('getOccurrenceAction Result>', result.data);
                const open = [];
                const close = [];
                result.data.map(item =>
                    item.occurrenceStatus.description === 'FECHADO'
                        ? close.push(item)
                        : open.push(item)
                );
                console.log('open> ', open);
                dispatch(getOccurrenceSuccess(open, close));
            })
            .catch(error => {
                console.log('getOccurrenceAction Error>', error.response);
                dispatch(getOccurrenceFailure('Error'));
            });
    };
}

// Get messagens occurrence
export function getMsgOccurrenceRequest() {
    return {
        type: ACTIONSNAME.GET_MSG_OCCURRENCE_REQUEST,
    };
}

export function getMsgOccurrenceSuccess(messages) {
    return {
        type: ACTIONSNAME.GET_MSG_OCCURRENCE_SUCCESS,
        messages,
    };
}

export function getMsgOccurrenceFailure(error) {
    return {
        type: ACTIONSNAME.GET_MSG_OCCURRENCE_FAILURE,
        error,
    };
}

export function getMsgOccurrenceAction(id) {
    return async dispatch => {
        dispatch(getMsgOccurrenceRequest());
        return client
            .get(`Occurrence/GetMessagesOccurenceByUser/${id}`)
            .then(result => {
                console.log('getMsgOccurrenceAction Result>', result.data);
                dispatch(getMsgOccurrenceSuccess(result.data));
            })
            .catch(error => {
                console.log('getOccurrenceAction Error>', error.response);
                dispatch(getMsgOccurrenceFailure('Error'));
            });
    };
}

// Send messagen occurrence
export function sendMsgOccurrenceRequest() {
    return {
        type: ACTIONSNAME.SEND_MSG_OCCURRENCE_REQUEST,
    };
}

export function sendMsgOccurrenceSuccess(message) {
    return {
        type: ACTIONSNAME.SEND_MSG_OCCURRENCE_SUCCESS,
        message,
    };
}

export function sendMsgOccurrenceFailure(error) {
    return {
        type: ACTIONSNAME.SEND_MSG_OCCURRENCE_FAILURE,
        error,
    };
}

export function sendMsgOccurrenceAction(data) {
    return async dispatch => {
        dispatch(sendMsgOccurrenceRequest());
        return client
            .post('Occurrence/SaveMessage', data, {
                headers: { 'Content-Type': 'multipart/form-data' },
            })
            .then(result => {
                console.log('sendMsgOccurrenceAction Result>', result.data);
                dispatch(sendMsgOccurrenceSuccess(result.data));
            })
            .catch(error => {
                console.log('sendMsgOccurrenceAction Error>', error.response);
                dispatch(sendMsgOccurrenceFailure('Error'));
            });
    };
}
