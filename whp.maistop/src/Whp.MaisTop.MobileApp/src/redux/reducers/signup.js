import ACTIONSNAME from '../actionsName';

export const initialState = {
    isLoading: false,
    error: false,
    success: false,
    errorMessage: null,
    user: null,
    shops: null,
};

const signupReducer = (state = initialState, action) => {
    let newState;
    switch (action.type) {
        case ACTIONSNAME.SIGNUP_RESET:
            newState = {
                ...state,
                isLoading: false,
                error: false,
                success: false,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.PRE_REGISTRATION_REQUEST:
            newState = {
                ...state,
                isLoading: true,
                success: false,
            };
            return newState;
        case ACTIONSNAME.PRE_REGISTRATION_SUCCESS:
            newState = {
                ...state,
                isLoading: false,
                error: false,
                success: true,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.PRE_REGISTRATION_FAILURE:
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

export default signupReducer;
