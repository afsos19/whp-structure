import ACTIONSNAME from '../actionsName';

export const initialState = {
    isLoading: false,
    error: false,
    success: false,
    verifyError: false,
    verifySuccess: false,
    successMessage: null,
    errorMessage: null,
    data: null,
};

const smsReducer = (state = initialState, action) => {
    let newState;
    switch (action.type) {
        case ACTIONSNAME.SMS_RESET:
            newState = {
                ...state,
                isLoading: false,
                error: false,
                success: false,
                successMessage: null,
                errorMessage: null,
                verifyError: false,
                verifySuccess: false,
            };
            return newState;
        case ACTIONSNAME.GET_SMS_REQUEST:
                newState = {
                    ...state,
                    isLoading: true,
                    success: false,
                };
                return newState;
        case ACTIONSNAME.GET_SMS_SUCCESS:
            newState = {
                ...state,
                data: action.data,
                isLoading: false,
                error: false,
                success: true,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.GET_SMS_FAILURE:
            newState = {
                ...state,
                isLoading: false,
                error: true,
                success: false,
                successMessage: null,
                errorMessage: action.error,
            };
            return newState;
        case ACTIONSNAME.GET_SMS_PASSWORD_REQUEST:
                newState = {
                    ...state,
                    isLoading: true,
                    success: false,
                };
                return newState;
        case ACTIONSNAME.GET_SMS_PASSWORD_SUCCESS:
            newState = {
                ...state,
                data: action.data,
                isLoading: false,
                error: false,
                success: true,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.GET_SMS_PASSWORD_FAILURE:
            newState = {
                ...state,
                isLoading: false,
                error: true,
                success: false,
                successMessage: null,
                errorMessage: action.error,
            };
            return newState;
        // case ACTIONSNAME.VERIFY_SMS_PASSWORD_REQUEST:
        //         newState = {
        //             ...state,
        //             isLoading: true,
        //             verifySuccess: false,
        //         };
        //         return newState;
        // case ACTIONSNAME.VERIFY_SMS_PASSWORD_SUCCESS:
        //     newState = {
        //         ...state,
        //         data: action.data,
        //         isLoading: false,
        //         verifyError: false,
        //         verifySuccess: true,
        //         errorMessage: null,
        //     };
        //     return newState;
        // case ACTIONSNAME.VERIFY_SMS_PASSWORD_FAILURE:
        //     newState = {
        //         ...state,
        //         isLoading: false,
        //         verifyError: true,
        //         verifySuccess: false,
        //         successMessage: null,
        //         errorMessage: action.error,
        //     };
        //     return newState;
        default:
            return state;
    }
};

export default smsReducer;
