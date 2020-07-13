import { AsyncStorage } from 'react-native';
import { api, rotas } from 'utils/api';

import { GET_USER_BY_CPF, LOADING, ERROR } from './actionTypes';

export const getUser = token => ({
    type: GET_USER_BY_CPF,
    token,
});

export const loading = bool => ({
    type: LOADING,
    isLoading: bool,
});

export const error = err => ({
    type: ERROR,
    error: err,
});

export const getUserCpf = cpf => async dispatch => {
    try {
        const res = await api.get(rotas.primeiroAcesso, { params: { cpf } });
        const token = res.CPF;
        await AsyncStorage.setItem('userToken', token);

        dispatch(getUser(token));
        dispatch(loading(false));
    } catch (err) {
        dispatch(error(err.message));
        dispatch(loading(false));
    }
};
