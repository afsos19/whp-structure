import { GET_USER_BY_CPF, LOADING, ERROR } from '../actions/actionTypes';

const loginReducer = (
    state = {
        cpf: '',
        isLoading: false,
        error: null,
    },
    action
) => {
    switch (action.type) {
        case GET_USER_BY_CPF:
            return { ...state, token: action.token };
        case LOADING:
            return { ...state, loading: action.isLoading };
        case ERROR:
            return { ...state, error: action.error };
        default:
            return state;
    }
};

export default loginReducer;
