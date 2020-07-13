import * as React from 'react';
import styled from 'styled-components/native';
import { ActivityIndicator, AsyncStorage } from 'react-native';
import GradientBackground from '../../components/common/GradientBackground';
import images from '../../res/images';
import jwtDecode from 'jwt-decode';
import ROUTENAMES from '../../navigation/routeName';
//Redux
import { connect } from 'react-redux';
import { getUserAction, userReset } from '../../redux/actions/user';
import { getNetworkAction, networkReset } from '../../redux/actions/network';
import { getPonctuationAction, ponctuationReset } from '../../redux/actions/ponctuation';
import { getNewsAction } from '../../redux/actions/news';
import { mgmCodeAction, mgmReset } from '../../redux/actions/mgm';
import { getAllProductsAction, getFocusProductsAction, productsReset } from '../../redux/actions/products';
import { getCampaignsAction, campaignReset } from '../../redux/actions/campaing';
import { getShopAction, getAcademyAction, authReset } from '../../redux/actions/auth';
import Logo from '../../components/common/Logo';

const Container = styled(GradientBackground)`
    flex: 1;
`;

const WrapperCenter = styled.View`
    flex: 1;
    align-items: center;
    justify-content: center;
`;

const CustomImage = styled.Image`
    align-self: center;
    width: 100;
    height: 100;
    /* height: ; */
`;

class Loading extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        role: ''
    }

    componentDidMount() {
        const {
            getUser,
            resetAuth,
            resetUser,
            resetNetwork,
            resetPonctuation,
            resetMgm,
            resetProducts,
            resetCampaign
        } = this.props;
        resetAuth();
        resetUser();
        resetNetwork();
        resetPonctuation();
        resetMgm();
        resetProducts();
        resetCampaign();

        AsyncStorage.getItem('token').then(value => {
            const decoded = jwtDecode(value);
            const role = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
            this.setState({ role });
            getUser(decoded.id);
        });
    }

    componentDidUpdate(nextProps) {
        const { 
            user, 
            getCampaigns,
            getAcademy,
            getNetwork,
            getPunctuation,
            getNews,
            getMgmCode,
            getShop,
            getAllProducts,
            getFocusProducts,
        } = this.props;
        const { role } = this.state;
        if (user.success && user.success !== nextProps.user.success) {
            getNetwork();
            getPunctuation();
            getNews();
            getMgmCode();
            getShop();
            getAllProducts();
            getFocusProducts();
            if (role === 'VENDEDOR') {
                getAcademy();
                getCampaigns();
            } else if (role === 'GERENTE') {
                getAcademy();                
            }
        }
    }

    render() {
        const { 
            user,
            network,
            ponctuation,
            mgm,
            navigation,
            auth,
            resetAuth,
            resetUser,
            resetNetwork,
            resetPonctuation,
            resetMgm,
            resetProducts,
            resetCampaign
         } = this.props;
        const { role } = this.state;
        if (role === 'VENDEDOR') {
            if (user.success && network.success && ponctuation.success && mgm.success && auth.successAcademy) {
                navigation.navigate(ROUTENAMES.HOME);
            }
        } else if (role === 'VENDEDOR') {
            if (user.success && network.success && ponctuation.success && mgm.success && auth.successShop && auth.successAcademy) {
                navigation.navigate(ROUTENAMES.HOME);
            }
        } else {
            if (user.success && network.success && ponctuation.success && mgm.success && auth.successShop) {
                navigation.navigate(ROUTENAMES.HOME);
            }
        }

        if (user.error || network.error || ponctuation.error || mgm.error || auth.errorAcademy) {
            AsyncStorage.clear();
            resetAuth();
            resetUser();
            resetNetwork();
            resetPonctuation();
            resetMgm();
            resetProducts();
            resetCampaign();
            navigation.navigate(ROUTENAMES.LOGIN);        
        }

        return (
            <Container>
                <WrapperCenter>
                    <Logo height="50px" img={images.logo} />
                    <ActivityIndicator style={{ marginTop: 16 }}/>
                    {/* <CommonText type="normal" color="dark" center>Carregando...</CommonText> */}
                </WrapperCenter>
            </Container>
        );
    }
}

// Redux
const mapStateToProps = state => ({
    auth: state.auth,
    user: state.user,
    network: state.network,
    ponctuation: state.ponctuation,
    news: state.news,
    auth: state.auth,
    mgm: state.mgm,
    products: state.products,
    campaign: state.campaign,
});

const mapDispatchToProps = dispatch => ({
    getUser: id => dispatch(getUserAction(id)),
    getNetwork: () => dispatch(getNetworkAction()),
    getPunctuation: () => dispatch(getPonctuationAction()),
    getNews: () => dispatch(getNewsAction()),
    getShop: () => dispatch(getShopAction()),
    getMgmCode: () => dispatch(mgmCodeAction()),
    getCampaigns: () => dispatch(getCampaignsAction()),
    getShop: () => dispatch(getShopAction()),
    getAcademy: () => dispatch(getAcademyAction()),
    getAllProducts: () => dispatch(getAllProductsAction()),
    getFocusProducts: () => dispatch(getFocusProductsAction()),
    //reset
    resetAuth: () => dispatch(authReset()),
    resetUser: () => dispatch(userReset()),
    resetNetwork: () => dispatch(networkReset()),
    resetPonctuation: () => dispatch(ponctuationReset()),
    resetMgm: () => dispatch(mgmReset()),
    resetProducts: () => dispatch(productsReset()),
    resetCampaign: () => dispatch(campaignReset()),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Loading);
