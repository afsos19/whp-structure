import * as React from 'react';
import styled from 'styled-components/native';
import { connect } from 'react-redux';
import { SafeAreaView } from 'react-navigation';
import { invitedUsersAction, mgmReset } from '../../redux/actions/mgm';
import GradientBackground from '../../components/common/GradientBackground';
import CommonText from '../../components/common/CommonText';
import Tab from '../../components/common/Tab';
import Header from '../../components/common/Header';
import MenuModal from '../../components/Home/MenuModal';
import Card from '../../components/common/Card';
import images from '../../res/images';
import { TouchableOpacity } from 'react-native';
import IniviteDetails from '../../components/Invite/IniviteDetails';

const Container = styled(GradientBackground)`
    flex: 1;
`;

const Scroll = styled.ScrollView.attrs({
    showsVerticalScrollIndicator: false
})`
    width: 100%;
`;

const List = styled.FlatList`
    align-self: center;
    margin-top: 15;
    width: 90%;
    background-color: ${p => p.theme.colors.background};
    padding-bottom: 15;
`;

const UnderlineText = styled(CommonText)`
    text-decoration-style: solid;
    text-decoration-line: underline;
`;

const TextBowWrapper = styled.View`
    align-self: center;
    width: 80%;
`;

const CollumnOne = styled.View`
    flex: 1;
    align-items: center;
`;

const CollumnTwo = styled.View`
    flex: 2;
    align-items: center;
`;

const CollumnThree = styled.View`
    flex: 1.5;
    align-items: center;
`;

const WrapperItem = styled.View`
    flex-direction: row;
    padding-horizontal: 5;
    padding-top: 20;
    padding-bottom: 5;
    align-items: center;
    justify-content: space-between;
    background-color: ${p => p.theme.colors.background};
    border-bottom-width: 0.5;
    border-color: ${p => p.theme.colors.tertiary};
`;

const WrapperHeader = styled.View`
    flex-direction: row;
    padding-horizontal: 5;
    padding-vertical: 20;
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
    align-items: center;
    justify-content: space-between;
    background-color: ${p => p.theme.colors.background};
    border-width: .5;
    border-color: ${p => p.theme.colors.borderLight};
    border-radius: 6;
`;

const ImageIcon = styled.Image`
    align-self: center;
    width: 20;
    height: 20;
`;

class MyInvites extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        menuVisible: false,
        detailVisible: false,
        selectedItem: null,
        phone: '',
    };

    componentDidMount() {
        const { getInvitedUsers } = this.props;
        getInvitedUsers();
    }

    renderItem = item => {
        const { navigation } = this.props;
        return (
            <WrapperItem>
                <CollumnOne>
                <CommonText type="small" color="dark" bold center>
                        {item.result.name ? item.result.name : ""}
                    </CommonText>
                    <CommonText type="small" color={item.result.gotPunctuation ? "secondary" : "dark"} center>
                        {item.result.isCanceled ? "Cancelado" : item.result.gotPunctuation ? "Cadastrado" : "Pendente"}
                    </CommonText>
                </CollumnOne>
                <CollumnTwo>
                    <ImageIcon source={item.result.isCanceled ? images.inviteIcon3 : item.result.gotPunctuation ? images.inviteIcon1 : images.inviteIcon2 } resizeMode="contain" />
                </CollumnTwo>
                <CollumnThree>
                    <TouchableOpacity onPress={() => this.setState({ selectedItem: item, detailVisible: true })}>
                        <UnderlineText type="small" color="dark" center bold>
                            + Informações
                        </UnderlineText>
                    </TouchableOpacity>
                </CollumnThree>
            </WrapperItem>
        );
    };

    renderHeader = () => {
        return (
            <WrapperHeader>
                <CollumnOne>
                    <CommonText type="small" color="dark" bold center>
                        Nome
                    </CommonText>
                </CollumnOne>
                <CollumnTwo>
                    <CommonText type="small" color="dark" bold center>
                    Credito de pontos
                    </CommonText>
                </CollumnTwo>
                <CollumnThree>
                    <CommonText type="small" color="dark" bold center />
                </CollumnThree>
            </WrapperHeader>
        );
    };

    render() {
        const { menuVisible, detailVisible, selectedItem } = this.state;
        const { navigation, mgm } = this.props;
        
        return (
            <Container isLoading={mgm.isLoading}>
                {/* <SafeAreaView /> */}
                <MenuModal
                    navigation={navigation}
                    visible={menuVisible}
                    close={() => this.setState({ menuVisible: false })}
                />
                <IniviteDetails
                    visible={detailVisible}
                    item={selectedItem}
                    close={() => this.setState({ detailVisible: false })}
                />
                <Header navigation={navigation} />
                <Scroll>
                    <Card marginBottom={40} width="90%" marginTop={10} noPadding>
                        { mgm.dataUsers ?
                            <React.Fragment>
                                <TextBowWrapper>
                                    <CommonText type="h3" color="primary" center>
                                    Progresso dos amigos indicados
                                    </CommonText>
                                    <CommonText type="normal" color="dark" marginTop={5} center>
                                        Abaixo você poderá acompanhar como anda o progresso de cada amigo indicado. Caso tenha dúvidas, entre em contato através do FALE CONOSCO.
                                    </CommonText>
                                </TextBowWrapper>
                                <List
                                    data={mgm.dataUsers}
                                    ListHeaderComponent={() => this.renderHeader()}
                                    renderItem={({ item }) => this.renderItem(item)}
                                />
                            </React.Fragment>
                            : (
                                <TextBowWrapper>
                                    <CommonText type="h3" color="primary" center>
                                        Nenhum convite listado
                                    </CommonText>
                                    <CommonText type="normal" color="dark" marginTop={5} center>
                                        Nenhum dos convites enviados foi cadastrado ainda.
                                    </CommonText>
                                </TextBowWrapper>
                            )
                        }
                    </Card>
                </Scroll>
                <Tab
                    openMenu={() => this.setState({ menuVisible: true })}
                    navigation={navigation}
                />
            </Container>
        );
    }
}

// Redux
const mapStateToProps = state => ({
    mgm: state.mgm,
});

const mapDispatchToProps = dispatch => ({
    getInvitedUsers: () => dispatch(invitedUsersAction()),
    reset: () => dispatch(mgmReset()),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(MyInvites);
