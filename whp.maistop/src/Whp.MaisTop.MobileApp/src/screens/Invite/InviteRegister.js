import * as React from 'react';
import { AsyncStorage } from 'react-native';
import styled from 'styled-components/native';
import { Formik } from 'formik';
import * as yup from 'yup';
import LinearGradient from 'react-native-linear-gradient';
import { connect } from 'react-redux';
import images from '../../res/images';
import LinkAction from '../../components/shared/LinkAction';
import Logo from '../../components/common/Logo';
import InputLine from '../../components/common/InputLine';
import Button from '../../components/common/Button';
import CommonText from '../../components/common/CommonText';
import Warning from '../../components/common/Warning';
import { mgmReset, userInvitedAction } from '../../redux/actions/mgm';

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

const WrapperText = styled.View`
    align-self: center;
    width: 80%;
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

const validationSchema = yup.object().shape({
    cpf: yup
        .string()
        .min(14, 'CPF deve ser preenchido')
        .required('CPF é obrigatório'),
    code: yup
        .string()
        .min(6, 'Mínimo 6 caracteres')
        .required('Codigo é obrigatório'),
    mobilePhone: yup
        .string()
        .min(11, 'Numero inválido')
        .required('Celular é obrigatório'),
});

class InviteRegister extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        warningVisible: false,
        warningMessage: '',
        warningType: 'error',
        warningTitle: 'Erro',
        codeMgm: '',
    };

    componentDidUpdate(nextProps) {
        const { reset, mgm } = this.props;
        if (mgm.success && mgm.success !== nextProps.mgm.success) {
            this.setState({
                warningVisible: true,
                warningType: 'success',
                warningTitle: 'SUCESSO!',
                warningMessage: 'Cadastro realizado, aguarde a confirmação via SMS',
            });
            reset();
        } else if (mgm.error && mgm.error !== nextProps.mgm.error) {
            this.setState({
                warningVisible: true,
                warningType: 'error',
                warningTitle: 'ERRO!',
                warningMessage: mgm.errorMessage,
            });
        }
    }

    handleSubmit = values => {
        const { userInvited } = this.props;

        userInvited({
            cpf: values.cpf,
            code: values.code,
            cellphone: values.mobilePhone,
        })
    }

    render() {
        const { warningVisible, warningMessage, warningType, warningTitle, } = this.state;

        return (
            <Container>
                <Warning
                    type={warningType}
                    title={warningTitle}
                    text={warningMessage}
                    visible={warningVisible}
                    close={() => this.setState({ warningVisible: false })}
                />
                <GradientContainer colors={['#8D8980', '#D9D5CD']}>
                    <Logo marginTop={100} height="80px" img={images.logo} />
                    <WrapperText>
                        <HeaderText color="light" marginTop={30} center>Bem-vindo(a)!</HeaderText>
                        <CommonText type="normal" color="light" marginTop={5} center>Você recebeu um código de indicação, por favor preencha os campos abaixo e aguarde a confirmação do seu cadastro via SMS.</CommonText>
                    </WrapperText>
                    
                    <Formik
                        ref="formik"
                        initialValues={{ cpf: '', code: '', mobilePhone: '' }}
                        onSubmit={values => this.handleSubmit(values)}
                        validationSchema={validationSchema}
                    >
                        {({ values, handleChange, errors, isValid, handleSubmit }) => (
                            <React.Fragment>
                                <InputLine
                                    mask="cpf"
                                    maxLength={14}
                                    value={values.cpf}
                                    marginTop={30}
                                    placeholder="CPF*"
                                    textColor="light"
                                    onChange={handleChange('cpf')}
                                    errorMessage={errors.cpf}
                                    returnKeyType='done'
                                    inputRef={ref => this.cpfInput = ref}
                                    onSubmitEditing={() => this.codeInput.focus()}
                                />
                                <InputLine
                                    maxLength={6}
                                    value={values.code}
                                    placeholder="CÓDIGO DE INDICAÇÃO*"
                                    textColor="light"
                                    onChange={handleChange('code')}
                                    errorMessage={errors.code}
                                    inputRef={ref => this.codeInput = ref}
                                    onSubmitEditing={() => this.mobilePhoneInput._inputElement.focus()}
                                />
                                <InputLine
                                    value={values.mobilePhone}
                                    maxLength={15}
                                    placeholder="CELULAR*"
                                    mask="cel-phone"
                                    textColor="light"
                                    onChange={handleChange('mobilePhone')}
                                    errorMessage={errors.mobilePhone}
                                    inputRef={ref => this.mobilePhoneInput = ref}
                                    returnKeyType='done'
                                    // onSubmitEditing={() => this.codeInput.focus()}
                                />
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
                </GradientContainer>
            </Container>
        );
    }
}

// Redux
const mapStateToProps = state => ({
    mgm: state.mgm
});

const mapDispatchToProps = dispatch => ({
    userInvited: data => dispatch(userInvitedAction(data)) ,
    reset: () => dispatch(mgmReset()),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(InviteRegister);
