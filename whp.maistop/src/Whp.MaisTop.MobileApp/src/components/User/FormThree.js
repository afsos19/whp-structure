import React from 'react';
import { Alert } from 'react-native';
import styled from 'styled-components/native';
import { Formik } from 'formik';
import * as yup from 'yup';
import CheckBox from 'react-native-check-box';
import InputBorder from '../common/InputBorder';
import Button from '../common/Button';
import CommonText from '../common/CommonText';
import theme from '../../theme';
import ROUTENAMES from '../../navigation/routeName';
import { pdfFirstAccess } from '../../utils/urls';

const validationSchema = yup.object().shape({
    password: yup
        .string()
        .min(8, 'Senha deve ter pelo menos 8 caracteres')
        .matches(/^(?=.*?[A-Z])(?=(.*[a-z]){1,})(?=(.*[\d]){1,})(?=(.*[\W]){1,})(?!.*\s).{8,}$/, {
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

const validationSchemaUpdate = yup.object().shape({
    lastPassword: yup.string().required('Senha é obrigatório'),
    password: yup
        .string()
        .min(8, 'Senha deve ter pelo menos 8 caracteres')
        .matches(/^(?=.*?[A-Z])(?=(.*[a-z]){1,})(?=(.*[\d]){1,})(?=(.*[\W]){1,})(?!.*\s).{8,}$/, {
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

const WrapperText = styled.View`
    align-self: center;
    width: 90%;
`;

const DownloadText = styled(CommonText)`
    text-decoration-style: solid;
    text-decoration-line: underline;
`;

const ContainerCheckBox = styled.View`
    width: 50%;
    flex-direction: row;
    align-self: center;
    align-items: center;
    justify-content: center;
`;

const ContainerButton = styled.View`
    flex-direction: row;
    align-self: center;
    align-items: center;
    justify-content: center;
`;

const CustomCheckBox = styled(CheckBox)`
    padding: 10px;
`;

class FormThree extends React.Component {
    state = {
        isChecked: false,
    };

    render() {
        const { isChecked } = this.state;
        const { submit, back, isUpdate, isLoading = false, navigation, pdfName = "" } = this.props;
        return (
            <Formik
                initialValues={{
                    lastPassword: '',
                    password: '',
                    confirmPassword: '',
                }}
                onSubmit={v => submit({ form: v })}
                validationSchema={isUpdate ? validationSchemaUpdate : validationSchema}
            >
                {({ values, handleChange, errors, isValid, handleSubmit }) => (
                    <React.Fragment>
                        <WrapperText>
                            <CommonText type="h5" color="dark">
                                {isUpdate ? "Alterar senha"
                                : "Crie uma senha"}                                
                            </CommonText>
                            <CommonText type="small" color="dark">
                                Preencha todos os campos com (*)
                            </CommonText>
                        </WrapperText>
                        {isUpdate && (
                            <InputBorder
                                value={values.lastPassword}
                                secureTextEntry
                                onChange={handleChange('lastPassword')}
                                placeholder="Senha Atual*:"
                                width="90%"
                                errorMessage={errors.lastPassword}
                                inputRef={ref => this.lastPassword = ref}
                                onSubmitEditing={() => this.passwordInput.focus()}
                            />
                        )}
                        <InputBorder
                            value={values.password}
                            secureTextEntry
                            onChange={handleChange('password')}
                            placeholder="Senha*:"
                            width="90%"
                            errorMessage={errors.password}
                            inputRef={ref => this.passwordInput = ref}
                            onSubmitEditing={() => this.confirmPassword.focus()}
                        />
                        <InputBorder
                            value={values.confirmPassword}
                            secureTextEntry
                            onChange={handleChange('confirmPassword')}
                            placeholder="Confirmar Senha*:"
                            width="90%"
                            errorMessage={errors.confirmPassword}
                            returnKeyType='done'
                            inputRef={ref => this.confirmPassword = ref}
                        />
                        <CommonText type="small" color="dark" center marginTop={20}>
                            A senha deve ter pelo menos 8 caracteres, combinando letras maiúsculas,
                            letras minúsculas, números e caracteres especiais.
                        </CommonText>
                        {!isUpdate && (
                            <React.Fragment>
                                <CommonText type="normal" center marginTop={20}>
                                    
                                    <DownloadText
                                        onPress={() => navigation.navigate(ROUTENAMES.PDF_SCREEN, { source: pdfFirstAccess(pdfName) })}
                                        //network.data[0].network.siteShortName.replace(/\s/g, '')
                                    >
                                        Clique aqui{' '}
                                    </DownloadText>
                                    e baixe e leia o regulamento completo.
                                </CommonText>
                                <ContainerCheckBox>
                                    <CustomCheckBox
                                        checkBoxColor={theme.colors.borderLight}
                                        checkedCheckBoxColor={theme.colors.borderLight}
                                        onClick={() => this.setState({ isChecked: !isChecked })}
                                        isChecked={isChecked}
                                    />
                                    <CommonText>Li e aceito os termos</CommonText>
                                </ContainerCheckBox>
                            </React.Fragment>
                        )}
                        <ContainerButton>
                            <Button
                                color="secondary"
                                onPress={() => back()}
                                text="Voltar"
                                marginTop={20}
                                center
                            />
                            {isUpdate ? (
                                <Button
                                    color="primary"
                                    isLoading={isLoading}
                                    onPress={isValid ? handleSubmit : () =>  Alert.alert('Preenchimento obrigatório', 'Campos com * são de preenchimento obrigatório') }
                                    // disabled={!isValid}
                                    text="Salvar"
                                    marginTop={20}
                                    center
                                />
                            ) : (
                                <Button
                                    color="primary"
                                    onPress={handleSubmit}
                                    disabled={!isValid || !isChecked}
                                    text="Finalizar"
                                    marginTop={20}
                                    center
                                />
                            )}
                        </ContainerButton>
                    </React.Fragment>
                )}
            </Formik>
        );
    }
}

export default FormThree;
