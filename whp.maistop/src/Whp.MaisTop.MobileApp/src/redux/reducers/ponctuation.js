import ACTIONSNAME from '../actionsName';

export const initialState = {
    isLoading: false,
    error: false,
    success: false,
    errorMessage: null,
    extract: null,
    credits: null,
};

const ponctuationReducer = (state = initialState, action) => {
    let newState;
    switch (action.type) {
        case ACTIONSNAME.PONCTUATION_RESET:
            newState = {
                ...state,
                isLoading: false,
                error: false,
                success: false,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.GET_PONCTUATION_REQUEST:
            newState = {
                ...state,
                isLoading: true,
                success: false,
            };
            return newState;
        case ACTIONSNAME.GET_PONCTUATION_SUCCESS:
            newState = {
                ...state,
                extract: action.data,
                isLoading: false,
                error: false,
                success: true,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.GET_PONCTUATION_FAILURE:
            newState = {
                ...state,
                isLoading: false,
                error: true,
                success: false,
                successMessage: null,
                errorMessage: action.error,
            };
            return newState;
        case ACTIONSNAME.GET_CREDITS_REQUEST:
            newState = {
                ...state,
                isLoading: true,
                success: false,
            };
            return newState;
        case ACTIONSNAME.GET_CREDITS_SUCCESS:
            newState = {
                ...state,
                credits: action.data,
                isLoading: false,
                error: false,
                success: true,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.GET_CREDITS_FAILURE:
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

export default ponctuationReducer;
