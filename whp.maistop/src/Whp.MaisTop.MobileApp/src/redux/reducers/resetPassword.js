import ACTIONSNAME from '../actionsName';

export const initialState = {
    isLoading: false,
    error: false,
    success: false,
    successMessage: null,
    errorMessage: null,
};

const authReducer = (state = initialState, action) => {
    let newState;
    switch (action.type) {
        case ACTIONSNAME.RESET_PASSWORD_RESET:
            newState = {
                ...state,
                isLoading: false,
                error: false,
                success: false,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.RESET_PASSWORD_REQUEST:
            newState = {
                ...state,
                isLoading: true,
                success: false,
            };
            return newState;
        case ACTIONSNAME.RESET_PASSWORD_SUCCESS:
            newState = {
                ...state,
                successMessage: action.message,
                isLoading: false,
                error: false,
                success: true,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.RESET_PASSWORD_FAILURE:
            newState = {
                ...state,
                isLoading: false,
                error: true,
                success: false,
                successMessage: null,
                errorMessage: action.error,
            };
            return newState;
        default:
            return state;
    }
};

export default authReducer;
