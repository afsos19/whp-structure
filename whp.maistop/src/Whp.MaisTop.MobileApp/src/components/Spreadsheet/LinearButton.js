import React from 'react';
import styled from 'styled-components/native';
import LinearGradient from 'react-native-linear-gradient';
import CommonText from '../common/CommonText';

const Wrapper = styled.TouchableOpacity`
    align-self: center;
    flex-direction: row;
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
    width: 70%;
    height: 115;
    margin-top: 40;
    background-color: ${p => p.theme.colors.background};
    border-radius: 6;
`;

const ImageSpreadsheet = styled.Image`
    align-self: center;
    width: 45;
    height: 54;
`;

const WrapperIcon = styled(LinearGradient).attrs({
    colors: p =>
        p.color === 'primary'
            ? [p.theme.colors.gradientPrimaryInitial, p.theme.colors.gradientPrimaryEnd]
            : [p.theme.colors.secondary, p.theme.colors.gradientSecondary],
})`
    border-top-left-radius: 6;
    border-bottom-left-radius: 6;
    align-self: center;
    align-items: center;
    justify-content: center;
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 2};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
    height: 100%;
    width: 30%;
`;

const WrapperText = styled.View`
    align-self: center;
    align-items: center;
    justify-content: center;
    width: 70%;
`;

const LinearButton = ({ color = 'primary', image, text, onPress }) => {
    return (
        <Wrapper onPress={onPress}>
            <WrapperIcon color={color}>
                <ImageSpreadsheet source={image} resizeMode="contain" />
            </WrapperIcon>
            <WrapperText>
                <CommonText type="normal" color="dark" center>
                    {text}
                </CommonText>
            </WrapperText>
        </Wrapper>
    );
};

export default LinearButton;
