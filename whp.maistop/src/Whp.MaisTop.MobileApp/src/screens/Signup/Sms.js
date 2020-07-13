/* eslint-disable no-nested-ternary */
/* eslint-disable react/no-string-refs */
import * as React from 'react';
import { AsyncStorage, KeyboardAvoidingView } from 'react-native';
import styled from 'styled-components/native';
import LinearGradient from 'react-native-linear-gradient';
import { connect } from 'react-redux';
import { SafeAreaView } from 'react-navigation';
import { getSMSAction, smsReset } from '../../redux/actions/sms';
import { preRegistrationAction, signupReset } from '../../redux/actions/signup';
import CommonText from '../../components/common/CommonText';
import Card from '../../components/common/Card';
import Header from '../../components/common/Header';
import Warning from '../../components/common/Warning';
import FormSms from '../../components/User/FormSms';
import ROUTENAMES from '../../navigation/routeName';

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

class Sms extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        warningVisible: false,
        warningMessage: '',
        formOneValue: null,
        formTwoValue: null,
        formThreeValue: null,
        time: 300,
        timer: 300,
        timerOn: false,
    };

    componentDidMount() {
        const { navigation, getSMS, firstAccess } = this.props;

        const formOneValue = navigation.getParam('formOneValue', {});
        const formTwoValue = navigation.getParam('formTwoValue', {});
        const formThreeValue = navigation.getParam('formThreeValue', {});

        this.setState({ formOneValue, formTwoValue, formThreeValue });

        AsyncStorage.getItem("signupTime").then(value => {
            const { time } = this.state;
            if (value) {
                const timeNow = Math.floor(Date.now() / 1000);
                let timer = time - (timeNow - parseInt(value));
                if (timer > 0) {
                    this.setState({ timerOn: true, timer });
                    this.startTimer();
                } else {
                    AsyncStorage.removeItem("signupTime");
                    getSMS({
                        id: firstAccess.user.id,
                        cellPhone: formOneValue.form.mobilePhone,
                    });
                }
            } else {
                getSMS({
                    id: firstAccess.user.id,
                    cellPhone: formOneValue.form.mobilePhone,
                });
            }
        })
    }

    componentDidUpdate(nextProps) {
        const { signup, sms, resetSms, resetSignup, navigation } = this.props;
        if (signup.success && signup.success !== nextProps.signup.success) {
            navigation.navigate(ROUTENAMES.LOADING);
        } else if (signup.error && signup.error !== nextProps.signup.error) {
            resetSignup();
            this.setState({
                warningVisible: true,
                warningMessage: signup.errorMessage,
            });
        }

        if (sms.success && sms.success !== nextProps.sms.success) {
            this.startTimer();
            AsyncStorage.setItem("signupTime", Math.floor(Date.now() / 1000).toString());
            this.setState({ timerOn: true });
        } else if (sms.error && sms.error !== nextProps.sms.error) {
            resetSms();
            this.setState({
                warningVisible: true,
                warningMessage: sms.errorMessage,
            });
        }
    }

    startTimer = () => {
        this.clockCall = setInterval(() => {
            this.decrementClock();
        }, 1000);
    }

    decrementClock = () => {  
        const { time } = this.state;

        if(this.state.timer === 0) {
            AsyncStorage.removeItem("signupTime");
            this.setState({ timerOn: false, timer: time });
            clearInterval(this.clockCall);
            
        }
        this.setState((prevstate) => ({ timer: prevstate.timer-1 }));
    };
       
      
    componentWillUnmount() {
        clearInterval(this.clockCall);
    }

    handleSubmit = values => {
        const { formOneValue, formTwoValue, formThreeValue } = this.state;
        const { sms, preRegistration, firstAccess } = this.props;
        const { user, shops } = firstAccess;

        if (values.form.smsCode != sms.data.code) {
            this.setState({
                warningVisible: true,
                warningMessage: 'Código informado não esta correto.',
            });
            return
        }

        const formdata = new FormData();

        formdata.append('Id', user.id);
        formdata.append('UserStatus.Id', user.userStatus.id);
        formdata.append('UserStatus.Description', user.userStatus.description);
        formdata.append('UserStatus.CreatedAt', user.userStatus.createdAt);
        formdata.append('UserStatus.Activated', user.userStatus.activated);
        formdata.append('Office.Id', user.office.id);
        formdata.append('Office.Description', user.office.description);
        formdata.append('Office.CreatedAt', user.office.createdAt);
        formdata.append('Office.Activated', user.office.activated);
        formdata.append('Name', user.name);
        formdata.append('CPF', user.cpf);
        formdata.append('CommercialPhone', user.commercialPhone);
        formdata.append('Photo', user.photo);
        formdata.append('CreatedAt', user.createdAt);
        formdata.append('OffIn', user.offIn);
        formdata.append('AccessCode', values.form.smsCode);
        formdata.append('FirstAcess', user.firstAcess);
        formdata.append('Network', shops[0].network.id); // ID???
        formdata.append('Shop', shops[0].id); // CNPJ ? ID?
        formdata.append('Email', formOneValue.form.email);
        formdata.append('CellPhone', formOneValue.form.mobilePhone);
        formdata.append('Phone', formOneValue.form.phone);
        formdata.append('BithDate', formOneValue.form.birthDate);
        formdata.append('Gender', formOneValue.form.gender);
        formdata.append('HeartTeam', formOneValue.form.team);
        formdata.append('SonAmout', formOneValue.form.children);
        formdata.append('CivilStatus', formOneValue.form.civilStatus);
        formdata.append('CEP', formTwoValue.form.cep);
        formdata.append('Address', formTwoValue.form.street);
        formdata.append('Number', formTwoValue.form.number);
        // Complemento
        if (formTwoValue.form.complement) {
            formdata.append('Complement', formTwoValue.form.complement);
        }
        formdata.append('Neighborhood', formTwoValue.form.neighborhood);
        formdata.append('City', formTwoValue.form.city);
        formdata.append('Uf', formTwoValue.form.state);
        if (formTwoValue.form.referencePoint) {
            formdata.append('ReferencePoint', formTwoValue.form.referencePoint);
        }
        formdata.append('Password', formThreeValue.form.password);
        formdata.append('OldPassword', null);
        if (formOneValue.image) {
            formdata.append('file', {
                uri: formOneValue.image,
                name: 'image.jpg',
                type: 'multipart/form-data',
            });
        }

        preRegistration(formdata);
    };

    handleReSendSMS = () => {
        const { formOneValue, timerOn } = this.state;
        const { getSMS, firstAccess } = this.props;
        
        if (!timerOn) {
            getSMS({
                id: firstAccess.user.id,
                cellPhone: formOneValue.form.mobilePhone,
            });
        }
    };

    render() {
        const { warningVisible, warningMessage, timerOn, timer } = this.state;
        const { navigation, signup } = this.props;

        let minutes = Math.floor(timer / 60).toString();
        let seconds = (timer - minutes * 60).toString();
        if (minutes.length <= 1) {
            minutes = "0" + minutes;
        }
        if (seconds.length <= 1) {
            seconds = "0" + seconds;
        }

        return (
            <KeyboardAvoidingView style={{ flex: 1 }} behavior="padding" enabled>
                <Container>
                    <SafeAreaView />
                    <Warning
                        type="error"
                        title="ERRO!"
                        text={warningMessage}
                        visible={warningVisible}
                        close={() => this.setState({ warningVisible: false })}
                    />
                    <Header navigation={navigation} />

                    <Scroll ref="scrollView">
                        <Card marginBottom={40} width="90%">
                            <CommonText type="h2" center color="secondary">
                                Quase lá!
                            </CommonText>
                            <CommonText type="normal" center color="dark" marginTop={30}>
                                Digite o código que você recebeu por SMS para ativar seu cadastro.
                            </CommonText>
                            <FormSms
                                timerOn={timerOn}
                                minutes={minutes}
                                seconds={seconds}
                                submit={values => this.handleSubmit(values)}
                                reSend={() => this.handleReSendSMS()}
                                isLoading={signup.isLoading}
                            />
                        </Card>
                    </Scroll>
                </Container>
            </KeyboardAvoidingView>
        );
    }
}

// Redux
const mapStateToProps = state => ({
    sms: state.sms,
    firstAccess: state.firstAccess,
    signup: state.signup,
});

const mapDispatchToProps = dispatch => ({
    getSMS: data => dispatch(getSMSAction(data)),
    preRegistration: data => dispatch(preRegistrationAction(data)),
    resetSignup: () => dispatch(signupReset()),
    resetSms: () => dispatch(smsReset()),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Sms);
