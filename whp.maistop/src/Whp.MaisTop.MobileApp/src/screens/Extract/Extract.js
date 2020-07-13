import * as React from 'react';
import styled from 'styled-components/native';
import { Dimensions, TouchableOpacity } from 'react-native';
import { connect } from 'react-redux';
import { SafeAreaView } from 'react-navigation';
import { Formik } from 'formik';
import * as yup from 'yup';
import { getCreditsAction } from '../../redux/actions/ponctuation';
import { getSalesAction } from '../../redux/actions/sales';
import { getOrdersAction } from '../../redux/actions/orders';
import CommonText from '../../components/common/CommonText';
import Card from '../../components/common/Card';
import Tab from '../../components/common/Tab';
import Header from '../../components/common/Header';
import MenuModal from '../../components/Home/MenuModal';
import GradientBackground from '../../components/common/GradientBackground';
import Button from '../../components/common/Button';
import { allYears, allMonths } from '../../utils/date';
import InputOptionsLine from '../../components/common/InputOptionsLine';
import ExtractDetails from '../../components/Extract/ExtractDetails';
import CreditsDetail from '../../components/Extract/CreditsDetail';
import OrdersDetail from '../../components/Extract/OrdersDetail';
import SalesDetail from '../../components/Extract/SalesDetail';

const width = Dimensions.get('window').width;

const Container = styled(GradientBackground)`
    flex: 1;
    align-items: center;
`;

const Scroll = styled.ScrollView.attrs({
    showsVerticalScrollIndicator: false
})`
    width: 100%;
`;

const WrapperTextLeft = styled.View`
    align-self: flex-end;
`;

const WrapperPonctuation = styled.View`
    margin-top: 20;
    align-self: center;
    align-items: center;
    justify-content: center;
    width: ${width * 0.95};
    background-color: ${p => p.theme.colors.productsBackground};
    padding-left: 20;
    height: 130;
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
`;

const WrapperLine = styled.View`
    flex-direction: row;
    align-self: center;
    justify-content: center;
`;

const WrapperPoints = styled.View`
    align-self: flex-end;
    justify-content: center;
    align-items: center;
    background-color: ${p => p.theme.colors.background};
    padding: 5px;
    border-radius: 5;
`;

const WrapperCollumn = styled.View`
    margin-top: 20;
    flex-direction: row;
    align-self: center;
    /* justify-content: center; */
    /* align-items: center; */
`;

const Collumn = styled.View`
    flex: 1;
    /* align-self: center; */
    justify-content: flex-start;
    align-items: center;
`;

const WrapperExtract = styled.View`
    align-self: center;
    margin-top: 30;
`;

const validationSchema = yup.object().shape({
    month: yup.string().required('Mês é obrigatório'),
    year: yup.string().required('Ano é obrigatório'),
});

const UnderlineText = styled(CommonText)`
    text-decoration: underline;
    text-decoration-color: ${p => p.theme.colors.secondary};
`;

class Extract extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        menuVisible: false,
        months: [],
        years: [],
        type: 1,
    };

    componentDidMount() {
        const months = allMonths();
        const years = allYears();

        this.setState({ months, years });
    }

    handleSearch = values => {
        const { getCredits, getSales, getOrders } = this.props;
        const { type } = this.state;

        if (type === 2) {
            getCredits({
                currentMonth: values.month,
                currentYear: values.year,
            });
        }

        if (type === 3) {
            getSales({
                currentMonth: values.month,
                currentYear: values.year,
            });
        }

        if (type === 4) {
            getOrders({
                currentMonth: values.month,
                currentYear: values.year,
            });
        }
    };

    render() {
        const { menuVisible, months, years, type } = this.state;
        const { navigation, ponctuation, orders, sales } = this.props;
        return (
            <Container>
                <SafeAreaView />
                <MenuModal
                    navigation={navigation}
                    visible={menuVisible}
                    close={() => this.setState({ menuVisible: false })}
                />
                <Header navigation={navigation} />
                <Scroll>
                    <Card marginBottom={40} width="90%" marginTop={10}>
                        <CommonText type="h4" color="secondary" center>
                            Extrato de pontos
                        </CommonText>
                        <WrapperPonctuation>
                            <CommonText type="normal" color="light" center>
                                TOTAL DE PONTOS
                            </CommonText>
                            <WrapperLine>
                                <CommonText type="h2" color="light" marginTop={10} center>
                                    {ponctuation.extract
                                        ? ponctuation.extract.balance.toFixed(2).replace('.', ',')
                                        : 0}
                                </CommonText>
                                <WrapperPoints>
                                    <CommonText type="noraml" color="primary" center>
                                        PTS
                                    </CommonText>
                                </WrapperPoints>
                            </WrapperLine>
                        </WrapperPonctuation>
                        <WrapperCollumn>
                            <Collumn>
                                <Button
                                    type="full"
                                    text="extrato"
                                    color={type === 1 ? 'dark' : 'light'}
                                    marginTop={20}
                                    onPress={() => this.setState({ type: 1 })}
                                />
                                <Button
                                    type="full"
                                    text="créditos"
                                    color={type === 2 ? 'primary' : 'light'}
                                    marginTop={5}
                                    onPress={() => this.setState({ type: 2 })}
                                />
                                <Button
                                    type="full"
                                    text="VENDAS REALIZADAS"
                                    color={type === 3 ? 'primary' : 'light'}
                                    marginTop={5}
                                    onPress={() => this.setState({ type: 3 })}
                                />
                                <Button
                                    type="full"
                                    text="HISTÓRICO DE RESGATES"
                                    color={type === 4 ? 'primary' : 'light'}
                                    marginTop={5}
                                    onPress={() => this.setState({ type: 4 })}
                                />
                                {/* <Button text="vendas" color="light" marginTop={5} />
                                <Button text="histórico resgates" color="light" marginTop={5} /> */}
                            </Collumn>
                            <Collumn>
                                {type !== 1 && (
                                    <Formik
                                        initialValues={{
                                            month: '',
                                            year: '',
                                        }}
                                        onSubmit={values => this.handleSearch(values)}
                                        validationSchema={validationSchema}
                                    >
                                        {({ values, setFieldValue, isValid, handleSubmit }) => (
                                            <React.Fragment>
                                                <InputOptionsLine
                                                    width="90%"
                                                    onChange={v => setFieldValue('month', v)}
                                                    value={values.month}
                                                    items={months}
                                                />
                                                <InputOptionsLine
                                                    width="90%"
                                                    onChange={v => setFieldValue('year', v)}
                                                    value={values.year}
                                                    items={years}
                                                />
                                                <TouchableOpacity
                                                    disabled={!isValid || ponctuation.isLoading}
                                                    onPress={handleSubmit}
                                                >
                                                    <UnderlineText color="secondary">
                                                        Pesquisar
                                                    </UnderlineText>
                                                </TouchableOpacity>
                                            </React.Fragment>
                                        )}
                                    </Formik>
                                )}
                            </Collumn>
                        </WrapperCollumn>
                        {type === 1 && ponctuation.extract && (
                            <ExtractDetails extract={ponctuation.extract} />
                        )}
                        {type === 2 && (
                            <CreditsDetail
                                credits={ponctuation.credits}
                                isLoading={ponctuation.isLoading}
                            />
                        )}
                        {type === 3 && (
                            <SalesDetail sales={sales.data} isLoading={sales.isLoading} />
                        )}
                        {type === 4 && (
                            <OrdersDetail orders={orders.data} isLoading={orders.isLoading} />
                        )}
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
    ponctuation: state.ponctuation,
    orders: state.orders,
    sales: state.sales,
});

const mapDispatchToProps = dispatch => ({
    getCredits: data => dispatch(getCreditsAction(data)),
    getSales: data => dispatch(getSalesAction(data)),
    getOrders: data => dispatch(getOrdersAction(data)),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Extract);
