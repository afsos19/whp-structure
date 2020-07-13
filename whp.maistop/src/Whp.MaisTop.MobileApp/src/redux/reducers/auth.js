import ACTIONSNAME from '../actionsName';

export const initialState = {
    isLoading: false,
    isLoadingShop: false,
    isLoadingAcademy: false,
    errorShop: false,
    errorAcademy: false,
    error: false,
    success: false,
    successShop: false,
    successAcademy: false,
    errorMessage: null,
    shopUrl: null,
    academyUrl: null,
    messageLogin: null,
    token: null,
    onlyShop: null,
};

const authReducer = (state = initialState, action) => {
    let newState;
    switch (action.type) {
        case ACTIONSNAME.AUTH_RESET:
            newState = {
                ...state,
                isLoading: false,
                isLoadingShop: false,
                isLoadingAcademy: false,
                error: false,
                errorShop: false,
                errorAcademy: false,
                success: false,
                successShop: false,
                successAcademy: false,
                errorMessage: null,
                shopUrl: null,
                academyUrl: null,
                messageLogin: null,
                onlyShop: null,
            };
            return newState;
        // Logout
        case ACTIONSNAME.LOGOUT:
            newState = {
                ...state,
                isLoading: false,
                error: false,
                success: false,
                errorMessage: null,
            };
            return newState;
        // Login
        case ACTIONSNAME.LOGIN_REQUEST:
            newState = {
                ...state,
                isLoading: true,
                success: false,
            };
            return newState;
        case ACTIONSNAME.LOGIN_SUCCESS:
            console.log("action data>>> ", action.data)
            const onlyShop = action.data.link ? action.data.link : null
            newState = {
                ...state,
                isLoading: false,
                error: false,
                success: true,
                errorMessage: null,
                messageLogin: action.data.message,
                token: action.data.token,
                onlyShop,
            };
            return newState;
        case ACTIONSNAME.LOGIN_FAILURE:
            newState = {
                ...state,
                isLoading: false,
                error: true,
                success: false,
                errorMessage: action.error,
            };
            return newState;
        // Shop
        case ACTIONSNAME.GET_SHOP_REQUEST:
            newState = {
                ...state,
                isLoadingShop: true,
                successShop: false,
            };
            return newState;
        case ACTIONSNAME.GET_SHOP_SUCCESS:
            newState = {
                ...state,
                shopUrl: action.data,
                isLoadingShop: false,
                errorShop: false,
                successShop: true,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.GET_SHOP_FAILURE:
            newState = {
                ...state,
                isLoadingShop: false,
                errorShop: true,
                successShop: false,
                errorMessage: action.error,
            };
            return newState;
        // Academy
        case ACTIONSNAME.GET_ACADEMY_REQUEST:
            newState = {
                ...state,
                isLoadingAcademy: true,
                successAcademy: false,
            };
            return newState;
        case ACTIONSNAME.GET_ACADEMY_SUCCESS:
            newState = {
                ...state,
                academyUrl: action.data,
                isLoadingAcademy: false,
                errorAcademy: false,
                successAcademy: true,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.GET_ACADEMY_FAILURE:
            newState = {
                ...state,
                isLoadingAcademy: false,
                errorAcademy: true,
                successAcademy: false,
                errorMessage: action.error,
            };
            return newState;
            case ACTIONSNAME.NEW_PASSWORD_REQUEST:
                newState = {
                    ...state,
                    isLoading: true,
                    success: false,
                };
                return newState;
            case ACTIONSNAME.NEW_PASSWORD_SUCCESS:
                newState = {
                    ...state,
                    isLoading: false,
                    error: false,
                    success: true,
                    errorMessage: null,
                };
                return newState;
            case ACTIONSNAME.NEW_PASSWORD_FAILURE:
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
