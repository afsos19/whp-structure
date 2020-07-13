import client from 'utils/api';
import ACTIONSNAME from '../actionsName';

export function campaignReset() {
    return {
        type: ACTIONSNAME.CAMPAIGN_RESET,
    };
}

// Extract
export function getCampaignsRequest() {
    return {
        type: ACTIONSNAME.GET_CAMPAIGNS_REQUEST,
    };
}

export function getCampaignsSuccess(data) {
    return {
        type: ACTIONSNAME.GET_CAMPAIGNS_SUCCESS,
        data,
    };
}

export function getCampaignsFailure(error) {
    return {
        type: ACTIONSNAME.GET_CAMPAIGNS_FAILURE,
        error,
    };
}

export function getCampaignsAction() {
    return async dispatch => {
        dispatch(getCampaignsRequest());
        return client
            .get('Campaign/GetCampaigns')
            .then(result => {
                console.log('getCampaignsAction result>', result.data);
                dispatch(getCampaignsSuccess(result.data));
            })
            .catch(error => {
                console.log('getCampaignsAction Error>', error.response);
                dispatch(
                    getCampaignsFailure(error.response.data ? error.response.data : 'Error')
                );
            });
    };
}
