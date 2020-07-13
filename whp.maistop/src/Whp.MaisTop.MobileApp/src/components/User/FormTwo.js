import React from 'react';
import { Alert } from 'react-native';
import axios from 'axios';
import styled from 'styled-components/native';
import { Formik } from 'formik';
import * as yup from 'yup';
import InputBorder from '../common/InputBorder';
import InputOptions from '../common/InputOptions';
import Button from '../common/Button';
import CommonText from '../common/CommonText';

const validationSchema = yup.object().shape({
    cep: yup
        .string()
        .min(9, 'CEP incompleto')
        .required('CEP é obrigatório'),
    street: yup.string().required('Rua é obrigatório'),
    number: yup.number().required('Número é obrigatório'),
    neighborhood: yup.string().required('Bairro é obrigatório'),
    city: yup.string().required('Cidade é obrigatório'),
    state: yup.string().required('UF é obrigatório'),
});

const WrapperText = styled.View`
    align-self: center;
    width: 90%;
`;

const ContainerButton = styled.View`
    flex-direction: row;
    align-self: center;
    align-items: center;
    justify-content: center;
`;

const states = [
    {
        label: 'AC',
        value: 'AC',
    },
    {
        label: 'AL',
        value: 'AL',
    },
    {
        label: 'AP',
        value: 'AP',
    },
    {
        label: 'AM',
        value: 'AM',
    },
    {
        label: 'BA',
        value: 'BA',
    },
    {
        label: 'CE',
        value: 'CE',
    },
    {
        label: 'DF',
        value: 'DF',
    },
    {
        label: 'ES',
        value: 'ES',
    },
    {
        label: 'GO',
        value: 'GO',
    },
    {
        label: 'MA',
        value: 'MA',
    },
    {
        label: 'MT',
        value: 'MT',
    },
    {
        label: 'MS',
        value: 'MS',
    },
    {
        label: 'MG',
        value: 'MG',
    },
    {
        label: 'PA',
        value: 'PA',
    },
    {
        label: 'PB',
        value: 'PB',
    },
    {
        label: 'PR',
        value: 'PR',
    },
    {
        label: 'PE',
        value: 'PE',
    },
    {
        label: 'PI',
        value: 'PI',
    },
    {
        label: 'RJ',
        value: 'RJ',
    },
    {
        label: 'RN',
        value: 'RN',
    },
    {
        label: 'RS',
        value: 'RS',
    },
    {
        label: 'RO',
        value: 'RO',
    },
    {
        label: 'RR',
        value: 'RR',
    },
    {
        label: 'SC',
        value: 'SC',
    },
    {
        label: 'SP',
        value: 'SP',
    },
    {
        label: 'SE',
        value: 'SE',
    },
    {
        label: 'TO',
        value: 'TO',
    },
];

class FormTwo extends React.Component {
    componentDidMount() {
        const { defaultValues = false, user, backForm = null } = this.props;

        if (defaultValues) {
            this.refs.formik.setFieldValue('cep', user.cep ? user.cep.substring(0, 5) + "-" + user.cep.substring(4, 7) : "");
            this.refs.formik.setFieldValue('street', user.address);
            this.refs.formik.setFieldValue('number', user.number.toString());
            this.refs.formik.setFieldValue('complement', user.complement);
            this.refs.formik.setFieldValue('neighborhood', user.neighborhood);
            this.refs.formik.setFieldValue('city', user.city);
            this.refs.formik.setFieldValue('state', user.uf);
            this.refs.formik.setFieldValue('referencePoint', user.referencePoint);
        }
        if (backForm) {
            this.refs.formik.setFieldValue('cep', backForm.form.cep);
            this.refs.formik.setFieldValue('street', backForm.form.street);
            this.refs.formik.setFieldValue('number', backForm.form.number);
            this.refs.formik.setFieldValue('complement', backForm.form.complement);
            this.refs.formik.setFieldValue('neighborhood', backForm.form.neighborhood);
            this.refs.formik.setFieldValue('city', backForm.form.city);
            this.refs.formik.setFieldValue('state', backForm.form.state);
            this.refs.formik.setFieldValue('referencePoint', backForm.form.referencePoint);
        }
    }

    handleCEP = (cep) => {
        axios.get(`https://viacep.com.br/ws/${cep}/json/`)
            .then(result => {
                this.refs.formik.setFieldValue('street', result.data.logradouro);
                this.refs.formik.setFieldValue('neighborhood', result.data.bairro);
                this.refs.formik.setFieldValue('city', result.data.localidade);
                this.refs.formik.setFieldValue('state', result.data.uf);
                this.refs.formik.setFieldValue('complement', result.data.complemento);
            })
            .catch(error => { });
    }

    render() {
        const { submit, back, isUpdate = false, isLoading = false } = this.props;

        return (
            <Formik
                ref="formik"
                initialValues={{
                    cep: '',
                    street: '',
                    number: '',
                    complement: '',
                    neighborhood: '',
                    city: '',
                    state: '',
                    referencePoint: '',
                }}
                onSubmit={v => submit({ form: v })}
                validationSchema={validationSchema}
            >
                {({ values, handleChange, errors, isValid, handleSubmit }) => (
                    <React.Fragment>
                        <WrapperText>
                            <CommonText type="h5" color="dark">
                                Endereço
                            </CommonText>
                            <CommonText type="small" color="dark">
                                Preencha todos os campos com (*)
                            </CommonText>
                        </WrapperText>

                        <InputBorder
                            value={values.cep}
                            onChange={handleChange('cep')}
                            placeholder="CEP*:"
                            width="90%"
                            mask="zip-code"
                            maxLength={9}
                            errorMessage={errors.cep}
                            inputRef={ref => this.cepInput = ref}
                            returnKeyType='done'
                            onSubmitEditing={() => {
                                this.handleCEP(values.cep)
                                this.streetInput.focus()
                            }}
                        />
                        <InputBorder
                            value={values.street}
                            onChange={handleChange('street')}
                            placeholder="RUA/AVENIDA/TRAVESSA/PRAÇA*:"
                            width="90%"
                            errorMessage={errors.street}
                            inputRef={ref => this.streetInput = ref}
                            onSubmitEditing={() => this.numberInput.focus()}
                        />
                        <InputBorder
                            value={values.number}
                            onChange={handleChange('number')}
                            placeholder="Número*:"
                            keyboardType="number-pad"
                            width="90%"
                            errorMessage={errors.number}
                            inputRef={ref => this.numberInput = ref}
                            returnKeyType='done'
                            onSubmitEditing={() => this.complementInput.focus()}
                        />
                        <InputBorder
                            value={values.complement}
                            onChange={handleChange('complement')}
                            placeholder="Complemento:"
                            width="90%"
                            errorMessage={errors.complement}
                            inputRef={ref => this.complementInput = ref}
                            onSubmitEditing={() => this.neighborhoodInput.focus()}
                        />
                        <InputBorder
                            value={values.neighborhood}
                            onChange={handleChange('neighborhood')}
                            placeholder="Bairro*:"
                            width="90%"
                            errorMessage={errors.neighborhood}
                            inputRef={ref => this.neighborhoodInput = ref}
                            onSubmitEditing={() => this.cityInput.focus()}
                        />
                        <InputBorder
                            value={values.city}
                            onChange={handleChange('city')}
                            placeholder="Cidade*:"
                            width="90%"
                            errorMessage={errors.city}
                            inputRef={ref => this.cityInput = ref}
                        />
                        <InputOptions
                            placeholder="UF*:"
                            width="90%"
                            onChange={handleChange('state')}
                            value={values.state}
                            items={states}
                            errorMessage={errors.state}
                        />
                        <InputBorder
                            value={values.referencePoint}
                            onChange={handleChange('referencePoint')}
                            placeholder="PONTO DE REFERÊNCIA:"
                            width="90%"
                            errorMessage={errors.referencePoint}
                            inputRef={ref => this.referenceInput = ref}
                            returnKeyType='done'
                            onSubmitEditing={handleSubmit}
                        />
                        <ContainerButton>
                            <Button color="secondary" onPress={() => back()} text="Voltar" marginTop={20} center />
                            <Button
                                color="primary"
                                onPress={isValid ? handleSubmit : () =>  Alert.alert('Preenchimento obrigatório', 'Campos com * são de preenchimento obrigatório') }
                                // disabled={!isValid}
                                isLoading={isLoading}
                                text={isUpdate ? 'Salvar' : 'Próximo'}
                                marginTop={20}
                                center
                            />
                        </ContainerButton>
                    </React.Fragment>
                )}
            </Formik>
        );
    }
}

export default FormTwo;
