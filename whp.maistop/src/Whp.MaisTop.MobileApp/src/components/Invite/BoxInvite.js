import React from 'react';
import styled from 'styled-components/native';
import LinearGradient from 'react-native-linear-gradient';
import CommonText from '../common/CommonText';
import images from '../../res/images';
import ROUTENAMES from '../../navigation/routeName';

// const width = Dimensions.get('window').width;

const Wrapper = styled.View`
    margin-top: 20;
    margin-bottom: 10;
    align-self: center;
    justify-content: space-around;
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
    width: 80%;
    max-height: 300;
    background-color: ${p => p.theme.colors.background};
    border-radius: 6;
`;

const WrapperTop = styled.View`
    align-self: center;
    align-items: center;
    justify-content: center;
    padding-vertical: 20;
    width: 100%;
`;

const WrapperBottom = styled(LinearGradient).attrs({
    colors: p =>
        p.color === 'primary'
            ? [p.theme.colors.gradientPrimaryInitial, p.theme.colors.gradientPrimaryEnd]
            : [p.theme.colors.secondary, p.theme.colors.gradientSecondary],
})`
    align-self: center;
    align-items: center;
    justify-content: center;
    padding-vertical: 20;
    width: 100%;
    border-bottom-left-radius: 6;
    border-bottom-right-radius: 6;
`;

const BoxInvite = ({ color, childrenTop, childrenBottom }) => {
    return (
        <Wrapper>
            <WrapperTop>{childrenTop}</WrapperTop>
            {childrenBottom && <WrapperBottom color={color}>{childrenBottom}</WrapperBottom>}
        </Wrapper>
    );
};

export default BoxInvite;
