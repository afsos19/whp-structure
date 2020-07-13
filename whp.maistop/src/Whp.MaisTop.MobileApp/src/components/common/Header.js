import React from 'react';
import styled from 'styled-components/native';
import Icon2 from 'react-native-vector-icons/Ionicons';
import Logo from './Logo';
import images from '../../res/images';
import ROUTERNAME from '../../navigation/routeName';

const Wrapper = styled.View`
    align-self: center;
    justify-content: center;
    align-items: center;
    height: 50px;
    width: 90%;
    margin-top: 10px;
`;

const WrapperBack = styled.TouchableOpacity`
    width: 100%;
    height: 30;
    position: absolute;
    top: 10;
    left: 5;
`;

const BackIcon = styled(Icon2).attrs({
    name: 'ios-arrow-back',
    size: 30,
    color: p => p.theme.colors.iconLight,
})`
    position: absolute;
    top: 0;
    left: 0;
`;

const Header = ({ navigation, color = false, safeArea = true }) => {
    return (
        <Wrapper color={color} safeArea={safeArea}>
            <WrapperBack onPress={() => navigation.navigate(ROUTERNAME.HOME)}>
                <BackIcon />
            </WrapperBack>
            <Logo height="30px" img={images.logoClean} />
        </Wrapper>
    );
};

export default Header;
