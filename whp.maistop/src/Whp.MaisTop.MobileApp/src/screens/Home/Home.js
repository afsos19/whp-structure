import * as React from 'react';
import styled from 'styled-components/native';
import { AsyncStorage, Dimensions, Platform } from 'react-native';
import { connect } from 'react-redux';
import jwtDecode from 'jwt-decode';
import Carousel from 'react-native-snap-carousel';
// import ROUTENAMES from '../../navigation/routeName';
import { getShopAction, authReset } from '../../redux/actions/auth';
import { getUserAction, updateUserPhoneAction } from '../../redux/actions/user';
import { getNetworkAction } from '../../redux/actions/network';
import { getPonctuationAction } from '../../redux/actions/ponctuation';
import { getNewsAction } from '../../redux/actions/news';
import CommonText from '../../components/common/CommonText';
import InputLine from '../../components/common/InputLine';
import GradientBackground from '../../components/common/GradientBackground';
import Card from '../../components/common/Card';
import Logo from '../../components/common/Logo';
import CircleImage from '../../components/common/CircleImage';
import { imgUserPath, imgNetworkPath } from '../../utils/urls';
import Button from '../../components/common/Button';
import Tab from '../../components/common/Tab';
import NewsCard from '../../components/News/NewsCard';
import MenuModal from '../../components/Home/MenuModal';
import images from '../../res/images';
import ROUTENAMES from '../../navigation/routeName';
import openBrowser from '../../utils/OpenLink';
import SpreadsheetButton from '../../components/Home/SpreadsheetButton';
import { mgmCodeAction } from '../../redux/actions/mgm';
import UpdateRegister from '../../components/Modals/UpdateRegister';

const sliderWidth = Dimensions.get('window').width;

const Container = styled(GradientBackground)`
    flex: 1;
    align-items: center;
`;

const Scroll = styled.ScrollView.attrs({
    showsVerticalScrollIndicator: false
})`
    width: 100%;
`;

const WrapperRow = styled.View`
    flex-direction: row;
    align-self: center;
    justify-content: center;
    width: 90%;
    margin-top: 30;
`;

const WrapperNotifications = styled.View`
    align-self: center;
    width: 90%;
`;

const WrapperTextRow = styled.View`
    flex-direction: row;
    align-self: center;
    justify-content: center;
`;

const WrapperCollum = styled.View`
    width: 50%;
    flex-direction: column;
    align-self: center;
    /* justify-content: center; */
    align-items: center;
`;

const UnderlineText = styled(CommonText)`
    text-decoration: underline;
    text-decoration-color: ${p => p.theme.colors.textDark};
`;

const WrapperNetwork = styled.View`
    align-self: center;
    align-items: flex-start;
    justify-content: flex-start;
    width: 90%;
`;

const NetworkImage = styled.Image`
    height: 30;
    width: 100;
`;

const TouchProfile = styled.TouchableOpacity`
    align-self: flex-start;
`;

const BackPoints = styled.View`
    align-self: flex-end;
    justify-content: center;
    align-items: center;
    background-color: ${p => p.theme.colors.tertiaryBackground};
    padding: 5px;
    border-radius: 5;
`;

const Line = styled.View`
    align-self: center;
    width: 90%;
    height: 1;
    margin-top: 40;
    background-color: ${p => p.theme.colors.secondaryBackground};
`;

const InviteWrapper = styled.TouchableOpacity`
    flex-direction: row;
    align-self: center;
    align-items: center;
    justify-content: space-around;
    width: 133;
    border-color: ${p => p.theme.colors.border};
    border-width: .5;
    margin-top: 10;
    background-color: ${p => p.theme.colors.background};
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
    border-radius: 6;
    padding-horizontal: 10;
    padding-vertical: 10;
`;

const InviteImage = styled.Image`
    align-self: center;
    width: 30;
    height: 30;
`;

const InviteTextWrapper = styled.View`
    align-self: center;
    width: 60%;
`;

class Home extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        menuVisible: false,
        decoded: null,
        editPhone: false,
        role: null,
        myPhone: '',
        editVisible: false,
    };

    componentDidMount() {
        const { user } = this.props;

        if (user.data) {
            this.setState({ myPhone: user.data.cellPhone });
        }

        AsyncStorage.getItem("editAdress").then(res => {
            console.log("res editAdress> ", res)
            if (!res) {
                this.setState({ editVisible: true })
            }
        }).catch(error => {
            console.log("error editAdress> ", error)
        })
    }

    handleEditPhone = newPhone => {
        const { user, updateUserPhone } = this.props;
        const formdata = new FormData();

        formdata.append('Name', user.data.name);
        formdata.append('Id', user.data.id);
        formdata.append('CPF', user.data.cpf);
        formdata.append('Photo', user.data.photo);
        // start FornOne
        formdata.append('file', null);
        formdata.append('Email', user.data.email);
        formdata.append('CellPhone', newPhone);
        formdata.append('Phone', user.data.phone);
        formdata.append('BithDate', user.data.bithDate);
        formdata.append('Gender', user.data.gender);
        formdata.append('HeartTeam', user.data.heartTeam);
        formdata.append('SonAmout', user.data.sonAmout);
        formdata.append('CivilStatus', user.data.civilStatus);
        // FormTwo
        formdata.append('Address', user.data.address);
        formdata.append('CEP', user.data.cep);
        formdata.append('Number', user.data.number);
        formdata.append('Complement', user.data.complement);
        formdata.append('Neighborhood', user.data.neighborhood);
        formdata.append('City', user.data.city);
        formdata.append('Uf', user.data.uf);
        formdata.append('PrivacyPolicy', true);
        formdata.append('ReferencePoint', user.data.referencePoint);

        updateUserPhone(formdata);
    };

    render() {
        const { menuVisible, role, editPhone, myPhone , editVisible} = this.state;
        const { user, network, ponctuation, news, navigation, getShop, auth, mgm } = this.props;

        return (
            <Container>
                <MenuModal
                    navigation={navigation}
                    visible={menuVisible}
                    close={() => this.setState({ menuVisible: false })}
                />
                <UpdateRegister
                    navigation={navigation}
                    visible={editVisible}
                    close={() => {
                        this.setState({ editVisible: false })
                        AsyncStorage.setItem("editAdress", "edit")
                    }}
                />

                <Scroll ref="scrollView">
                    <Logo height="30px" img={images.logoClean} marginTop={20}/>
                    <Card marginBottom={40} width="90%" marginTop={20}>
                        {user.data && network.data && (
                            <React.Fragment>
                                <WrapperNetwork>
                                    <NetworkImage
                                        source={{
                                            uri: imgNetworkPath(network.data[0].network.id),
                                        }}
                                        resizeMode="contain"
                                    />
                                </WrapperNetwork>
                                <WrapperRow>
                                    <WrapperCollum>
                                        <CircleImage image={imgUserPath + user.data.photo} size={110} />
                                    </WrapperCollum>
                                    <WrapperCollum>
                                        <CommonText type="normal" bold color="primary">
                                            {user.data.name}
                                        </CommonText>
                                        {!editPhone ? (
                                            <CommonText type="normal" color="dark" bold marginTop={10}>
                                                {myPhone.replace(/\D/g, '').replace(/(\d{2})(\d)(\d{4})(\d{4})$/, '($1) $2$3-$4')}
                                            </CommonText>
                                        ) : (
                                            <InputLine
                                                mask="cel-phone"
                                                value={myPhone}
                                                placeholder="Celular:"
                                                // center
                                                onChange={v => this.setState({ myPhone: v })}
                                            />
                                        )}

                                        <TouchProfile
                                            onPress={() => {
                                                if (editPhone) {
                                                    this.handleEditPhone(myPhone);
                                                }
                                                this.setState({ editPhone: !editPhone });
                                            }}
                                        >
                                            <UnderlineText type="normal" color="dark" marginTop={5}>
                                                {!editPhone ? 'ALTERAR' : 'SALVAR'}
                                            </UnderlineText>
                                        </TouchProfile>
                                        <TouchProfile onPress={() => navigation.navigate(ROUTENAMES.PROFILE)}>
                                            <CommonText type="normal" color="dark" marginTop={5} bold>
                                                MEU PERFIL
                                            </CommonText>
                                        </TouchProfile>
                                    </WrapperCollum>
                                </WrapperRow>
                                <WrapperRow>
                                    <WrapperCollum>
                                        <CommonText type="normal" color="dark" bold center>
                                            MEU SALDO
                                        </CommonText>
                                        <WrapperTextRow>
                                            <CommonText type="h1" color="primary" marginTop={5}>
                                                {ponctuation.extract ? ponctuation.extract.balance.toFixed(0) : 0}
                                            </CommonText>

                                            <BackPoints>
                                                <CommonText type="small" color="light" bold>
                                                    PTS
                                                </CommonText>
                                            </BackPoints>
                                        </WrapperTextRow>
                                    </WrapperCollum>
                                    <WrapperCollum>
                                        <Button
                                            text="Extrato"
                                            color="light"
                                            onPress={() => navigation.navigate(ROUTENAMES.EXTRACT)}
                                        />
                                        <Button
                                            text="Resgate já"
                                            color="primary"
                                            marginTop={5}
                                            isLoading={auth.isLoadingShop}
                                            onPress={() => {
                                                if (auth.shopUrl) {
                                                    openBrowser(auth.shopUrl)
                                                }
                                            }}
                                        />
                                        <InviteWrapper onPress={() => navigation.navigate(ROUTENAMES.INVITE)}>
                                            <InviteImage source={images.inviteIcon} resizeMode="contain" />
                                            <InviteTextWrapper>
                                                <CommonText type="normal" color="dark" bold>
                                                    CONVIDE AMIGOS
                                                </CommonText>
                                            </InviteTextWrapper>
                                        </InviteWrapper>
                                    </WrapperCollum>
                                </WrapperRow>

                                <Line />

                                {role === 'GESTOR DA INFORMACAO' && (
                                    <React.Fragment>
                                        <SpreadsheetButton
                                            onPress={() => navigation.navigate(ROUTENAMES.SPREADSHEET)}
                                        />
                                        <Line />
                                    </React.Fragment>
                                )}

                                <WrapperNotifications>
                                    <CommonText type="h5" color="primary" marginTop={40}>
                                        Últimas novidades
                                    </CommonText>
                                    <CommonText type="normal" color="dark" marginTop={5}>
                                        Acompanhe as principais novidades do +TOP
                                    </CommonText>
                                </WrapperNotifications>
                                {news.data ? (
                                    <Carousel
                                        containerCustomStyle={{
                                            marginTop: 20,
                                            alignSelf: 'center',
                                        }}
                                        contentContainerCustomStyle={{ paddingVertical: 20 }}
                                        data={news.data}
                                        renderItem={({ item, index }) => (
                                            <NewsCard item={item} index={index} navigation={navigation} />
                                        )}
                                        sliderWidth={sliderWidth * 0.9}
                                        itemWidth={200}
                                    />
                                ) : (
                                    <WrapperNotifications>
                                        <CommonText>Ops, não tem nenhuma novidade</CommonText>
                                    </WrapperNotifications>
                                )}
                                <Button
                                    text="Ver todas as novidades"
                                    type="full"
                                    marginTop={30}
                                    onPress={() => navigation.navigate(ROUTENAMES.ALL_NEWS)}
                                />
                            </React.Fragment>
                        )}
                    </Card>
                </Scroll>
                <Tab openMenu={() => this.setState({ menuVisible: true })} navigation={navigation} />
            </Container>
        );
    }
}

// Redux
const mapStateToProps = state => ({
    user: state.user,
    network: state.network,
    ponctuation: state.ponctuation,
    news: state.news,
    auth: state.auth,
    mgm: state.mgm,
});

const mapDispatchToProps = dispatch => ({
    getUser: id => dispatch(getUserAction(id)),
    updateUserPhone: data => dispatch(updateUserPhoneAction(data)),
    getNetwork: () => dispatch(getNetworkAction()),
    getPunctuation: () => dispatch(getPonctuationAction()),
    getNews: () => dispatch(getNewsAction()),
    getShop: () => dispatch(getShopAction()),
    getMgmCode: () => dispatch(mgmCodeAction()),
    resetAuth: () => dispatch(authReset()),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Home);
