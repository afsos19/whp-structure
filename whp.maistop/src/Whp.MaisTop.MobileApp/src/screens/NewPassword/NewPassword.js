import * as React from 'react';
import { AsyncStorage, Alert } from 'react-native';
import styled from 'styled-components/native';
import { Formik } from 'formik';
import * as yup from 'yup';
import LinearGradient from 'react-native-linear-gradient';
import jwtDecode from 'jwt-decode';
import { connect } from 'react-redux';
import images from '../../res/images';
import ROUTENAMES from '../../navigation/routeName';
import { newPasswordAction, authReset } from '../../redux/actions/auth';
import { getSMSPasswordAction, smsReset } from '../../redux/actions/sms';
import LinkAction from '../../components/shared/LinkAction';
import Logo from '../../components/common/Logo';
import InputLine from '../../components/common/InputLine';
import Button from '../../components/common/Button';
import CommonText from '../../components/common/CommonText';
import Warning from '../../components/common/Warning';
import SmsModal from '../../components/NewPassword/SmsModal';

const Container = styled.KeyboardAvoidingView`
    flex: 1;
`;

const WrapperHelp = styled.View`
    align-self: center;
    width: 70%;
`;

const WrapperValidation = styled.View`
    margin-top: 10;
    align-self: center;    
    width: 80%;
`;

const GradientContainer = styled(LinearGradient)`
    flex: 1;
    align-self: flex-start;
    justify-content: flex-start;
    width: 100%;
    /* padding: 0 80px 100px 35px; */
`;

const Right = styled.View`
    align-items: flex-end;
`;

const Bottom = styled.View`
    max-width: 200px;
`;

const ButtonContainer = styled.View`
    margin-top: 20px;
`;

const HeaderText = styled(CommonText)`
    font-family: "Montserrat-Light";
    font-size: 25;
`;

const Phone = styled(LinkAction)`
    text-decoration-style: solid;
    text-decoration-line: underline;
`;

const ImageContainer = styled.View`
    flex: 1;
    align-self: center;
    align-items: center;
    justify-content: flex-end;
`;

const BottomImage = styled.Image`
    height: 180;
`;

const validationSchema = yup.object().shape({
    password: yup
        .string()
        .min(8, 'Senha deve ter pelo menos 8 caracteres')
        .matches(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/, {
            message: 'Senha invalida',
            excludeEmptyString: true,
        })
        .required('Senha é obrigatório'),
    confirmPassword: yup.string().when('password', (value, schema) => {
        return schema
            .oneOf([yup.ref('password')], 'As senhas devem ser iguais')
            .required('Confirmar Senha é obrigatório');
    }),
});

class NewPassword extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        warningVisible: false,
        warningMessage: '',
        smsVisible: false,
        newPass: '',
        time: 300,
        time: 300,
        timer: 300,
        timerOn: false,
    };

    componentDidMount() {        
        AsyncStorage.getItem("newPasswordTime").then(value => {
            const { time } = this.state;
            if (value) {
                const timeNow = Math.floor(Date.now() / 1000);
                let timer = time - (timeNow - parseInt(value));
                if (timer > 0) {
                    this.setState({ timerOn: true, timer });
                    this.startTimer();
                } else {
                    AsyncStorage.removeItem("newPasswordTime");
                }
            }
        })
    }

    handleSuccessCode = () => {
        const { newPassword, auth } = this.props;
        const { newPass } = this.state;
        newPassword({
            password: newPass
        }, auth.token);

    }

    handleNewPassword = (values) => {
        const { timerOn } = this.state;
        const { auth, getSMS } = this.props;
        if (!timerOn) {
            getSMS({
                password: values.password
            }, auth.token);
            this.startTimer();
            this.setState({ timerOn: true });
            AsyncStorage.setItem("newPasswordTime", Math.floor(Date.now() / 1000).toString());
        }
        this.setState({ newPass: values.password, smsVisible: true })
    }

    startTimer = () => {
        this.clockCall = setInterval(() => {
            this.decrementClock();
        }, 1000);
    }

    decrementClock = () => {  
        const { time } = this.state;

        if(this.state.timer === 0) {
            AsyncStorage.removeItem("newPasswordTime");
            this.setState({ timerOn: false, timer: time });
            clearInterval(this.clockCall);
            
        }
        this.setState((prevstate) => ({ timer: prevstate.timer-1 }));
    };       

    componentWillUnmount() {
        clearInterval(this.clockCall);
    }

    resendSmsCode = () => {
        const { auth, getSMS } = this.props;
        const { timerOn, newPass } = this.state;
        if (!timerOn) {
            getSMS({
                password: newPass
            }, auth.token);
            this.startTimer();
            this.setState({ timerOn: true });
            AsyncStorage.setItem("newPasswordTime", Math.floor(Date.now() / 1000).toString());
        }
    }

    render() {
        const { warningVisible, warningMessage, smsVisible, newPass, timer, timerOn } = this.state;
        const { auth, navigation } = this.props;

        return (
            <Container>
                <Warning
                    type="error"
                    title="Aviso"
                    text={warningMessage}
                    visible={warningVisible}
                    close={() => this.setState({ warningVisible: false })}
                />
                <SmsModal 
                    newPass={newPass}
                    visible={smsVisible}
                    close={() => this.setState({ smsVisible: false })}
                    resend={() => this.resendSmsCode()}
                    success={() => navigation.navigate(ROUTENAMES.LOGIN)}
                    timer={timer}
                    timerOn={timerOn}
                />
                {/* <Top> */}
                {/* </Top> */}
                <GradientContainer colors={['#8D8980', '#D9D5CD']}>
                    <Logo marginTop={50} height="60px" img={images.logo} />
                    <HeaderText color="light" marginTop={16} center>Sua senha Expirou!</HeaderText>
                    <CommonText type="normal" color="light" marginTop={5} center>Por favor, digite uma nova senha</CommonText>
                    <CommonText type="normal" color="light" center>(Diferente da atual):</CommonText>
                    <Formik
                        initialValues={{ password: '', confirmPassword: '' }}
                        onSubmit={values => this.handleNewPassword(values)}
                        validationSchema={validationSchema}
                    >
                        {({ values, handleChange, errors, isValid, handleSubmit }) => (
                            <React.Fragment>
                                <InputLine
                                    marginTop={16}
                                    secureTextEntry
                                    value={values.password}
                                    placeholder="NOVA SENHA"
                                    textColor="light"
                                    onChange={handleChange('password')}
                                    errorMessage={errors.password}
                                />
                                <InputLine
                                    secureTextEntry
                                    value={values.confirmPassword}
                                    placeholder="CONFIRMAR NOVA SENHA"
                                    textColor="light"
                                    onChange={handleChange('confirmPassword')}
                                    errorMessage={errors.confirmPassword}
                                />
                                <WrapperValidation>
                                    <CommonText type="small" color="light">
                                        A senha deve ter pelo menos 8 caracteres, combinando letras maiúsculas, letras minúsculas, caracteres especiais e números.
                                    </CommonText>
                                </WrapperValidation>
                                <ButtonContainer>
                                    <Button
                                        onPress={handleSubmit}
                                        text="ENVIAR"
                                        disabled={!isValid}
                                        color="primary"
                                        // isLoading={user.isLoading}
                                    />
                                    <WrapperHelp>
                                        <CommonText type="normal" marginTop={25} color="light" center>
                                            <CommonText type="normal" bold color="secondary" center>
                                                Dúvidas?{' '}
                                            </CommonText>
                                            Ligue para <Phone type="tel" value="0800 780 0606" /> e fale com um de nossos atendentes
                                        </CommonText>
                                    </WrapperHelp>
                                </ButtonContainer>
                            </React.Fragment>
                        )}
                    </Formik>
                    {/* <ImageContainer>
                        <BottomImage source={images.manBottom3} resizeMode="contain" />
                    </ImageContainer> */}
                </GradientContainer>
            </Container>
        );
    }
}

// Redux
const mapStateToProps = state => ({
    auth: state.auth,
    sms: state.sms,
});

const mapDispatchToProps = dispatch => ({
    newPassword: (data, token) => dispatch(newPasswordAction(data, token)),
    reset: () => dispatch(authReset()),
    getSMS: (data, token) => dispatch(getSMSPasswordAction(data, token)),
    resetSms: () => dispatch(smsReset()),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(NewPassword);
