import * as React from 'react';
import { Image, Alert } from 'react-native';
import styled from 'styled-components/native';
import { Formik } from 'formik';
import * as yup from 'yup';
import LinearGradient from 'react-native-linear-gradient';

import { connect } from 'react-redux';
import images from '../../res/images';
import ROUTENAMES from '../../navigation/routeName';
import { loginAction, authReset } from '../../redux/actions/auth';

import LinkAction from '../../components/shared/LinkAction';
import Logo from '../../components/common/Logo';
import InputLine from '../../components/common/InputLine';
import Button from '../../components/common/Button';
import CommonText from '../../components/common/CommonText';
import Warning from '../../components/common/Warning';
import openBrowser from '../../utils/OpenLink';

const Container = styled.KeyboardAvoidingView`
    flex: 1;
`;

const Top = styled.View`
    flex: 1;
    align-self: center;
    align-items: center;
    justify-content: center;
`;

const GradientContainer = styled(LinearGradient)`
    flex: 2;
    align-self: flex-start;
    justify-content: flex-start;
    width: 90%;
    margin-bottom: 50px;
    padding: 0 100px 100px 35px;
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
`;

const Right = styled.View`
    align-items: flex-end;
    margin-right: -20;
    elevation: 2;
`;

const Bottom = styled.View`
    max-width: 200px;
`;

const ButtonContainer = styled.View`
    margin-top: 20px;
`;

const UnderlineText = styled(CommonText)`
    text-decoration: underline;
    text-decoration-color: ${p => p.theme.colors.textDark};
`;

const Phone = styled(LinkAction)`
    text-decoration-style: solid;
    text-decoration-line: underline;
`;

const ImageContainer = styled.View`
    flex: 1;
    justify-content: flex-end;
`;

const validationSchema = yup.object().shape({
    cpf: yup
        .string()
        .min(14, 'CPF deve ser preenchido')
        .required('CPF é obrigatório'),
    password: yup
        .string()
        .min(6, 'Mínimo 6 caracteres')
        .required('Senha é obrigatório'),
});

class Login extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        warningVisible: false,
        warningMessage: '',
    };

    componentDidUpdate(nextProps) {
        const { auth, reset, navigation } = this.props;
        if (auth.success && auth.success !== nextProps.auth.success) {
            if (auth.messageLogin == 'Usuário encontrado com sucesso') {
                reset();
                navigation.navigate(ROUTENAMES.LOADING_APP);
            } else if (auth.messageLogin == 'Senha expirada!') {
                reset();
                navigation.navigate(ROUTENAMES.NEW_PASSWORD);
            } else if (auth.messageLogin == 'Usuario somente catalogo') {
                if (auth.onlyShop) {
                    openBrowser(auth.onlyShop)
                }
                // this.setState({
                //     warningVisible: true,
                //     warningMessage: "Usuario somente catalogo",
                // });
                reset();
            }
        } else if (auth.error && auth.error !== nextProps.auth.error) {
            this.setState({
                warningVisible: true,
                warningMessage: auth.errorMessage,
            });
            reset();
        }
    }

    render() {
        const { warningVisible, warningMessage } = this.state;
        const { login, auth, navigation } = this.props;

        return (
            <Container>
                <Warning
                    type="error"
                    title="Aviso"
                    text={warningMessage}
                    visible={warningVisible}
                    close={() => this.setState({ warningVisible: false })}
                />
                <Top>
                    <Logo height="90px" img={images.logo} />
                </Top>
                <GradientContainer colors={['#8D8980', '#D9D5CD']}>
                    <Formik
                        initialValues={{ cpf: '', password: '' }}
                        onSubmit={values => login(values)}
                        validationSchema={validationSchema}
                    >
                        {({ values, handleChange, errors, isValid, handleSubmit }) => (
                            <React.Fragment>
                                <InputLine
                                    mask="cpf"
                                    maxLength={14}
                                    value={values.cpf}
                                    placeholder="CPF"
                                    textColor="light"
                                    onChange={handleChange('cpf')}
                                    errorMessage={errors.cpf}
                                    inputRef={ref => this.cpfInput = ref}
                                    returnKeyType='done'
                                    onSubmitEditing={() => this.passwordInput.focus()}
                                    textContentType="none"
                                    autoCapitalize="none"
                                    autoCorrect={false}
                                 />
                                <InputLine
                                    secureTextEntry
                                    value={values.password}
                                    placeholder="SENHA"
                                    textColor="light"
                                    onChange={handleChange('password')}
                                    errorMessage={errors.password}
                                    returnKeyType='done'
                                    inputRef={ref => this.passwordInput = ref}
                                    onSubmitEditing={handleSubmit}
                                    textContentType="none"
                                    autoCapitalize="none"
                                    autoCorrect={false}
                                />
                                <UnderlineText
                                    center
                                    type="small"
                                    marginTop={20}
                                    color="light"
                                    bold
                                    onPress={() => navigation.navigate(ROUTENAMES.FORGOT_PASSWORD)}
                                >
                                    Esqueci minha senha
                                </UnderlineText>
                                <ButtonContainer>
                                    <Button
                                        onPress={handleSubmit}
                                        text="Entrar"
                                        disabled={!isValid}
                                        color="primary"
                                        isLoading={auth.isLoading}
                                    />
                                    <Button
                                        onPress={() => navigation.navigate(ROUTENAMES.FIRST_ACCESS)}
                                        text="Primeiro Acesso"
                                        color="transparent"
                                        marginTop={10}
                                        bold
                                    />
                                </ButtonContainer>
                            </React.Fragment>
                        )}
                    </Formik>
                    <Bottom>
                        <CommonText type="normal" marginTop={25} color="light" center>
                            <CommonText type="normal" bold color="light">
                                Dúvidas?{' '}
                            </CommonText>
                            Ligue para <Phone type="tel" value="0800 780 0606" /> e fale com um de nossos atendentes
                        </CommonText>
                    </Bottom>
                </GradientContainer>
                <Right>
                    <ImageContainer>
                        <Image source={images.manRight1} />
                    </ImageContainer>
                </Right>
            </Container>
        );
    }
}

// Redux
const mapStateToProps = state => ({
    auth: state.auth,
});

const mapDispatchToProps = dispatch => ({
    login: data => dispatch(loginAction(data)),
    reset: () => dispatch(authReset()),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Login);
