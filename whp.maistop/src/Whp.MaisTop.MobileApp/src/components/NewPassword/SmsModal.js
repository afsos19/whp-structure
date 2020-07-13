import React from 'react';
import styled from 'styled-components/native';
import Icon from 'react-native-vector-icons/Ionicons';
import jwtDecode from 'jwt-decode';
import { AsyncStorage, TouchableOpacity, ActivityIndicator } from 'react-native';
import { connect } from 'react-redux';
import { getSMSAction, smsReset } from '../../redux/actions/sms';
import { newPasswordAction, authReset } from '../../redux/actions/auth';
import CommonText from '../common/CommonText';
import Card from '../common/Card';
import InputCode from './InputCode';
import images from '../../res/images';

const Modal = styled.Modal.attrs({
    animationType: 'slide',
    transparent: true,
    visible: p => p.visible,
})``;

const Wrapper = styled.View`
    align-items: center;
    justify-content: center;
    width: 100%;
    height: 100%;
    padding-top: 45;
    padding-bottom: 15;
    padding-horizontal: 20;
    background-color: ${p => p.theme.colors.modalTransparent};
`;

const WrapperBottom = styled.View`
    align-self: center;
    align-items: center;
    justify-content: center;
    flex-direction: row;
`;

const WrapperButton = styled.TouchableOpacity`
    align-self: center;
    margin-top: 20;
    background-color: ${p => p.theme.colors.secondary};
    justify-content: center;
    align-items: center;
    margin-horizontal: 5;
    border: 0;
    margin-top: 20;
    width: 133;
    height: 35.5;
    border-radius: 8;
`;

const TitleButton = styled.Text`
    font-family: ${p => p.theme.fontFamily.button};
    font-size: ${p => p.theme.fontSize.button};
    color: ${p => p.theme.colors.textLight};
    text-align: center;
    font-size: ${p => p.theme.fontSize.button};
`;

// CODE
const WrapperCard = styled.View`
    align-self: center;
    align-items: center;
    width: 80%;
    margin-bottom: 20;
`;

const WrapperClose = styled.View`
    height: 80;
    width: 100%;
    border-top-right-radius: 17;
    border-top-left-radius: 17;
    background-color: ${p => p.theme.colors.tertiaryBackground};
    align-items: center;
    justify-content: center;
`;

// ERROR
const WrapperCardError = styled.View`
    align-self: center;
    align-items: center;
    width: 80%;
    margin-bottom: 20;
`;

const WrapperCloseError = styled.View`
    width: 100%;
    border-top-right-radius: 17;
    border-top-left-radius: 17;
    padding-bottom: 16;
    background-color: ${p => p.theme.colors.tertiaryBackground};
    align-items: center;
    justify-content: center;
`;

// SUCCESS
const WrapperCardSuccess = styled.View`
    align-self: center;
    align-items: center;
    background-color: ${p => p.theme.colors.tertiaryBackground};
    width: 100%;
    border-radius: 17;
    padding-bottom: 20;
`;

const ModalImage = styled.Image`
    width: 120;
    height: 120;
    margin-top: 60;
    align-self: center;
`;


const TouchIcon = styled.TouchableOpacity`
    position: absolute;
    top: 5;
    right: 5;
`;

const CloseIcon = styled(Icon).attrs({
        name: 'ios-close',
    size: 20,
    color: p => p.theme.colors.iconLight,
})``;

const UnderlineText = styled(CommonText)`
    text-decoration-style: solid;
    text-decoration-line: underline;
`;

const WrapperCoutdown = styled.View`
    margin-top: 16;
    align-self: center;
    flex-direction: row;
    justify-content: space-between;
    width: 80%;
`;

class SmsModal extends React.Component {
    state = {
        modalState: 'code',
        // code
        // success
        // error
        code: ['', '', '', '', '', '', ],
        errorMsg: '',
    }

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

    componentDidUpdate(nextProps) {
        const { auth, reset, sms, resetSms } = this.props;
        if (auth.success && auth.success !== nextProps.success) {
            this.setState({ modalState: 'success' })
            reset();
        } else if (auth.error && auth.error !== nextProps.error) {
            this.setState({ modalState: 'error' })
            reset();
        }
        if (sms.error && sms.error !== nextProps.sms.error) {
            this.setState({ modalState: 'error', errorMsg: sms.errorMessage })
            resetSms();
        }
    }

    handleSendCode = () => {
        const { newPassword, auth, newPass } = this.props;
        const { code } = this.state;
        
        let myCode = '';
        code.map(v => myCode = myCode + v);

        newPassword({
            token: myCode,
            password: newPass
        }, auth.token);
    }

    render() {
        const { modalState, errorMsg } = this.state;
        const { visible = false, close, success, resend, auth, timer, timerOn } = this.props;

        let minutes = Math.floor(timer / 60).toString();
        let seconds = (timer - minutes * 60).toString();
        if (minutes.length <= 1) {
            minutes = "0" + minutes;
        }
        if (seconds.length <= 1) {
            seconds = "0" + seconds;
        }

        return (
            <Modal visible={visible}>
                <Wrapper>
                    <Card width="90%" marginTop={15} noPaddingTop>
                        {modalState === 'code' ?
                            (
                                <React.Fragment>
                                    <WrapperClose>
                                        <CommonText type="h3" color="light" center>Quase lá!</CommonText>
                                        <TouchIcon onPress={() => close()}>
                                            <CloseIcon />
                                        </TouchIcon>
                                    </WrapperClose>
                                    <WrapperCard>
                                        <CommonText type="normal" color="dark" marginTop={20} center>Digite o código que você recebeu por SMS para ativar seu cadastro.</CommonText>
                                        <InputCode onChange={values => this.setState({ code: values })}/>
                                        <WrapperButton onPress={() => this.handleSendCode()}>
                                            {!auth.isLoading ?
                                                <TitleButton>ENVIAR</TitleButton>
                                                :
                                                <ActivityIndicator />
                                            }
                                        </WrapperButton>
                                        {timerOn ?
                                            <WrapperCoutdown>
                                                <CommonText>Reenviar código</CommonText>
                                                <CommonText>{minutes}:{seconds}</CommonText>
                                            </WrapperCoutdown>
                                            :
                                            <TouchableOpacity onPress={() => resend()}>
                                                <UnderlineText color="dark" type="small" marginTop="10" center>REENVIAR CÓDIGO</UnderlineText>
                                            </TouchableOpacity>
                                        }
                                    </WrapperCard>
                                </React.Fragment>
                            )
                            : modalState === 'error'
                            ?(
                                <React.Fragment>
                                    <WrapperCloseError>
                                        <ModalImage source={images.modalError} resizeMode="contain" />
                                        <CommonText type="h3" color="light" marginTop={24} center>Ops, algo deu errado!</CommonText>
                                        {errorMsg &&
                                            <CommonText type="normal" color="light" marginTop={24} center>{errorMsg}</CommonText>
                                        }
                                        <TouchIcon onPress={() => close()}>
                                            <CloseIcon />
                                        </TouchIcon>
                                    </WrapperCloseError>
                                    <WrapperCardError>
                                        <WrapperButton onPress={() => {
                                            this.setState({ modalState: 'code' })
                                        }}>
                                            <TitleButton>TENTAR NOVAMENTE</TitleButton>
                                        </WrapperButton>
                                        <TouchableOpacity onPress={() => resend()}>
                                            <UnderlineText color="dark" type="small" marginTop="10" center>REENVIAR CÓDIGO</UnderlineText>
                                        </TouchableOpacity>
                                    </WrapperCardError>
                                </React.Fragment>
                            )
                            :
                            <WrapperCardSuccess>
                                <ModalImage source={images.modalSuccess} resizeMode="contain" />
                                <CommonText type="h3" color="light" marginTop={24} center>Troca de senha efetuada com sucesso!</CommonText>
                                <WrapperButton onPress={() => {
                                    close()
                                    success()
                                }}>
                                    <TitleButton>LOGIN</TitleButton>
                                </WrapperButton>
                            </WrapperCardSuccess>
                        }
                    </Card>
                </Wrapper>
            </Modal>
        );
    }
}

// Redux
const mapStateToProps = state => ({
    sms: state.sms,
    auth: state.auth,
    signup: state.signup,
});

const mapDispatchToProps = dispatch => ({
    getSMS: data => dispatch(getSMSAction(data)),
    newPassword: (data, token) => dispatch(newPasswordAction(data, token)),
    reset: () => dispatch(authReset()),
    resetSms: () => dispatch(smsReset()),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(SmsModal);
