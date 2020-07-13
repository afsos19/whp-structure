import ACTIONSNAME from '../actionsName';

export const initialState = {
    isLoading: false,
    error: false,
    success: false,
    errorMessage: null,
    focusProducts: null,
    groupFocusProducts: null,
    groupAllProducts: null,
};

const productsReducer = (state = initialState, action) => {
    let newState;
    switch (action.type) {
        case ACTIONSNAME.PRODUCTS_RESET:
            newState = {
                ...state,
                isLoading: false,
                error: false,
                success: false,
                errorMessage: null,
            };
            return newState;
        // Focus Products
        case ACTIONSNAME.GET_FOCUS_PRODUCTS_REQUEST:
            newState = {
                ...state,
                isLoading: true,
                success: false,
            };
            return newState;
        case ACTIONSNAME.GET_FOCUS_PRODUCTS_SUCCESS:
            newState = {
                ...state,
                focusProducts: action.data,
                groupFocusProducts: action.groupData,
                isLoading: false,
                error: false,
                success: true,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.GET_FOCUS_PRODUCTS_FAILURE:
            newState = {
                ...state,
                isLoading: false,
                error: true,
                success: false,
                successMessage: null,
                errorMessage: action.error,
            };
            return newState;
        // All Products
        case ACTIONSNAME.GET_ALL_PRODUCTS_REQUEST:
            newState = {
                ...state,
                isLoading: true,
                success: false,
            };
            return newState;
        case ACTIONSNAME.GET_ALL_PRODUCTS_SUCCESS:
            newState = {
                ...state,
                groupAllProducts: action.data,
                isLoading: false,
                error: false,
                success: true,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.GET_ALL_PRODUCTS_FAILURE:
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

export default productsReducer;
