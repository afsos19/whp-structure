import 'moment';
import moment from 'moment-timezone';
import 'moment/locale/pt-br';
import React from 'react';
import styled from 'styled-components/native';
import LinearGradient from 'react-native-linear-gradient';
import { connect } from 'react-redux';
import CommonText from '../common/CommonText';
import { allMonths } from '../../utils/date';
import InputOptionsLine from '../common/InputOptionsLine';
import InputBorder from '../common/InputBorder';
import { getTeamUsersDetailAction, teamReset } from '../../redux/actions/team';

const Wrapper = styled.View`
    align-self: center;
    width: 100%;
    padding-bottom: 20;
    margin-top: 20;
    background-color: ${p => p.theme.colors.background};
`;

const WrapperCards = styled.View`
    width: 100%;
    flex-direction: row;
    align-self: center;
    align-items: center;
    justify-content: space-around;
`;

const Card = styled.View`
    align-self: center;
    background-color: ${p => p.theme.colors.background};
    width: 30%;
    height: 100%;
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
    border-color: ${p => p.theme.colors.borderMyTeam};
    border-width: .5;
    padding-vertical: 10;
    padding-horizontal: 5;
`;

const TextOne = styled(CommonText)`
    color: ${p => p.theme.colors.chartColorOne};
`;

const TextTwo = styled(CommonText)`
    color: ${p => p.theme.colors.chartColorTwo};
`;

const TextThree = styled(CommonText)`
    color: ${p => p.theme.colors.chartColorThree};
`;

const WrapperInput = styled.View`
    flex-direction: row;
    align-self: flex-start;
    padding-horizontal: 20;
`;

const WrapperSearch = styled.View`
    align-self: flex-end;
    align-items: center;
    justify-content: center;
    flex-direction: row;
    padding-horizontal: 20;
`;

const List = styled.FlatList`
    align-self: center;
    width: 90%;
    /* padding-bottom: 15; */
`;

const CollumnOne = styled.View`
    flex: 2;
    align-items: center;
    padding-right: 10;
`;

const CollumnTwo = styled.View`
    flex: 1;
    align-items: center;
`;

const HeaderCard = styled(CommonText)`
    color: ${p => p.theme.colors.headerCardMyTeam};
`;

const WrapperItem = styled.View`
    flex-direction: row;
    padding-horizontal: 5;
    padding-vertical: 10;
    align-items: center;
    justify-content: space-between;
    background-color: ${p => p.theme.colors.background};
    border-top-width: 0.5;
    border-color: ${p => p.theme.colors.borderMyTeam};
`;

const WrapperHeader = styled(LinearGradient).attrs({
    colors: ['#FFFFFF', '#FAFAFA'],
})`
    flex-direction: row;
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
    padding-horizontal: 5;
    padding-vertical: 10;
    border-color: #AFAFAF;
    border-width: .5;
    border-radius: 6;
    align-items: center;
    justify-content: space-between;
    background-color: ${p => p.theme.colors.background};
    margin-bottom: 20;
    margin-top: 20;
`;

class UsersTab extends React.Component {
    state = {
        months: [],
        selectedMonth: '',
        search: '',
        data: null,
        dataList: null,
        shop: null,
        networkList: null,
    };

    componentDidMount() {
        const { data, network, role } = this.props;
        const months = allMonths();

        if (role === 'GERENTE REGIONAL') {
            const networkList = [];
            network.data.map(i => {
                networkList.push({
                    label: i.network.name,
                    value: i.id,
                });
            });
            this.setState({ networkList });
        }

        this.setState({ months, shop: network.data[0].id });
        if (data) {
            this.setState({ data, dataList: data.userList });
        }
    }

    componentDidUpdate(nextProps) {
        const { team, data, reset } = this.props;
        if (data && data !== nextProps.data) {
            this.setState({ data, dataList: data.trainingList });
        }
        if (team.successUser && team.successUser !== nextProps.team.successUser) {
            this.setState({
                data: team.dataUsersDetails,
                dataList: team.dataUsersDetails.userList,
            });
        }
        if (team.errorUser && team.errorUser !== nextProps.team.errorUser) {
            this.setState({
                data: null,
                dataList: null,
            });
            reset();
        }
    }

    renderItem = item => {
        return (
            <WrapperItem>
                <CollumnOne>
                    <CommonText type="normal" color="dark" numberOfLines={1}>
                        {item.name}
                    </CommonText>
                </CollumnOne>
                <CollumnTwo>
                    <CommonText type="normal" color="dark" numberOfLines={1}>
                        {item.userStatus.description}
                    </CommonText>
                </CollumnTwo>
            </WrapperItem>
        );
    };

    renderHeader = () => {
        return (
            <WrapperHeader>
                <CollumnOne>
                    <HeaderCard type="normal" color="dark">
                        Nome
                    </HeaderCard>
                </CollumnOne>
                <CollumnTwo>
                    <HeaderCard type="normal" color="dark">
                        Status
                    </HeaderCard>
                </CollumnTwo>
            </WrapperHeader>
        );
    };

    handleSearch = value => {
        const { data } = this.props;
        const filtered = data.userList.filter(d => {
            const name = d.name.toLowerCase();
            return name.indexOf(value.toLowerCase()) !== -1;
        });
        // newData.userList = filtered;
        this.setState({ search: value, dataList: filtered });
    };

    handleMonth = month => {
        const { getTeamUsersDetail } = this.props;
        const { shop } = this.state;

        this.setState({ selectedMonth: month, search: '' });
        const year = moment().year();

        const data = {
            currentMonth: month,
            currentYear: year,
            shop,
        };

        getTeamUsersDetail(data);
    };

    handleShop = shop => {
        const { getTeamUsersDetail } = this.props;
        const { month } = this.state;

        this.setState({ shop, search: '' });
        const year = moment().year();

        const data = {
            currentMonth: month,
            currentYear: year,
            shop,
        };
        getTeamUsersDetail(data);
    };

    render() {
        const { months, selectedMonth, search, data, dataList, shop, networkList } = this.state;
        const { role } = this.props;

        return (
            <Wrapper>
                <WrapperCards>
                    <Card>
                        <TextOne type="h2">{data && data.activated}%</TextOne>
                        <TextOne type="small">Ativo</TextOne>
                    </Card>
                    <Card>
                        <TextTwo type="h2">{data && data.inactivated}%</TextTwo>
                        <TextTwo type="small">Pré-cadastrados</TextTwo>
                    </Card>
                    <Card>
                        <TextThree type="h2">{data && data.preRegistered}%</TextThree>
                        <TextThree type="small">Inativo</TextThree>
                    </Card>
                </WrapperCards>
                <WrapperInput>
                    <InputOptionsLine
                        width="50%"
                        onChange={v => this.handleMonth(v)}
                        value={selectedMonth}
                        items={months}
                    />
                    {role === 'GERENTE REGIONAL' && networkList && (
                        <InputOptionsLine
                            width="50%"
                            onChange={v => this.handleShop(v)}
                            value={shop}
                            items={networkList}
                        />
                    )}
                </WrapperInput>
                <WrapperSearch>
                    <CommonText type="normal" color="dark" center>
                        PESQUISAR{'  '}
                    </CommonText>
                    <InputBorder
                        marginTop={-10}
                        width="65%"
                        onChange={v => this.handleSearch(v)}
                        value={search}
                        placeholder=""
                    />
                </WrapperSearch>
                {dataList && dataList.length > 0 ? (
                    <List
                        data={dataList}
                        ListHeaderComponent={() => this.renderHeader()}
                        renderItem={({ item }) => this.renderItem(item)}
                    />
                ) : (
                    <CommonText type="normal" color="dark" center>
                        Sem dados
                    </CommonText>
                )}
            </Wrapper>
        );
    }
}
// Redux
const mapStateToProps = state => ({
    team: state.team,
    network: state.network,
});

const mapDispatchToProps = dispatch => ({
    getTeamUsersDetail: data => dispatch(getTeamUsersDetailAction(data)),
    reset: () => dispatch(teamReset()),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(UsersTab);
