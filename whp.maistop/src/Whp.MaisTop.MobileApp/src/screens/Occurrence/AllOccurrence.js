import 'moment';
import moment from 'moment-timezone';
import 'moment/locale/pt-br';
import * as React from 'react';
import styled from 'styled-components/native';
import { connect } from 'react-redux';
import { SafeAreaView } from 'react-navigation';
import { getOccurrenceAction } from '../../redux/actions/occurrence';
import GradientBackground from '../../components/common/GradientBackground';
import CommonText from '../../components/common/CommonText';
import Tab from '../../components/common/Tab';
import Header from '../../components/common/Header';
import MenuModal from '../../components/Home/MenuModal';
import Card from '../../components/common/Card';
import FaqCall from '../../components/Faq/FaqCall';
import ROUTENAMES from '../../navigation/routeName';

const Container = styled(GradientBackground)`
    flex: 1;
`;

const Scroll = styled.ScrollView.attrs({
    showsVerticalScrollIndicator: false
})`
    width: 100%;
`;

const WrapperTabCard = styled.View`
    margin-top: 15;
    flex-direction: row;
    align-self: center;
    background-color: ${p => p.theme.colors.tertiary};
    height: 55;
`;

const TabCard = styled.TouchableOpacity`
    flex: 1;
    align-self: center;
    align-items: center;
    justify-content: center;
    padding-horizontal: 30;
    background-color: ${p => (p.active ? p.theme.colors.tertiary : p.theme.colors.tabNoActive)};
    height: 55;
`;

const List = styled.FlatList`
    width: 100%;
    background-color: ${p => p.theme.colors.tertiary};
    padding-bottom: 15;
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

const WrapperItem = styled.TouchableOpacity`
    flex-direction: row;
    padding-horizontal: 5;
    padding-vertical: 20;
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
    align-items: center;
    justify-content: space-between;
    background-color: ${p => p.theme.colors.tertiary};
`;

class AllOccurrence extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        menuVisible: false,
        tabActive: 1,
    };

    componentDidMount() {
        const { getOccurrence } = this.props;
        getOccurrence();
    }

    renderItem = item => {
        const { navigation } = this.props;

        return (
            <WrapperItem
                onPress={() => navigation.navigate(ROUTENAMES.OCCURERENCE_DETAIL, { item: item })}
            >
                <CollumnOne>
                    <CommonText type="small" color="dark" bold center>
                        {moment(item.createdAt).format('L')}
                    </CommonText>
                </CollumnOne>
                <CollumnTwo>
                    <CommonText type="small" color="dark" center>
                        {item.code}
                    </CommonText>
                </CollumnTwo>
                <CollumnThree>
                    <CommonText type="small" color="dark" center>
                        {item.occurrenceSubject.description}
                    </CommonText>
                </CollumnThree>
            </WrapperItem>
        );
    };

    renderHeader = () => {
        return (
            <WrapperHeader>
                <CollumnOne>
                    <CommonText type="small" color="light" bold center>
                        Abertura
                    </CommonText>
                </CollumnOne>
                <CollumnTwo>
                    <CommonText type="small" color="light" bold center>
                        Chamado
                    </CommonText>
                </CollumnTwo>
                <CollumnThree>
                    <CommonText type="small" color="light" bold center>
                        Assunto
                    </CommonText>
                </CollumnThree>
            </WrapperHeader>
        );
    };

    render() {
        const { menuVisible, tabActive } = this.state;
        const { occurrence, navigation } = this.props;

        return (
            <Container isLoading={occurrence.isLoading}>
                <SafeAreaView />
                <MenuModal
                    navigation={navigation}
                    visible={menuVisible}
                    close={() => this.setState({ menuVisible: false })}
                />
                <Header navigation={navigation} />
                <Scroll>
                    <Card marginBottom={40} width="90%" marginTop={10} noPadding>
                        <CommonText type="h4" color="secondary" center>
                            Histórico de chamados
                        </CommonText>

                        <WrapperTabCard>
                            <TabCard
                                active={tabActive === 1}
                                onPress={() => this.setState({ tabActive: 1 })}
                            >
                                <CommonText type="small" color={tabActive === 1 ? "primary" : "light"} center bold>
                                    CHAMADAS EM ANDAMENTO
                                </CommonText>
                            </TabCard>
                            <TabCard
                                active={tabActive === 2}
                                onPress={() => this.setState({ tabActive: 2 })}
                            >
                                <CommonText type="small" color={tabActive === 2 ? "primary" : "light"} center bold>
                                    CHAMADAS FINALIZADAS
                                </CommonText>
                            </TabCard>
                        </WrapperTabCard>
                        <List
                            data={tabActive === 1 ? occurrence.open : occurrence.close}
                            ListHeaderComponent={() => this.renderHeader()}
                            renderItem={({ item }) => this.renderItem(item)}
                        />
                        <FaqCall navigation={navigation} noMargin />
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
    occurrence: state.occurrence,
});

const mapDispatchToProps = dispatch => ({
    getOccurrence: () => dispatch(getOccurrenceAction()),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(AllOccurrence);
