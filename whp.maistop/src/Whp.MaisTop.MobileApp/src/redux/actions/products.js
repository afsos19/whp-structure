import client from 'utils/api';
import ACTIONSNAME from '../actionsName';

const groupFocusItems = items => {
    const groupId = [];
    const groupData = [];
    items.map(item => {
        if (!groupId.includes(item.groupProduct.id)) {
            groupId.push(item.groupProduct.id);
            groupData.push(item);
        }
    });
    return groupData;
};

const groupAllItems = items => {
    const groupId = [];
    const groupData = [];
    items.map(item => {
        if (!groupId.includes(item.product.categoryProduct.id)) {
            groupId.push(item.product.categoryProduct.id);
            groupData.push(item);
        }
    });
    return groupData;
};

export function productsReset() {
    return {
        type: ACTIONSNAME.PRODUCTS_RESET,
    };
}

// Get focus products (Supertops)
export function getFocusProductsRequest() {
    return {
        type: ACTIONSNAME.GET_FOCUS_PRODUCTS_REQUEST,
    };
}

export function getFocusProductsSuccess(data, groupData) {
    return {
        type: ACTIONSNAME.GET_FOCUS_PRODUCTS_SUCCESS,
        data,
        groupData,
    };
}

export function getFocusProductsFailure(error) {
    return {
        type: ACTIONSNAME.GET_FOCUS_PRODUCTS_FAILURE,
        error,
    };
}

export function getFocusProductsAction() {
    return async dispatch => {
        dispatch(getFocusProductsRequest());
        return client
            .get('Product/GetFocusProduct')
            .then(result => {
                const group = groupFocusItems(result.data);
                dispatch(getFocusProductsSuccess(result.data, group));
            })
            .catch(error => {
                console.log('getFocusProductsAction Error>', error.response);
                dispatch(getFocusProductsFailure(error.response.data ? error.response.data : 'Ocorreu um erro inesperado. Tente novamente.'));
            });
    };
}

// Get all products participants
export function getAllProductsRequest() {
    return {
        type: ACTIONSNAME.GET_ALL_PRODUCTS_REQUEST,
    };
}

export function getAllProductsSuccess(data) {
    return {
        type: ACTIONSNAME.GET_ALL_PRODUCTS_SUCCESS,
        data,
    };
}

export function getAllProductsFailure(error) {
    return {
        type: ACTIONSNAME.GET_ALL_PRODUCTS_FAILURE,
        error,
    };
}

export function getAllProductsAction() {
    return async dispatch => {
        dispatch(getAllProductsRequest());
        return client
            .get('Product/GetParticipantProduct')
            .then(result => {
                const group = groupAllItems(result.data);
                dispatch(getAllProductsSuccess(group));
            })
            .catch(error => {
                console.log('getAllProductsAction Error>', error.response);
                dispatch(getAllProductsFailure(error.response.data ? error.response.data : 'Ocorreu um erro inesperado. Tente novamente.'));
            });
    };
}
