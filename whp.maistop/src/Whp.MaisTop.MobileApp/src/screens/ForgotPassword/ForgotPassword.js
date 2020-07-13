import * as React from 'react';
import { AsyncStorage } from 'react-native';
import styled from 'styled-components/native';
import { Formik } from 'formik';
import * as yup from 'yup';

import { connect } from 'react-redux';
import images from '../../res/images';
import ROUTENAMES from '../../navigation/routeName';

import { resetPasswordAction, resetPasswordReset } from '../../redux/actions/resetPassword';
import LinkAction from '../../components/shared/LinkAction';
import Logo from '../../components/common/Logo';
import InputLine from '../../components/common/InputLine';
import Button from '../../components/common/Button';
import CommonText from '../../components/common/CommonText';
import Warning from '../../components/common/Warning';

const Container = styled.KeyboardAvoidingView`
    flex: 1;
`;

const Top = styled.View`
    flex: 2;
    align-items: center;
    justify-content: center;
`;

const Bottom = styled.View`
    flex: 1;
    justify-content: flex-end;
`;

const ButtonContainer = styled.View`
    flex-direction: row;
    align-items: center;
    margin-top: 25px;
`;

const Phone = styled(LinkAction)`
    text-decoration-style: solid;
    text-decoration-line: underline;
`;

const WrapperCenter = styled.View`
    margin-top: 8;
    align-self: center;
    width: 80%;
`;

const Line = styled.View`
    align-self: center;
    width: 80%;
    height: .5;
    background-color: ${p => p.theme.colors.textDark};
    margin-top: 8;
    margin-bottom: 8;
`;

const WrapperCoutdown = styled.View`
    align-self: center;
    flex-direction: row;
    justify-content: space-between;
    width: 80%;
`;

const ImageContainer = styled.View`
    flex: 1;
    width: 100%;
`;

const BottomImage = styled.Image`
    height: 300;
    align-self: center;
`;

const validationSchema = yup.object().shape({
    cpf: yup
        .string()
        .min(14, 'CPF deve ser preenchido')
        .required('CPF é obrigatório'),
});

class ForgotPassword extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        warningVisible: false,
        warningType: 'error',
        warningTitle: '',
        warningMessage: '',
        time: 300,
        timer: 300,
        timerOn: false,
    };

    componentDidMount() {        
        AsyncStorage.getItem("forgotPasswordTime").then(value => {
            const { time } = this.state;
            if (value) {
                const timeNow = Math.floor(Date.now() / 1000);
                let timer = time - (timeNow - parseInt(value));
                if (timer > 0) {
                    this.setState({ timerOn: true, timer });
                    this.startTimer();
                } else {
                    AsyncStorage.removeItem("forgotPasswordTime");
                }
            }
        })
    }
    
    componentDidUpdate(nextProps) {
        const { resetPassword, reset } = this.props;
        if (resetPassword.success && resetPassword.success !== nextProps.success) {
            this.setState({
                warningVisible: true,
                warningType: 'success',
                warningTitle: 'SUCESSO!',
                warningMessage: resetPassword.successMessage,
            });
            
            this.startTimer();
            AsyncStorage.setItem("forgotPasswordTime", Math.floor(Date.now() / 1000).toString());
            this.setState({ timerOn: true });
            reset();
        } else if (resetPassword.error && resetPassword.error !== nextProps.error) {
            this.setState({
                warningVisible: true,
                warningType: 'error',
                warningTitle: 'ERRO!',
                warningMessage: resetPassword.errorMessage,
            });
            reset();
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
            AsyncStorage.removeItem("forgotPasswordTime");
            this.setState({ timerOn: false, timer: time });
            clearInterval(this.clockCall);
            
        }
        this.setState((prevstate) => ({ timer: prevstate.timer-1 }));
    };
       
      
    componentWillUnmount() {
        clearInterval(this.clockCall);
    }

    handleForgotPassword = values => {
        const { getResetPassword } = this.props;
        const { timerOn } = this.state;
        
        if (!timerOn) {
            getResetPassword(values.cpf);
        }
    }

    render() {
        const { warningVisible, warningType, warningTitle, warningMessage, timer, timerOn } = this.state;
        const { navigation, resetPassword, getResetPassword } = this.props;

        let minutes = Math.floor(timer / 60).toString();
        let seconds = (timer - minutes * 60).toString();
        if (minutes.length <= 1) {
            minutes = "0" + minutes;
        }
        if (seconds.length <= 1) {
            seconds = "0" + seconds;
        }

        return (
            <Container>
                <Warning
                    type={warningType}
                    title={warningTitle}
                    text={warningMessage}
                    visible={warningVisible}
                    close={() => this.setState({ warningVisible: false })}
                />
                <Top>
                    <Logo height="80px" img={images.logo} />
                    <CommonText type="subtitle" center marginTop={25}>
                        Esqueceu sua senha?
                    </CommonText>
                    <Formik
                        initialValues={{ cpf: '' }}
                        onSubmit={values => this.handleForgotPassword(values)}
                        validationSchema={validationSchema}
                    >
                        {({ values, handleChange, errors, isValid, handleSubmit }) => (
                            <React.Fragment>
                                <InputLine
                                    mask="cpf"
                                    maxLength={14}
                                    value={values.cpf}
                                    placeholder="DIGITE SEU CPF CADASTRADO:"
                                    center
                                    onChange={handleChange('cpf')}
                                    errorMessage={errors.cpf}
                                    returnKeyType='done'
                                    onSubmitEditing={handleSubmit}
                                />

                                <ButtonContainer>
                                    <Button
                                        isLoading={resetPassword.isLoading}
                                        onPress={handleSubmit}
                                        text="Enviar"
                                        disabled={!isValid}
                                        color="primary"
                                    />
                                    <Button
                                        onPress={() => navigation.goBack()}
                                        text="Voltar"
                                        color="secondary"
                                    />
                                </ButtonContainer>

                                {timerOn &&
                                    <React.Fragment>
                                        <Line />
                                        <WrapperCoutdown>
                                            <CommonText>Reenviar código</CommonText>
                                            <CommonText>{minutes}:{seconds}</CommonText>
                                        </WrapperCoutdown>
                                        <Line />
                                    </React.Fragment>
                                }
                            </React.Fragment>
                        )}
                    </Formik>
                    <WrapperCenter>
                        <CommonText type="normal" center marginTop={10}>
                            <CommonText type="normal" bold center>
                                Dúvidas?{' '}
                            </CommonText>
                            Ligue para <Phone type="tel" value="0800 780 0606" /> e fale com um de nossos atendentes
                        </CommonText>
                    </WrapperCenter>
                </Top>
                <Bottom>
                    <ImageContainer>
                        <BottomImage source={images.manBottom2} resizeMode="contain"/>
                    </ImageContainer>
                </Bottom>
            </Container>
        );
    }
}

// Redux
const mapStateToProps = state => ({
    resetPassword: state.resetPassword,
});

const mapDispatchToProps = dispatch => ({
    getResetPassword: cpf => dispatch(resetPasswordAction(cpf)),
    reset: () => dispatch(resetPasswordReset()),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ForgotPassword);
