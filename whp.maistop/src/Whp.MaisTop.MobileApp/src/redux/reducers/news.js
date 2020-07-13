import ACTIONSNAME from '../actionsName';

export const initialState = {
    isLoading: false,
    error: false,
    success: false,
    errorMessage: null,
    data: null,
};

const newsReducer = (state = initialState, action) => {
    let newState;
    switch (action.type) {
        case ACTIONSNAME.GET_NEWS_REQUEST:
            newState = {
                ...state,
                isLoading: true,
                success: false,
            };
            return newState;
        case ACTIONSNAME.GET_NEWS_SUCCESS:
            newState = {
                ...state,
                data: action.data,
                isLoading: false,
                error: false,
                success: true,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.GET_NEWS_FAILURE:
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

export default newsReducer;
