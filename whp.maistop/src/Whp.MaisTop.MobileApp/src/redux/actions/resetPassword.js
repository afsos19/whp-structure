import client from 'utils/api';
import { unmaskCPF } from 'utils/masks';
import ACTIONSNAME from '../actionsName';

export function resetPasswordReset() {
    return {
        type: ACTIONSNAME.RESET_PASSWORD_RESET,
    };
}

export function resetPasswordRequest() {
    return {
        type: ACTIONSNAME.RESET_PASSWORD_REQUEST,
    };
}

export function resetPasswordSuccess(message) {
    return {
        type: ACTIONSNAME.RESET_PASSWORD_SUCCESS,
        message,
    };
}

export function resetPasswordFailure(error) {
    return {
        type: ACTIONSNAME.RESET_PASSWORD_FAILURE,
        error,
    };
}

export function resetPasswordAction(cpf) {
    return async dispatch => {
        dispatch(resetPasswordRequest());
        console.log(`/Authentication/ForgotPassword/${unmaskCPF(cpf)}`);
        return client
            .get(`/Authentication/ForgotPassword/${unmaskCPF(cpf)}`)
            .then(result => {
                console.log('resetPasswordAction Result> ', result);
                dispatch(resetPasswordSuccess(result.data.message));
            })
            .catch(error => {
                console.log('resetPasswordAction Error>', error.response);
                dispatch(
                    resetPasswordFailure(
                        error.response.data ? error.response.data : 'Seu CPF ainda não está cadastrado. Entre em contato com o Gestor da Informação da sua revenda e peça para incluir.'
                    )
                );
            });
    };
}
