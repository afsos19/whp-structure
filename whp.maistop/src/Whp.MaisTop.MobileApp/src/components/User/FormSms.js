import React from 'react';
import styled from 'styled-components/native';
import { Formik } from 'formik';
import * as yup from 'yup';
import InputLine from '../common/InputLine';
import InputOptions from '../common/InputOptions';
import Button from '../common/Button';
import CommonText from '../common/CommonText';

const validationSchema = yup.object().shape({
    smsCode: yup.string().required('Código é obrigatório'),
});

const UnderlineText = styled(CommonText)`
    text-decoration-style: solid;
    text-decoration-line: underline;
`;

const Wrapper = styled.View`
    align-self: center;
    width: 100%;
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

const FormSms = ({ submit, reSend, isLoading, timerOn, minutes, seconds }) => {
    return (
        <Formik
            initialValues={{
                smsCode: '',
            }}
            onSubmit={v => submit({ form: v })}
            validationSchema={validationSchema}
        >
            {({ values, handleChange, errors, isValid, handleSubmit }) => (
                <Wrapper>
                    <InputLine
                        center
                        value={values.smsCode}
                        onChange={handleChange('smsCode')}
                        errorMessage={errors.smsCode}
                    />
                    <Button
                        color="secondary"
                        onPress={handleSubmit}
                        disabled={!isValid}
                        isLoading={isLoading}
                        text="Enviar"
                        marginTop={20}
                        center
                    />

                    {timerOn ?
                        <React.Fragment>
                            <Line />
                            <WrapperCoutdown>
                                <CommonText>Reenviar código</CommonText>
                                <CommonText>{minutes}:{seconds}</CommonText>
                            </WrapperCoutdown>
                            <Line />
                        </React.Fragment>
                        : 
                        <UnderlineText type="small" center marginTop={20} onPress={() => reSend()}>
                            REENVIAR CÓDIGO
                        </UnderlineText>
                    }
                </Wrapper>
            )}
        </Formik>
    );
};

export default FormSms;
