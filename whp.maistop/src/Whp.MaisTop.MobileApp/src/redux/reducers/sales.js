import ACTIONSNAME from '../actionsName';

export const initialState = {
    isLoading: false,
    error: false,
    success: false,
    errorMessage: null,
    data: null,
};

const salesReducer = (state = initialState, action) => {
    let newState;
    switch (action.type) {
        case ACTIONSNAME.SALES_RESET:
            newState = {
                ...state,
                isLoading: false,
                error: false,
                success: false,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.GET_SALES_REQUEST:
            newState = {
                ...state,
                isLoading: true,
                success: false,
            };
            return newState;
        case ACTIONSNAME.GET_SALES_SUCCESS:
            newState = {
                ...state,
                data: action.data,
                isLoading: false,
                error: false,
                success: true,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.GET_SALES_FAILURE:
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

export default salesReducer;
