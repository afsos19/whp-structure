import 'moment';
import moment from 'moment-timezone';
import 'moment/locale/pt-br';
import * as React from 'react';
import styled from 'styled-components/native';
import { AsyncStorage } from 'react-native';
import jwtDecode from 'jwt-decode';
import { connect } from 'react-redux';
import { SafeAreaView } from 'react-navigation';
import { getTeamUsersAction, getTeamSalesAction, getTeamTrainingAction } from '../../redux/actions/team';
import GradientBackground from '../../components/common/GradientBackground';
import CommonText from '../../components/common/CommonText';
import Tab from '../../components/common/Tab';
import Header from '../../components/common/Header';
import MenuModal from '../../components/Home/MenuModal';
import TeamTab from '../../components/MyTeam/TeamTab';
import Overview from '../../components/MyTeam/Overview';
import UsersTab from '../../components/MyTeam/UsersTab';
import TrainingTab from '../../components/MyTeam/TrainingTab';
import SalesTab from '../../components/MyTeam/SalesTab';

const Container = styled(GradientBackground)`
    flex: 1;
`;

const WrapperText = styled.View`
    align-self: center;
    width: 90%;
`;

const Scroll = styled.ScrollView.attrs({
    showsVerticalScrollIndicator: false
})`
    width: 100%;
`;

const WrapperContent = styled.View`
    align-self: center;
    width: 100%;
    margin-top: 20;
    background-color: ${p => p.theme.colors.background};
`;

class MyTeam extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        menuVisible: false,
        users: null,
        sales: null,
        training: null,
        activeTab: 1,
        role: null,
    };

    componentDidMount() {
        const { network, getTeamUsers, getTeamSales, getTeamTraining } = this.props;
        const month = moment().month();
        const year = moment().year();

        const data = {
            currentMonth: month,
            currentYear: year,
            // shop: 0,
            shop: network.data[0].id,
        };

        getTeamUsers(data);
        getTeamSales(data);
        getTeamTraining(data);

        AsyncStorage.getItem('token').then(value => {
            const decoded = jwtDecode(value);
            const role = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
            this.setState({ role });
        });
    }

    componentDidUpdate(nextProps) {
        const { team } = this.props;
        if (team.dataUsers && team.dataUsers !== nextProps.team.dataUsers) {

            const users = [];
            if (team.dataUsers.activated > 0) {
                users.push({
                    y: team.dataUsers.activated,
                    label: `${team.dataUsers.activated}%`,
                    type: 'activated',
                    title: 'Ativos',
                });
            }
            if (team.dataUsers.inactivated > 0) {
                users.push({
                    y: team.dataUsers.inactivated,
                    label: `${team.dataUsers.inactivated}%`,
                    type: 'inactivated',
                    title: 'Inativos',
                });
            }
            if (team.dataUsers.preRegistered > 0) {
                users.push({
                    y: team.dataUsers.preRegistered,
                    label: `${team.dataUsers.preRegistered}%`,
                    type: 'preRegistered',
                    title: 'Pré-Cadastrados',
                });
            }
            this.setState({ users });
        }

        if (team.dataTraining && team.dataTraining !== nextProps.team.dataTraining) {
            const training = [];
            if (team.dataTraining.trainingNotDone > 0) {
                training.push({
                    y: team.dataTraining.trainingNotDone,
                    label: `${team.dataTraining.trainingNotDone}%`,
                    type: 'trainingNotDone',
                    title: 'Não realizaram nenhum trenamento',
                });
            }
            if (team.dataTraining.trainingOneDone > 0) {
                training.push({
                    y: team.dataTraining.trainingOneDone,
                    label: `${team.dataTraining.trainingOneDone}%`,
                    type: 'trainingOneDone',
                    title: 'Realizaram 1 de 2 treinamentos',
                });
            }
            if (team.dataTraining.trainingTwoDone > 0) {
                training.push({
                    y: team.dataTraining.trainingTwoDone,
                    label: `${team.dataTraining.trainingTwoDone}%`,
                    type: 'trainingTwoDone',
                    title: 'Realizaram todos trenamentos',
                });
            }

            this.setState({ training });
        }

        if (team.dataSales && team.dataSales !== nextProps.team.dataSales) {
            const sales = [];
            sales.push({
                y: team.dataSales.superTop,
                label: team.dataSales.superTop,
                type: 'superTop',
                title: 'Supertops',
            });
            sales.push({
                y: team.dataSales.participant,
                label: team.dataSales.participant,
                type: 'participant',
                title: 'Produtos Participantes',
            });

            this.setState({ sales });
        }
    }

    render() {
        const { menuVisible, users, sales, training, activeTab, role } = this.state;
        const { navigation, team } = this.props;

        return (
            <Container isLoading={team.isLoading}>
                <SafeAreaView />
                <MenuModal
                    navigation={navigation}
                    visible={menuVisible}
                    close={() => this.setState({ menuVisible: false })}
                />
                <Header navigation={navigation} />
                <Scroll>
                    <WrapperText>
                        <CommonText type="h3" color="light" center>
                            Minha equipe
                        </CommonText>
                    </WrapperText>
                    <WrapperContent>
                        <TeamTab activeItem={activeTab} changeTab={tab => this.setState({ activeTab: tab })} />
                        {activeTab === 1 && <Overview users={users} sales={sales} training={training} />}
                        {activeTab === 2 && <UsersTab role={role} data={team.dataUsersDetails} />}
                        {activeTab === 3 && <TrainingTab role={role} data={team.dataTrainingDetails} />}
                        {activeTab === 4 && <SalesTab role={role} data={team.dataSalesDetail} />}
                    </WrapperContent>
                </Scroll>
                <Tab openMenu={() => this.setState({ menuVisible: true })} navigation={navigation} />
            </Container>
        );
    }
}

// Redux
const mapStateToProps = state => ({
    team: state.team,
    network: state.network,
});

const mapDispatchToProps = dispatch => ({
    getTeamUsers: data => dispatch(getTeamUsersAction(data)),
    getTeamSales: data => dispatch(getTeamSalesAction(data)),
    getTeamTraining: data => dispatch(getTeamTrainingAction(data)),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(MyTeam);
