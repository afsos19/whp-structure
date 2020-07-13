import React from 'react';
import styled from 'styled-components/native';
import Icon from 'react-native-vector-icons/SimpleLineIcons';
import CommonText from '../common/CommonText';

const Wrapper = styled.View`
    align-self: center;
    flex-direction: row;
    width: 100%;
    justify-content: center;
    align-items: flex-start;
    padding-horizontal: 16;
    padding-vertical: 16;
`;

const WrapperBadge = styled.TouchableOpacity`
    justify-content: center;
    align-items: center;
    width: 65;
`;

const Badge = styled.View`
    align-items: center;
    justify-content: center;
    width: 50;
    height: 50;
    border-radius: 25;
    background-color: ${p => (p.selected ? p.theme.colors.primary : p.theme.colors.background)};
    border-width: 1;
    border-color: ${p => (p.selected ? p.theme.colors.primary : p.theme.colors.borderLight)};
    margin-bottom: 5;
`;

const CustomIcon = styled(Icon).attrs({
    size: 22,
    color: p => (p.selected ? p.theme.colors.iconLight : p.theme.colors.iconDark),
})`
    align-self: center;
`;

const Line = styled.View`
    margin-top: 30;
    width: 50;
    height: 1;
    background-color: ${p => p.theme.colors.borderLight};
`;

const Steps = ({ step, setStep = null }) => {
    return (
        <Wrapper>
            <WrapperBadge onPress={() => setStep && setStep(1)}>
                <Badge selected={step === 1}>
                    <CustomIcon name="user" selected={step === 1} />
                </Badge>
                <CommonText type="small" color="dark" center>
                    Dados Cadastrais
                </CommonText>
            </WrapperBadge>
            <Line />
            <WrapperBadge onPress={() => setStep && setStep(2)}>
                <Badge selected={step === 2}>
                    <CustomIcon name="home" selected={step === 2} />
                </Badge>
                <CommonText type="small" color="dark" center>
                    Endereço
                </CommonText>
            </WrapperBadge>
            <Line />
            <WrapperBadge onPress={() => setStep && setStep(3)}>
                <Badge selected={step === 3}>
                    <CustomIcon name="lock" selected={step === 3} />
                </Badge>
                <CommonText type="small" color="dark" center>
                    Senha
                </CommonText>
            </WrapperBadge>
        </Wrapper>
    );
};

export default Steps;
