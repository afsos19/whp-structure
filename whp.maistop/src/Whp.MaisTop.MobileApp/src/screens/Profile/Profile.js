/* eslint-disable no-nested-ternary */
/* eslint-disable react/no-string-refs */
import * as React from 'react';
import { KeyboardAvoidingView, Platform } from 'react-native';
import styled from 'styled-components/native';
import LinearGradient from 'react-native-linear-gradient';
import { connect } from 'react-redux';
import { SafeAreaView } from 'react-navigation';
import { userReset, updateUserAction } from '../../redux/actions/user';
import CommonText from '../../components/common/CommonText';
import Card from '../../components/common/Card';
import Header from '../../components/common/Header';
import FormOne from '../../components/User/FormOne';
import Steps from '../../components/User/Steps';
import FormTwo from '../../components/User/FormTwo';
import Warning from '../../components/common/Warning';
import FormThree from '../../components/User/FormThree';
import ROUTENAMES from '../../navigation/routeName';
import { imgUserPath } from '../../utils/urls';

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

class Profile extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        warningVisible: false,
        warningMessage: '',
        warningTitle: 'ERRO',
        warningType: 'error',
        step: 1,
        formOneValue: {},
        formTwoValue: {},
        formThreeValue: {},
    };

    componentDidMount() {
        const editAdress = this.props.navigation.getParam('editAdress', false);
        if (editAdress) {
            this.setState({ step: 2 })
        }
    }
    
    handleSignup = () => {
        const { formOneValue, formTwoValue, formThreeValue } = this.state;
        const { navigation } = this.props;
        navigation.navigate(ROUTENAMES.SMS, { formOneValue, formTwoValue, formThreeValue });
    };

    handleSubmitOne = values => {
        const { user, updateUser } = this.props;
        const formdata = new FormData();

        formdata.append('Name', user.data.name);
        formdata.append('Id', user.data.id);
        formdata.append('CPF', user.data.cpf);
        formdata.append('Photo', user.data.photo);
        // start FornOne
        if (values.image.toString().includes("file://")) {
            formdata.append('file', {
                uri: values.image,
                name: 'image.jpg',
                type: 'multipart/form-data',
            });
        } else {
            formdata.append('file', null);
        }
        formdata.append('Email', values.form.email);
        formdata.append('CellPhone', values.form.mobilePhone);
        formdata.append('Phone', values.form.phone);
        formdata.append('BithDate', values.form.birthDate);
        formdata.append('Gender', values.form.gender);
        formdata.append('HeartTeam', values.form.team);
        formdata.append('SonAmout', values.form.children);
        formdata.append('CivilStatus', values.form.civilStatus);
        // FormTwo
        formdata.append('Address', user.data.address);
        formdata.append('CEP', user.data.cep);
        formdata.append('Number', user.data.number);
        if (values.form.complement) {
            formdata.append('Complement', values.form.complement);
        }
        formdata.append('Neighborhood', user.data.neighborhood);
        formdata.append('City', user.data.city);
        formdata.append('Uf', user.data.uf);
        formdata.append('PrivacyPolicy', true);
        if (values.form.referencePoint) {
            formdata.append('ReferencePoint', values.form.referencePoint);
        }
        updateUser(formdata);
    };

    handleSubmitTwo = values => {
        const { user, updateUser } = this.props;
        const formdata = new FormData();
        formdata.append('Name', user.data.name);
        formdata.append('Id', user.data.id);
        formdata.append('CPF', user.data.cpf);
        formdata.append('Photo', user.data.photo);
        // start FornOne
        formdata.append('file', null);
        formdata.append('Email', user.data.email);
        formdata.append('CellPhone', user.data.cellPhone);
        formdata.append('Phone', user.data.phone);
        formdata.append('BithDate', user.data.bithDate);
        formdata.append('Gender', user.data.gender);
        formdata.append('HeartTeam', user.data.heartTeam);
        formdata.append('SonAmout', user.data.sonAmout);
        formdata.append('CivilStatus', user.data.civilStatus);
        // FormTwo
        formdata.append('Address', values.form.street);
        formdata.append('CEP', values.form.cep);
        formdata.append('Number', values.form.number);
        if (values.form.complement) {
            formdata.append('Complement', values.form.complement);
        }
        formdata.append('Neighborhood', values.form.neighborhood);
        formdata.append('City', values.form.city);
        formdata.append('Uf', values.form.state);
        formdata.append('PrivacyPolicy', true);
        if (values.form.referencePoint) {
            formdata.append('ReferencePoint', values.form.referencePoint);
        }
        updateUser(formdata);
    };

    handleSubmitThree = values => {
        const { user, updateUser } = this.props;
        const formdata = new FormData();

        formdata.append('Name', user.data.name);
        formdata.append('Id', user.data.id);
        formdata.append('CPF', user.data.cpf);
        formdata.append('Photo', user.data.photo);
        // start FornOne
        formdata.append('file', null);
        formdata.append('Email', user.data.email);
        formdata.append('CellPhone', user.data.cellPhone);
        formdata.append('Phone', user.data.phone);
        formdata.append('BithDate', user.data.bithDate);
        formdata.append('Gender', user.data.gender);
        formdata.append('HeartTeam', user.data.heartTeam);
        formdata.append('SonAmout', user.data.sonAmout);
        formdata.append('CivilStatus', user.data.civilStatus);
        // FormTwo
        formdata.append('Address', user.data.address);
        formdata.append('CEP', user.data.cep);
        formdata.append('Number', user.data.number);
        if (values.form.complement) {
            formdata.append('Complement', values.form.complement);
        }
        formdata.append('Neighborhood', user.data.neighborhood);
        formdata.append('City', user.data.city);
        formdata.append('Uf', user.data.uf);
        formdata.append('PrivacyPolicy', true);
        if (values.form.referencePoint) {
            formdata.append('ReferencePoint', values.form.referencePoint);
        }
        // FormThree
        formdata.append('Password', values.form.password);
        formdata.append('OldPassword', values.form.lastPassword);

        updateUser(formdata);
    };

    componentDidUpdate(nextProps) {
        const { user, reset } = this.props;

        if (user.error && user.error !== nextProps.user.error) {
            reset();
            this.setState({
                warningVisible: true,
                warningMessage: user.errorMessage,
                warningTitle: 'ERRO',
                warningType: 'error',
            });
        }
        if (user.success && user.success !== nextProps.user.success) {
            reset();
            this.setState({
                warningVisible: true,
                warningMessage: 'Alteração realizada com sucesso',
                warningTitle: 'Sucesso',
                warningType: 'success'

            });
        }
    }

    render() {
        const { warningVisible, warningMessage, warningType, warningTitle, step } = this.state;
        const { navigation, user, network } = this.props;

        return (
            <KeyboardAvoidingView style={{ flex: 1 }} behavior={Platform.OS === 'ios' && 'padding'} enabled>
                <Container>
                    <SafeAreaView />
                    <Warning
                        type={warningType}
                        title={warningTitle}
                        text={warningMessage}
                        visible={warningVisible}
                        close={() => this.setState({ warningVisible: false })}
                    />
                    <Header navigation={navigation} />

                    <Scroll ref="scrollView">
                        <Card marginBottom={40} width="90%">
                            <CommonText type="h2" center color="secondary">
                                Meu Perfil
                            </CommonText>
                            <Steps step={step} setStep={value => this.setState({ step: value })} />
                            {step === 1 ? (
                                <FormOne
                                    isLoading={user.isLoading}
                                    isUpdate
                                    submit={values => this.handleSubmitOne(values)}
                                    user={user.data}
                                    shops={network.data}
                                    defaultValues
                                />
                            ) : step === 2 ? (
                                <FormTwo
                                    isLoading={user.isLoading}
                                    isUpdate
                                    user={user.data}
                                    submit={values => this.handleSubmitTwo(values)}
                                    back={() => this.setState({ step: 1 })}
                                    defaultValues
                                />
                            ) : (
                                <FormThree
                                    isLoading={user.isLoading}
                                    isUpdate
                                    submit={values => this.handleSubmitThree(values)}
                                    back={() => this.setState({ step: 2 })}
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
    user: state.user,
    network: state.network,
});

const mapDispatchToProps = dispatch => ({
    updateUser: data => dispatch(updateUserAction(data)),
    reset: () => dispatch(userReset()),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Profile);
