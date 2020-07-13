import React from 'react';
import styled from 'styled-components/native';
import Icon from 'react-native-vector-icons/Ionicons';
import jwtDecode from 'jwt-decode';
import { AsyncStorage, Alert, ActivityIndicator } from 'react-native';
import { connect } from 'react-redux';
import { getShopAction, getAcademyAction, authReset } from '../../redux/actions/auth';
import CommonText from '../common/CommonText';
import Logo from '../common/Logo';
import images from '../../res/images';
import ROUTENAMES from '../../navigation/routeName';
import openBrowser from '../../utils/OpenLink';

const Modal = styled.Modal.attrs({
    animationType: 'slide',
    transparent: false,
    visible: p => p.visible,
})``;

const Wrapper = styled.View`
    align-items: center;
    justify-content: center;
    width: 100%;
    height: 100%;
    padding-top: 45;
    padding-bottom: 15;
    padding-horizontal: 20;
    background-color: ${p => p.theme.colors.tertiary};
`;

const WrapperLogo = styled.View`
    flex: 1;
    align-items: center;
    justify-content: center;
`;

const MenuText = styled(CommonText)`
    font-family: ${p => p.theme.fontFamily.menu};
`;

const WrapperText = styled.View`
    flex: 3;
    align-items: center;
    justify-content: center;
`;

const WrapperTouch = styled.TouchableOpacity`
    margin-top: 17;
`;

const WrapperClose = styled.TouchableOpacity`
    align-self: flex-end;
`;

const CloseIcon = styled(Icon).attrs({
    name: 'ios-close',
    size: 25,
    color: p => p.theme.colors.iconLight,
})`
    align-self: flex-end;
`;

class MenuModal extends React.Component {
    state = {
        role: null,
    };

    componentDidMount() {
        AsyncStorage.getItem('token').then(value => {
            const decoded = jwtDecode(value);
            this.setState({
                role: decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
            });
        });
    }


    handleLogout = () => {
        const { navigation, close } = this.props;

        return Alert.alert('Sair da conta ?', `Tem certeza que deseja sair da conta ?`, [
            {
                text: 'Cancelar',
                onPress: () => console.log('Cancel Pressed'),
                style: 'cancel',
            },
            {
                text: 'Sair',
                onPress: () => {
                    AsyncStorage.clear();
                    close();
                    navigation.navigate(ROUTENAMES.LOGIN);
                },
            },
        ]);
    };

    render() {
        const { role } = this.state;
        const { visible = false, close, navigation, auth, getShop, getAcademy } = this.props;

        return (
            <Modal visible={visible}>
                <Wrapper>
                    <WrapperClose onPress={close}>
                        <CloseIcon />
                    </WrapperClose>
                    <WrapperLogo>
                        <Logo img={images.logoWhite} height="60" />
                    </WrapperLogo>
                    <WrapperText>
                        <WrapperTouch
                            onPress={() => {
                                close();
                                navigation.navigate(ROUTENAMES.HOME);
                            }}
                        >
                            <MenuText type="subtitle" color="light" center>
                                HOME
                            </MenuText>
                        </WrapperTouch>
                        <WrapperTouch
                            onPress={() => {
                                close();
                                navigation.navigate(ROUTENAMES.ABOUT);
                            }}
                        >
                            <MenuText type="subtitle" color="light" center>
                                SOBRE O +TOP
                            </MenuText>
                        </WrapperTouch>
                        {role === 'VENDEDOR' && (
                            <WrapperTouch onPress={() => openBrowser(auth.academyUrl)}>
                                <MenuText type="subtitle" color="light" center>
                                    TREINAMENTOS
                                </MenuText>
                            </WrapperTouch>
                        )}
                        {role === 'GERENTE' && (
                            <WrapperTouch onPress={() => openBrowser(auth.academyUrl)}>
                                <MenuText type="subtitle" color="light" center>
                                    TREINAMENTOS
                                </MenuText>
                            </WrapperTouch>
                        )}
                        <WrapperTouch onPress={() => {
                            if (auth.shopUrl) {
                                openBrowser(auth.shopUrl)
                            }
                        }}>
                            <MenuText type="subtitle" color="light" center>
                                CATÁLOGO DE PRÊMIOS
                            </MenuText>
                        </WrapperTouch>
                        {role === 'GESTOR DA INFORMACAO' && (
                            <WrapperTouch onPress={() => {
                                close();
                                navigation.navigate(ROUTENAMES.SPREADSHEET);
                            }}>
                                <MenuText type="subtitle" color="light" center>
                                    PUBLICAR PLANILHAS
                                </MenuText>
                            </WrapperTouch>
                        )}
                        <WrapperTouch
                            onPress={() => {
                                close();
                                navigation.navigate(ROUTENAMES.ALL_NEWS);
                            }}
                        >
                            <MenuText type="subtitle" color="light" center>
                                NOVIDADES
                            </MenuText>
                        </WrapperTouch>
                        {role !== 'GESTOR DA INFORMACAO' && (
                            <WrapperTouch
                                onPress={() => {
                                    close();
                                    navigation.navigate(ROUTENAMES.ALL_CAMPAIGNS);
                                }}
                            >
                            <MenuText type="subtitle" color="light" center>
                                CAMPANHAS
                            </MenuText>
                        </WrapperTouch>
                        )}
                        {role === 'GERENTE REGIONAL' && (
                            <WrapperTouch
                                onPress={() => {
                                    close();
                                    navigation.navigate(ROUTENAMES.MY_TEAM);
                                }}
                            >
                                <MenuText type="subtitle" color="light" center>
                                    MINHA EQUIPE
                                </MenuText>
                            </WrapperTouch>
                        )}
                        {role === 'GERENTE' && (
                            <WrapperTouch
                                onPress={() => {
                                    close();
                                    navigation.navigate(ROUTENAMES.MY_TEAM);
                                }}
                            >
                                <MenuText type="subtitle" color="light" center>
                                    MINHA EQUIPE
                                </MenuText>
                            </WrapperTouch>
                        )}
                        <WrapperTouch
                            onPress={() => {
                                close();
                                navigation.navigate(ROUTENAMES.NEW_OCCURERENCE);
                            }}
                        >
                            <MenuText type="subtitle" color="light" center>
                                FALE CONOSCO
                            </MenuText>
                        </WrapperTouch>
                        <WrapperTouch onPress={() => this.handleLogout()}>
                            <MenuText type="subtitle" color="light" center>
                                SAIR
                            </MenuText>
                        </WrapperTouch>
                    </WrapperText>
                </Wrapper>
            </Modal>
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
)(MenuModal);
