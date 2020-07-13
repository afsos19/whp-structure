import React from 'react';
import styled from 'styled-components/native';
import { Dimensions } from 'react-native';
import CommonText from '../common/CommonText';
import images from '../../res/images';
import ROUTENAMES from '../../navigation/routeName';

const width = Dimensions.get('window').width;

const Wrapper = styled.View`
    flex-direction: row;
    margin-top: ${p => (!p.noMargin ? 35 : 0)};
    align-self: center;
    align-items: center;
    justify-content: center;
    width: ${width * 0.95};
    background-color: ${p => p.theme.colors.productsBackground};
    height: 130;
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
`;

const FaqImage = styled.Image`
    align-self: center;
    width: 93;
    height: 100;
`;

const TextWrapper = styled.TouchableOpacity`
    align-self: center;
    width: 50%;
    flex-direction: column;
`;

const FaqCall = ({ navigation, noMargin = false }) => {
    return (
        <Wrapper noMargin={noMargin}>
            <FaqImage source={images.faq} resizeMode="contain" />
            <TextWrapper onPress={() => navigation.navigate(ROUTENAMES.FAQ)}>
                <CommonText type="subtitle" color="primary" center>
                    Clique aqui
                </CommonText>

                <CommonText type="normal" color="light" marginTop={5} center bold>
                    E veja as dúvidas frequentes sobre o programa +TOP
                </CommonText>
            </TextWrapper>
        </Wrapper>
    );
};

export default FaqCall;
