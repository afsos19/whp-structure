import ACTIONSNAME from '../actionsName';

export const initialState = {
    isLoading: false,
    error: false,
    success: false,
    errorUsers: false,
    successUsers: false,
    errorMessage: null,
    data: null,
    dataUsers: null,
};

const mgmReducer = (state = initialState, action) => {
    let newState;
    switch (action.type) {
        case ACTIONSNAME.MGM_RESET:
            newState = {
                ...state,
                isLoading: false,
                error: false,
                success: false,
                errorUsers: false,
                successUsers: false,
                errorMessage: null,
            };
            return newState;
            case ACTIONSNAME.MGM_CODE_REQUEST:
                newState = {
                    ...state,
                    isLoading: true,
                    success: false,
                };
                return newState;
            case ACTIONSNAME.MGM_CODE_SUCCESS:
                newState = {
                    ...state,
                    isLoading: false,
                    error: false,
                    success: true,
                    errorMessage: null,
                    data: action.data,
                };
                return newState;
            case ACTIONSNAME.MGM_CODE_FAILURE:
                newState = {
                    ...state,
                    isLoading: false,
                    error: true,
                    success: false,
                    errorMessage: action.error,
                };
                return newState;
            case ACTIONSNAME.MGM_SMS_REQUEST:
                newState = {
                    ...state,
                    isLoading: true,
                    success: false,
                };
                return newState;
            case ACTIONSNAME.MGM_SMS_SUCCESS:
                newState = {
                    ...state,
                    isLoading: false,
                    error: false,
                    success: true,
                    errorMessage: null,
                };
                return newState;
            case ACTIONSNAME.MGM_SMS_FAILURE:
                newState = {
                    ...state,
                    isLoading: false,
                    error: true,
                    success: false,
                    errorMessage: action.error,
                };
                return newState;
            case ACTIONSNAME.INVITED_USERS_REQUEST:
                newState = {
                    ...state,
                    isLoading: true,
                    successUsers: false,
                };
                return newState;
            case ACTIONSNAME.INVITED_USERS_SUCCESS:
                newState = {
                    ...state,
                    isLoading: false,
                    errorUsers: false,
                    successUsers: true,
                    errorMessage: null,
                    dataUsers: action.data,
                };
                return newState;
            case ACTIONSNAME.INVITED_USERS_FAILURE:
                newState = {
                    ...state,
                    isLoading: false,
                    errorUsers: true,
                    successUsers: false,
                    errorMessage: action.error,
                };
                return newState;
            case ACTIONSNAME.USER_INVITED_REQUEST:
                newState = {
                    ...state,
                    isLoading: true,
                    success: false,
                };
                return newState;
            case ACTIONSNAME.USER_INVITED_SUCCESS:
                newState = {
                    ...state,
                    isLoading: false,
                    error: false,
                    success: true,
                    errorMessage: null,
                };
                return newState;
            case ACTIONSNAME.USER_INVITED_FAILURE:
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

export default mgmReducer;
