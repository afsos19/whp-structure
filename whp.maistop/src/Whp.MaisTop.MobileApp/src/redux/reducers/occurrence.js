import ACTIONSNAME from '../actionsName';

export const initialState = {
    isLoading: false,
    isLoadingMsg: false,
    error: false,
    success: false,
    successMsg: false,
    errorMessage: null,
    open: null,
    close: null,
    messages: [],
};

const occurrenceReducer = (state = initialState, action) => {
    let newState;
    switch (action.type) {
        case ACTIONSNAME.OCCURRENCE_RESET:
            newState = {
                ...state,
                isLoading: false,
                isLoadingMsg: false,
                error: false,
                success: false,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.NEW_OCCURRENCE_REQUEST:
            newState = {
                ...state,
                isLoading: true,
                successMsg: false,
            };
            return newState;
        case ACTIONSNAME.NEW_OCCURRENCE_SUCCESS:
            newState = {
                ...state,
                isLoading: false,
                error: false,
                successMsg: true,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.NEW_OCCURRENCE_FAILURE:
            newState = {
                ...state,
                isLoading: false,
                error: true,
                successMsg: false,
                successMessage: null,
                errorMessage: action.error,
            };
            return newState;
        case ACTIONSNAME.GET_OCCURRENCE_REQUEST:
            newState = {
                ...state,
                isLoading: true,
                success: false,
            };
            return newState;
        case ACTIONSNAME.GET_OCCURRENCE_SUCCESS:
            newState = {
                ...state,
                isLoading: false,
                error: false,
                success: true,
                errorMessage: null,
                open: action.open,
                close: action.close,
            };
            return newState;
        case ACTIONSNAME.GET_OCCURRENCE_FAILURE:
            newState = {
                ...state,
                isLoading: false,
                error: true,
                success: false,
                successMessage: null,
                errorMessage: action.error,
            };
            return newState;
        case ACTIONSNAME.GET_MSG_OCCURRENCE_REQUEST:
            newState = {
                ...state,
                isLoading: true,
                success: false,
            };
            return newState;
        case ACTIONSNAME.GET_MSG_OCCURRENCE_SUCCESS:
            newState = {
                ...state,
                isLoading: false,
                error: false,
                success: true,
                errorMessage: null,
                messages: action.messages,
            };
            return newState;
        case ACTIONSNAME.GET_MSG_OCCURRENCE_FAILURE:
            newState = {
                ...state,
                isLoading: false,
                error: true,
                success: false,
                successMessage: null,
                errorMessage: action.error,
            };
            return newState;
        case ACTIONSNAME.SEND_MSG_OCCURRENCE_REQUEST:
            newState = {
                ...state,
                isLoadingMsg: true,
                success: false,
            };
            return newState;
        case ACTIONSNAME.SEND_MSG_OCCURRENCE_SUCCESS:
            const messages = state.messages;
            messages.push(action.message);
            newState = {
                ...state,
                isLoadingMsg: false,
                error: false,
                success: true,
                errorMessage: null,
                messages,
            };
            return newState;
        case ACTIONSNAME.SEND_MSG_OCCURRENCE_FAILURE:
            newState = {
                ...state,
                isLoadingMsg: false,
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

export default occurrenceReducer;
