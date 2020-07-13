import client from 'utils/api';
import { unmaskCPF } from 'utils/masks';
import ACTIONSNAME from '../actionsName';

export function firstAccessReset() {
    return {
        type: ACTIONSNAME.FIRST_ACCESS_RESET,
    };
}

// First Access
export function firstAccessRequest() {
    return {
        type: ACTIONSNAME.FIRST_ACCESS_REQUEST,
    };
}

export function firstAccessSuccess(data) {
    return {
        type: ACTIONSNAME.FIRST_ACCESS_SUCCESS,
        data,
    };
}

export function firstAccessFailure(error) {
    return {
        type: ACTIONSNAME.FIRST_ACCESS_FAILURE,
        error,
    };
}

export function firstAccessAction(cpf) {
    return async dispatch => {
        dispatch(firstAccessRequest());
        return client
            .get(`/Authentication/FirstAccess/${unmaskCPF(cpf)}`)
            .then(result => {
                console.log('firstAccessAction Success> ', result);
                dispatch(firstAccessSuccess(result.data));
            })
            .catch(error => {
                console.log('firstAccessAction Error> ', error.response);
                dispatch(
                    firstAccessFailure(
                        'Seu CPF ainda não está cadastrado. Entre em contato com o Gestor da Informação da sua revenda e peça para incluir.'
                    )
                );
            });
    };
}
