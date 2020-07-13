import React from 'react';
import styled from 'styled-components/native';
import LinearGradient from 'react-native-linear-gradient';
import CommonText from '../common/CommonText';
import images from '../../res/images';
import ROUTENAMES from '../../navigation/routeName';

// const width = Dimensions.get('window').width;

const Wrapper = styled.View`
    margin-top: 10;
    align-self: center;
    justify-content: space-around;
    flex-direction: row;
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
    width: 90%;
    max-height: 300;
    padding-horizontal: 20;
    background-color: ${p => p.theme.colors.background};
    border-radius: 6;
`;

const Image = styled.Image`
    align-self: center;
    width: 45;
    height: 54;
`;

const WrapperLeft = styled.View`
    align-self: center;
    align-items: flex-start;
    justify-content: center;
    height: 100%;
    width: 40%;
`;

const VerticalLine = styled.View`
    align-self: center;
    background-color: ${p => p.theme.colors.borderMyTeam};
    height: 80%;
    width: 0.5px;
`;

const WrapperRight = styled.View`
    align-self: center;
    align-items: flex-start;
    justify-content: center;
    height: 100%;
    width: 55%;
`;

const CardStep = ({ title, number, text, image }) => {
    return (
        <Wrapper>
            <WrapperLeft>
                <CommonText type="h3" color="primary">
                    {number}
                </CommonText>
                <CommonText type="subtitle" color="dark" bold>
                    {title}
                </CommonText>
            </WrapperLeft>
            <VerticalLine />
            <WrapperRight>
                <Image source={image} resizeMode="contain" />
                {text}
            </WrapperRight>
        </Wrapper>
    );
};

export default CardStep;
