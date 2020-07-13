import ACTIONSNAME from '../actionsName';

export const initialState = {
    isLoading: false,
    isLoadingUser: false,
    isLoadingTraining: false,
    isLoadingSales: false,
    error: false,
    errorUser: false,
    errorTraining: false,
    errorSales: false,
    success: false,
    successUser: false,
    successTraining: false,
    successSales: false,
    successMessage: null,
    errorMessage: null,
    dataUsers: null,
    dataUsersDetails: null,
    dataSales: null,
    dataSalesDetail: null,
    dataTraining: null,
    dataTrainingDetails: null,
};

const teamReducer = (state = initialState, action) => {
    let newState;
    switch (action.type) {
        case ACTIONSNAME.TEAM_RESET:
            newState = {
                ...state,
                isLoading: false,
                isLoadingUser: false,
                isLoadingTraining: false,
                isLoadingSales: false,
                error: false,
                errorUser: false,
                errorTraining: false,
                errorSales: false,
                success: false,
                successUser: false,
                successTraining: false,
                successSales: false,
                successMessage: null,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.GET_TEAM_USERS_REQUEST:
            newState = {
                ...state,
                isLoading: true,
                success: false,
            };
            return newState;
        case ACTIONSNAME.GET_TEAM_USERS_SUCCESS:
            newState = {
                ...state,
                dataUsers: action.data,
                dataUsersDetails: action.data,
                isLoading: false,
                error: false,
                success: true,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.GET_TEAM_USERS_FAILURE:
            newState = {
                ...state,
                isLoading: false,
                error: true,
                success: false,
                successMessage: null,
                errorMessage: action.error,
            };
            return newState;
        case ACTIONSNAME.GET_TEAM_USERS_DETAIL_REQUEST:
            newState = {
                ...state,
                isLoadingUser: true,
                successUser: false,
            };
            return newState;
        case ACTIONSNAME.GET_TEAM_USERS_DETAIL_SUCCESS:
            newState = {
                ...state,
                dataUsersDetails: action.data,
                isLoadingUser: false,
                error: false,
                successUser: true,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.GET_TEAM_USERS_DETAIL_FAILURE:
            newState = {
                ...state,
                isLoadingUser: false,
                errorUser: true,
                successUser: false,
                successMessage: null,
                errorMessage: action.error,
            };
            return newState;
        case ACTIONSNAME.GET_TEAM_SALES_REQUEST:
            newState = {
                ...state,
                isLoading: true,
                success: false,
            };
            return newState;
        case ACTIONSNAME.GET_TEAM_SALES_SUCCESS:
            newState = {
                ...state,
                dataSales: action.data,
                dataSalesDetail: action.data,
                isLoading: false,
                error: false,
                success: true,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.GET_TEAM_SALES_FAILURE:
            newState = {
                ...state,
                isLoading: false,
                error: true,
                success: false,
                successMessage: null,
                errorMessage: action.error,
            };
            return newState;
        case ACTIONSNAME.GET_TEAM_SALES_DETAIL_REQUEST:
            newState = {
                ...state,
                isLoadingSales: true,
                success: false,
            };
            return newState;
        case ACTIONSNAME.GET_TEAM_SALES_DETAIL_SUCCESS:
            newState = {
                ...state,
                dataSalesDetail: action.data,
                isLoadingSales: false,
                errorSales: false,
                successSales: true,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.GET_TEAM_SALES_DETAIL_FAILURE:
            newState = {
                ...state,
                isLoadingSales: false,
                errorSales: true,
                successSales: false,
                successMessage: null,
                errorMessage: action.error,
            };
            return newState;
        case ACTIONSNAME.GET_TEAM_TRAINING_REQUEST:
            newState = {
                ...state,
                isLoading: true,
                success: false,
            };
            return newState;
        case ACTIONSNAME.GET_TEAM_TRAINING_SUCCESS:
            newState = {
                ...state,
                dataTraining: action.data,
                dataTrainingDetails: action.data,
                isLoading: false,
                error: false,
                success: true,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.GET_TEAM_TRAINING_FAILURE:
            newState = {
                ...state,
                isLoading: false,
                error: true,
                success: false,
                successMessage: null,
                errorMessage: action.error,
            };
            return newState;
        case ACTIONSNAME.GET_TEAM_TRAINING_DETAIL_REQUEST:
            newState = {
                ...state,
                isLoadingTraining: true,
                successTraining: false,
            };
            return newState;
        case ACTIONSNAME.GET_TEAM_TRAINING_DETAIL_SUCCESS:
            newState = {
                ...state,
                dataTrainingDetails: action.data,
                isLoadingTraining: false,
                errorTraining: false,
                successTraining: true,
                errorMessage: null,
            };
            return newState;
        case ACTIONSNAME.GET_TEAM_TRAINING_DETAIL_FAILURE:
            newState = {
                ...state,
                isLoadingTraining: false,
                errorTraining: true,
                successTraining: false,
                successMessage: null,
                errorMessage: action.error,
            };
            return newState;
        default:
            return state;
    }
};

export default teamReducer;
