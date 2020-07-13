/* eslint-disable no-nested-ternary */
/* eslint-disable react/no-string-refs */
import * as React from 'react';
import { KeyboardAvoidingView, Dimensions } from 'react-native';
import styled from 'styled-components/native';
import LinearGradient from 'react-native-linear-gradient';
import { connect } from 'react-redux';
import { SafeAreaView } from 'react-navigation';
import CommonText from '../../components/common/CommonText';
import Card from '../../components/common/Card';
import Header from '../../components/common/Header';
import FormOne from '../../components/User/FormOne';
import Steps from '../../components/User/Steps';
import FormTwo from '../../components/User/FormTwo';
import Warning from '../../components/common/Warning';
import FormThree from '../../components/User/FormThree';
import ROUTENAMES from '../../navigation/routeName';
import { pdfRegulation } from '../../utils/urls';

const heightScreen = Dimensions.get('window').height;

const Container = styled(LinearGradient).attrs({
    colors: p => [p.theme.colors.gradientDarkStart, p.theme.colors.gradientDarkEnd],
})`
    flex: 1;
    align-items: center;
`;

const Scroll = styled.ScrollView.attrs({
    showsVerticalScrollIndicator: false
})`
    width: 100%;
`;

class Signup extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        warningVisible: false,
        step: 1,
        formOneValue: null,
        formTwoValue: null,
        formThreeValue: null,
    };

    handleSignup = () => {
        const { formOneValue, formTwoValue, formThreeValue } = this.state;
        const { navigation } = this.props;
        navigation.navigate(ROUTENAMES.SMS, { formOneValue, formTwoValue, formThreeValue });
    };

    handleSubmitOne = async values => {
        this.setState({
            step: 2,
            formOneValue: values,
        });
        this.refs.scrollView.scrollTo({ x: 0, y: heightScreen*0.95, animated: true })
    };

    handleBackOne = () => {
        this.setState({ step: 1 });
        this.refs.scrollView.scrollTo({ x: 0, y: 0, animated: true })
    };

    handleSubmitTwo = values => {
        this.setState({
            step: 3,
            formTwoValue: values,
        });
        this.refs.scrollView.scrollTo({ x: 0, y: heightScreen*0.45, animated: true });
    };

    handleBackTwo = () => {
        this.setState({ step: 2 });
        this.refs.scrollView.scrollTo({ x: 0, y: 0, animated: true })
    };

    handleSubmitThree = values => {
        this.setState({
            formThreeValue: values,
        });
        this.handleSignup();
    };

    render() {
        const { warningVisible, step, formOneValue, formTwoValue, formThreeValue } = this.state;
        const { navigation, firstAccess } = this.props;

        return (
            <KeyboardAvoidingView style={{ flex: 1 }} behavior="padding" enabled>
                <Container>
                    <SafeAreaView />
                    <Warning
                        type="error"
                        title="ERROR!"
                        text="Seu CPF ainda não está cadastrado. Entre em contato com o Gestor da Informação da sua revenda e peça para incluir."
                        visible={warningVisible}
                        close={() => this.setState({ warningVisible: false })}
                    />
                    <Header navigation={navigation} />

                    <Scroll ref="scrollView">
                        <Card marginBottom={40} width="90%">
                            <CommonText type="h2" center color="secondary">
                                Meu Perfil
                            </CommonText>
                            <Steps step={step} />
                            {step === 1 ? (
                                <FormOne
                                    submit={values => this.handleSubmitOne(values)}
                                    user={firstAccess.user}
                                    shops={firstAccess.shops}
                                    backForm={formOneValue}
                                />
                            ) : step === 2 ? (
                                <FormTwo
                                    submit={values => this.handleSubmitTwo(values)}
                                    back={() => this.handleBackOne()}
                                    backForm={formTwoValue}
                                />
                            ) : (
                                <FormThree
                                    submit={values => this.handleSubmitThree(values)}
                                    navigation={navigation}
                                    back={() => this.handleBackTwo()}
                                    isLoading={firstAccess.isLoading}
                                    backForm={formThreeValue}
                                    pdfName={firstAccess.shops[0] ? firstAccess.shops[0].network.siteShortName.replace(/\s/g, '') : ""}
                                    openPdf={() => navigation.navigate(ROUTENAMES.PDF_SCREEN_NO_AUTH, { source: pdfRegulation(firstAccess.shops[0].network.siteShortName.replace(/\s/g, '')) })}
                                />
                            )}
                        </Card>
                    </Scroll>
                </Container>
            </KeyboardAvoidingView>
        );
    }
}

// Redux
const mapStateToProps = state => ({
    firstAccess: state.firstAccess,
});

export default connect(mapStateToProps)(Signup);
