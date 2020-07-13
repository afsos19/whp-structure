import ACTIONSNAME from '../actionsName';

export const initialState = {
    isLoading: false,
    error: false,
    success: false,
    errorMessage: null,
    user: null,
    shops: null,
};

const firstAccessReducer = (state = initialState, action) => {
    let newState;
    switch (action.type) {
        case ACTIONSNAME.FIRST_ACCESS_RESET:
            newState = {
                ...state,
                isLoading: false,
                error: false,
                success: false,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.FIRST_ACCESS_REQUEST:
            newState = {
                ...state,
                isLoading: true,
                success: false,
            };
            return newState;
        case ACTIONSNAME.FIRST_ACCESS_SUCCESS:
            newState = {
                ...state,
                isLoading: false,
                error: false,
                success: true,
                errorMessage: null,
                user: action.data.user,
                shops: action.data.shops,
            };
            return newState;
        case ACTIONSNAME.FIRST_ACCESS_FAILURE:
            newState = {
                ...state,
                isLoading: false,
                error: true,
                success: false,
                errorMessage: action.error,
            };
            return newState;
        default:
            return state;
    }
};

export default firstAccessReducer;
