import React from 'react';
import { AsyncStorage, ActivityIndicator } from 'react-native';
import styled from 'styled-components/native';
import { connect } from 'react-redux';
import jwtDecode from 'jwt-decode';
import images from '../../res/images';
import ROUTENAMES from '../../navigation/routeName';
import { getShopAction, getAcademyAction, authReset } from '../../redux/actions/auth';
import openBrowser from '../../utils/OpenLink';

const Wrapper = styled.View`
    flex-direction: row;
    background-color: ${p => p.theme.colors.background};
    justify-content: space-around;
    align-items: center;
    height: 70px;
    width: 100%;
    border-top-right-radius: 20;
    border-top-left-radius: 20;
`;

const WrapperTouch = styled.TouchableOpacity``;

const ImageIcon = styled.Image`
    height: ${p => p.size};
    width: ${p => p.size};
`;

class Tab extends React.Component {
    state = {
        role: null,
    };

    componentDidMount() {
        AsyncStorage.getItem('token').then(value => {
            const decoded = jwtDecode(value);
            this.setState({
                role: decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
            });
            console.log("role >>>>", decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'])
        });
    }

    render() {
        const { role } = this.state;
        const { openMenu, navigation, getShop, getAcademy, auth } = this.props;
        return (
            <Wrapper>
                <WrapperTouch onPress={() => navigation.navigate(ROUTENAMES.PRODUCTS)}>
                    <ImageIcon source={images.icon1} resizeMode="contain" size={25}/>
                </WrapperTouch>
                {role === 'VENDEDOR' && (
                    <WrapperTouch onPress={() => openBrowser(auth.academyUrl)}>
                        {auth.isLoadingAcademy ? (
                            <ActivityIndicator />
                        ) : (
                            <ImageIcon source={images.icon2} resizeMode="contain" size={27}/>
                        )}
                    </WrapperTouch>
                )}
                {role === 'GERENTE' && (
                    <WrapperTouch onPress={() => openBrowser(auth.academyUrl)}>
                        {auth.isLoadingAcademy ? (
                            <ActivityIndicator />
                        ) : (
                            <ImageIcon source={images.icon2} resizeMode="contain" size={27}/>
                        )}
                    </WrapperTouch>
                )}
                <WrapperTouch onPress={() => {
                    if (auth.shopUrl) {
                        openBrowser(auth.shopUrl)
                    }
                }}>
                    {auth.isLoadingShop ? (
                        <ActivityIndicator />
                    ) : (
                        <ImageIcon source={images.icon3} resizeMode="contain" size={25}/>
                    )}
                </WrapperTouch>
                <WrapperTouch onPress={() => openMenu()}>
                    <ImageIcon source={images.icon4} resizeMode="contain" size={25}/>
                </WrapperTouch>
            </Wrapper>
        );
    }
}

// Redux
const mapStateToProps = state => ({
    auth: state.auth,
});

const mapDispatchToProps = dispatch => ({
    getShop: () => dispatch(getShopAction()),
    getAcademy: () => dispatch(getAcademyAction()),
    reset: () => dispatch(authReset()),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Tab);
