import React from 'react';
import styled from 'styled-components/native';
import LinearGradient from 'react-native-linear-gradient';
import CommonText from '../common/CommonText';
import images from '../../res/images';
import ROUTENAMES from '../../navigation/routeName';

// const width = Dimensions.get('window').width;

const Wrapper = styled.TouchableOpacity`
    align-self: center;
    flex-direction: row;
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
    width: 90%;
    height: 115;
    margin-top: 40;
    background-color: ${p => p.theme.colors.secondary};
    border-radius: 6;
`;

const ImageSpreadsheet = styled.Image`
    align-self: center;
    width: 45;
    height: 54;
`;

const WrapperIcon = styled(LinearGradient).attrs({
    colors: p => [p.theme.colors.secondary, p.theme.colors.gradientSecondary],
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

const SpreadsheetButton = ({ onPress }) => {
    return (
        <Wrapper onPress={onPress}>
            <WrapperIcon>
                <ImageSpreadsheet source={images.spreadsheet} resizeMode="contain" />
            </WrapperIcon>
            <WrapperText>
                <CommonText type="normal" color="light" center bold>
                    PUBLICAR PLANILHAS
                </CommonText>
            </WrapperText>
        </Wrapper>
    );
};

export default SpreadsheetButton;
