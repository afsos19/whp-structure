import 'moment';
import moment from 'moment-timezone';
import 'moment/locale/pt-br';
import React from 'react';
import styled from 'styled-components/native';
import { Alert } from 'react-native';
import { Formik } from 'formik';
import * as yup from 'yup';
import InputBorder from '../common/InputBorder';
import InputDate from '../common/InputDate';
import InputOptions from '../common/InputOptions';
import Button from '../common/Button';
import Photo from './Photo';
import CommonText from '../common/CommonText';
import teams from '../../utils/teams';
import { imgUserPath } from '../../utils/urls';

const validationSchema = yup.object().shape({
    email: yup
        .string()
        .email('E-mail inválido')
        .required('E-mail é obrigatório'),
    confirmEmail: yup
        .string()
        .email('E-mail inválido')
        // .oneOf([yup.ref('email', null)], 'Os e-mails devem ser iguais')
        .required('Confirmar E-mail é obrigatório'),
    birthDate: yup.string().required('Data de nascimento é obrigatório'),
    mobilePhone: yup
        .string()
        .min(11, 'Numero inválido')
        .required('Celular é obrigatório'),
    phone: yup
        .string()
        .min(11, 'Numero inválido')
        .required('Celular é obrigatório'),
    gender: yup.string().required('Campo Sexo é obrigatório'),
    children: yup.string().required('Filhos é obrigatório'),
    civilStatus: yup.string().required('Estado civil é obrigatório'),
});

const genders = [
    {
        label: 'Masculino',
        value: 'M',
    },
    {
        label: 'Feminino',
        value: 'F',
    },
];

const childrens = [
    {
        label: '0',
        value: '0',
    },
    {
        label: '1',
        value: '1',
    },
    {
        label: '2',
        value: '2',
    },
    {
        label: '3',
        value: '3',
    },
    {
        label: '4',
        value: '4',
    },
    {
        label: '5',
        value: '5',
    },
    {
        label: '6 ou mais',
        value: '6',
    },
];

const civilStatus = [
    {
        label: 'Solteiro(a)',
        value: 'S',
    },
    {
        label: 'Casado(a)',
        value: 'C',
    },
    {
        label: 'Viúvo(a)',
        value: 'V',
    },
];

const WrapperText = styled.View`
    align-self: center;
    width: 90%;
`;

class FormOne extends React.Component {
    state = {
        image: '',
    };

    componentDidMount() {
        const { defaultValues = false, user, backForm = null } = this.props;
        if (defaultValues) {
            this.setState({ image: imgUserPath + user.photo });
            this.refs.formik.setFieldValue('email', user.email);
            this.refs.formik.setFieldValue('confirmEmail', user.email);
            this.refs.formik.setFieldValue('mobilePhone', user.cellPhone);
            this.refs.formik.setFieldValue('phone', user.phone);
            this.refs.formik.setFieldValue('birthDate', user.bithDate);
            this.refs.formik.setFieldValue('gender', user.gender);
            this.refs.formik.setFieldValue('team', user.heartTeam);
            this.refs.formik.setFieldValue('children', user.sonAmout.toString());
            this.refs.formik.setFieldValue('civilStatus', user.civilStatus);
        }
        if (backForm) {
            this.refs.formik.setFieldValue('email', backForm.form.email);
            this.refs.formik.setFieldValue('confirmEmail', backForm.form.confirmEmail);
            this.refs.formik.setFieldValue('mobilePhone', backForm.form.mobilePhone);
            this.refs.formik.setFieldValue('phone', backForm.form.phone);
            this.refs.formik.setFieldValue('birthDate', backForm.form.birthDate);
            this.refs.formik.setFieldValue('gender', backForm.form.gender);
            this.refs.formik.setFieldValue('team', backForm.form.team);
            this.refs.formik.setFieldValue('children', backForm.form.children);
            this.refs.formik.setFieldValue('civilStatus', backForm.form.civilStatus);
        }
    }

    render() {
        const { image } = this.state;
        const { submit, user, shops, isUpdate = false, isLoading = false } = this.props;
        return (
            <Formik
                ref="formik"
                initialValues={{
                    email: '',
                    confirmEmail: '',
                    mobilePhone: '',
                    phone: '',
                    birthDate: '',
                    gender: '',
                    team: '',
                    children: '',
                    civilStatus: '',
                }}
                onSubmit={v => submit({ form: v, image })}
                validationSchema={validationSchema}
            >
                {({ values, handleChange, errors, isValid, handleSubmit, setFieldValue }) => (
                    <React.Fragment>
                        <Photo
                            image={image}
                            setImage={value => {
                                this.setState({ image: value });
                            }}
                        />
                        <WrapperText>
                            <CommonText type="h5" color="dark">
                                Dados Cadastrais
                            </CommonText>
                            <CommonText type="small" color="dark">
                                Preencha todos os campos com (*)
                            </CommonText>
                        </WrapperText>

                        <InputBorder
                            value={shops ? shops[0].network.name : ' '}
                            placeholder="Rede:"
                            width="90%"
                            enable={false}
                        />
                        <InputBorder
                            mask="cnpj"
                            value={shops ? shops[0].cnpj : ' '}
                            placeholder="Loja:"
                            width="90%"
                            enable={false}
                        />
                        <InputBorder
                            value={user.office ? user.office.description : ' '}
                            placeholder="Cargo:"
                            width="90%"
                            enable={false}
                        />
                        <InputBorder value={user ? user.name : ' '} placeholder="Nome:" width="90%" enable={false} />
                        <InputBorder
                            value={values.email}
                            onChange={handleChange('email')}
                            placeholder="Email*:"
                            width="90%"
                            keyboardType="email-address"
                            errorMessage={errors.email}
                            inputRef={ref => this.emailInput = ref}
                            onSubmitEditing={() => this.confirmEmailInput.focus()}
                        />
                        <InputBorder
                            value={values.confirmEmail}
                            onChange={handleChange('confirmEmail')}
                            placeholder="confirmar e-mail*:"
                            width="90%"
                            keyboardType="email-address"
                            errorMessage={errors.confirmEmail}
                            inputRef={ref => this.confirmEmailInput = ref}
                            onSubmitEditing={() => this.mobileInput._inputElement.focus()}
                        />
                        <InputBorder
                            value={values.mobilePhone}
                            mask="cel-phone"
                            onChange={handleChange('mobilePhone')}
                            placeholder="Celular (com DDD)*:"
                            width="90%"
                            errorMessage={errors.mobilePhone}
                            inputRef={ref => this.mobileInput = ref}
                            returnKeyType='done'
                            onSubmitEditing={() => this.phoneInput._inputElement.focus()}
                        />
                        <InputBorder
                            value={values.phone}
                            mask="cel-phone"
                            onChange={handleChange('phone')}
                            placeholder="Telefone (com DDD)*:"
                            width="90%"
                            errorMessage={errors.phone}
                            inputRef={ref => this.phoneInput = ref}
                            returnKeyType='done'
                        />
                        <InputDate
                            initialValue={user.bithDate}
                            placeholder="Data de nascimento*:"
                            width="90%"
                            setField={setFieldValue}
                            fieldName="birthDate"
                            errorMessage={errors.birthDate}
                        />
                        <InputOptions
                            placeholder="Sexo*:"
                            width="90%"
                            onChange={handleChange('gender')}
                            value={values.gender}
                            items={genders}
                            errorMessage={errors.gender}
                        />
                        <InputOptions
                            placeholder="Time do coração:"
                            width="90%"
                            onChange={handleChange('team')}
                            value={values.team}
                            items={teams}
                        />
                        <InputOptions
                            placeholder="Filhos*:"
                            width="90%"
                            onChange={handleChange('children')}
                            value={values.children}
                            items={childrens}
                            errorMessage={errors.children}
                        />
                        <InputOptions
                            placeholder="Estado Civil*:"
                            width="90%"
                            onChange={handleChange('civilStatus')}
                            value={values.civilStatus}
                            items={civilStatus}
                            errorMessage={errors.civilStatus}
                        />

                        <Button
                            color="primary"
                            onPress={isValid ? handleSubmit : () =>  Alert.alert('Preenchimento obrigatório', 'Campos com * são de preenchimento obrigatório') }
                            isLoading={isLoading}
                            // disabled={!isValid}
                            text={isUpdate ? 'Salvar' : 'Próximo'}
                            marginTop={20}
                            center
                        />
                    </React.Fragment>
                )}
            </Formik>
        );
    }
}

export default FormOne;
