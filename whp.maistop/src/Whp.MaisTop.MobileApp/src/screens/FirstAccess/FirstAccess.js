import * as React from 'react';
import { Image } from 'react-native';
import styled from 'styled-components/native';
import { Formik } from 'formik';
import * as yup from 'yup';
import { connect } from 'react-redux';
import images from '../../res/images';
import { firstAccessAction, firstAccessReset } from '../../redux/actions/firstAccess';
import LinkAction from '../../components/shared/LinkAction';
import Logo from '../../components/common/Logo';
import InputLine from '../../components/common/InputLine';
import Button from '../../components/common/Button';
import CommonText from '../../components/common/CommonText';
import ROUTENAMES from '../../navigation/routeName';
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

const ImageContainer = styled.View`
    flex: 1;
    width: 100%;
`;

const BottomImage = styled.Image`
    height: 300;
    margin-top: 20;
    align-self: center;
`;

const validationSchema = yup.object().shape({
    cpf: yup
        .string()
        .min(14, 'CPF deve ser preenchido')
        .required('CPF é obrigatório'),
});

class FirstAccess extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        warningVisible: false,
        warningMessage: '',
    };

    componentDidUpdate(nextProps) {
        const { firstAccess, reset, navigation } = this.props;
        if (firstAccess.success && firstAccess.success !== nextProps.success) {
            reset();
            navigation.navigate(ROUTENAMES.SIGNUP);
        } else if (firstAccess.error && firstAccess.error !== nextProps.error) {
            reset();
            this.setState({
                warningVisible: true,
                warningMessage: firstAccess.errorMessage,
            });
        }
    }

    render() {
        const { warningVisible, warningMessage } = this.state;
        const { navigation, firstAccess, getPreRegister } = this.props;

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
                <Logo height="80px" img={images.logo} />
                    <CommonText type="subtitle" center marginTop={25}>
                        Bem-vindo (a)
                    </CommonText>
                    <Formik
                        initialValues={{ cpf: '' }}
                        onSubmit={values => getPreRegister(values.cpf)}
                        validationSchema={validationSchema}
                    >
                        {({ values, handleChange, errors, isValid, handleSubmit }) => (
                            <React.Fragment>
                                <InputLine
                                    mask="cpf"
                                    maxLength={14}
                                    value={values.cpf}
                                    placeholder="INFORME SEU CPF:"
                                    center
                                    onChange={handleChange('cpf')}
                                    errorMessage={errors.cpf}
                                    // returnKeyType='done'
                                    onSubmitEditing={handleSubmit}
                                />
                                <ButtonContainer>
                                    <Button
                                        isLoading={firstAccess.isLoading}
                                        onPress={handleSubmit}
                                        text="Entrar"
                                        disabled={!isValid}
                                        color="primary"
                                    />
                                    <Button onPress={() => navigation.pop()} text="Voltar" color="secondary" />
                                </ButtonContainer>
                            </React.Fragment>
                        )}
                    </Formik>
                    <WrapperCenter>
                        <CommonText type="normal" center marginTop={25}>
                            <CommonText type="normal" bold center color="secondary">
                                Dúvidas?{' '}
                            </CommonText>
                            Ligue para <Phone type="tel" value="0800 780 0606" /> e fale com um de nossos atendentes
                        </CommonText>
                    </WrapperCenter>
                </Top>
                <Bottom>
                    <ImageContainer>
                        <BottomImage source={images.manBottom1} resizeMode="contain" />
                    </ImageContainer>
                </Bottom>
            </Container>
        );
    }
}

// Redux
const mapStateToProps = state => ({
    firstAccess: state.firstAccess,
});

const mapDispatchToProps = dispatch => ({
    getPreRegister: cpf => dispatch(firstAccessAction(cpf)),
    reset: () => dispatch(firstAccessReset()),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(FirstAccess);
