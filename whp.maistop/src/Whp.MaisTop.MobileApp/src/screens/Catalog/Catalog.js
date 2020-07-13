import * as React from 'react';
import { TouchableOpacity } from 'react-native'
import styled from 'styled-components/native';
import { SafeAreaView } from 'react-navigation';
import GradientBackground from '../../components/common/GradientBackground';
import CommonText from '../../components/common/CommonText';
import LinearGradient from 'react-native-linear-gradient';
import Logo from '../../components/common/Logo';
import images from '../../res/images';
import Icon from 'react-native-vector-icons/Ionicons';

const BackIcon = styled(Icon).attrs({
    name: 'ios-arrow-round-back',
    size: 25,
    color: p => p.theme.colors.iconDark,
})`
    align-self: center;
    margin-right: 8px;
`;

const Container = styled(LinearGradient).attrs({
    colors: p => ['#EEE', p.theme.colors.gradientDarkEnd],
})`
    flex: 1;
    background-color: #FEFEFE;
`;

const ContainerAbsolute = styled.View`
    position: absolute;
    width: 100%;
    height: 100%;
`;

const ShadowTop = styled.View`
    flex: 1;
    flex-direction: row;
    justify-content:space-around;
`;

const ShadowBottom = styled.View`
    flex: 5;
    flex-direction: row;
    justify-content:space-around;
`;

const Top = styled.View`
    align-items: center;
    justify-content: center;
    flex: 4;
`;

const Bottom = styled.View`
    flex: 8;
    align-items: center;
    justify-content: center;
`;

const ShadowLight = styled.View`
    width: 50;
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
`;

const ShadowDark = styled.View`
    width: 50;
    height: auto;
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 0.1;
    background-color: rgba(255, 255, 255, 0.2);
`;

const ContainerLinearBack = styled.View`
    flex: 4;
    align-items: center;
    justify-content: center;
    margin-bottom: 64px;
`;

const OrangeBackground = styled(LinearGradient).attrs({
    colors: p => [p.theme.colors.gradientPrimaryInitial, p.theme.colors.gradientPrimaryEnd],
})`
    align-self: flex-start;
    margin-left: 24;
    width: 150;
    height: 150;
`;

const GreenBackground = styled(LinearGradient).attrs({
    colors: p => [p.theme.colors.secondary, p.theme.colors.gradientSecondary],
})`
    align-self: flex-end;
    margin-right: 24;
    margin-bottom: 16;
    width: 150;
    height: 150;
`;

const CardLogo = styled.View`
    margin-top: 16px;
    width: 50%;
    padding: 20px;
    align-items: center;
    justify-content: center;
    align-self: center;
    background-color: ${p => p.theme.colors.background};
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
`;

const CardBody = styled.View`
    margin-bottom: 40px;
    padding: 32px 16px;
    width: 80%;
    align-self: center;
    justify-content: center;
    background-color: ${p => p.theme.colors.background};
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
`;


const Subtitle = styled(CommonText)`
    color: #AEA697;
    font-family: ${p => p.theme.fontFamily.bold};
`;

const TextWrapper = styled.View`
    width: 70%;
`;

const TextRow = styled.View`
    flex-direction: row;
    margin-top: 8px;
`;

const Line = styled.View`
    width: 100%;
    height: 1;
    background-color: #000;
    margin-top: 8;
`;

const ImageContainer = styled.View`
    flex: 1;
    flex-direction: row;
    align-self: flex-end;
    align-items: flex-end;
    justify-content: flex-end;
    elevation: 2;
`;

const BottomImage = styled.Image`
    height: 220;
    width: 180;
`;

class Catalog extends React.Component {
    static navigationOptions = {
        header: null,
    };

    render() {
        const { navigation } = this.props;

        return (
            <Container>
                <ContainerAbsolute>
                    <ShadowTop>
                        <ShadowLight />
                        <ShadowLight />
                        <ShadowLight />
                    </ShadowTop>
                    <ShadowBottom>
                        <ShadowDark />
                        <ShadowDark />
                        <ShadowDark />
                    </ShadowBottom>
                </ContainerAbsolute>
                <ContainerAbsolute>
                    <Top />
                    <ContainerLinearBack>
                        <GreenBackground />
                        <OrangeBackground />
                    </ContainerLinearBack>
                    <ImageContainer />                    
                </ContainerAbsolute>
                <SafeAreaView />
                <Top>
                    <CardLogo>
                        <Logo height="50px" img={images.logo}/>
                    </CardLogo>
                </Top>

                <Bottom>    
                    <CardBody>
                        <TextWrapper>
                            <Subtitle type="h5">Prepare-se!</Subtitle>
                            <CommonText type="h5" color="dark" marginTop={6}>Tem notícia boa chegando por aí.</CommonText>
                            <Line />
                            <CommonText type="h5" color="dark" marginTop={6}>O +TOP vai voltar ainda melhor.</CommonText>
                            <CommonText type="h5" color="dark">E com muito mais vantagens para você.</CommonText>
                            <CommonText type="h5" color="dark" marginTop={6} bold>Aguarde.</CommonText>
                            <Line />
                            <CommonText type="normal" color="dark" marginTop={6}>Para informações sobre pedidos em andamento, entre em contato com a gente pelo:</CommonText>
                            <CommonText type="normal" color="secondary" marginTop={6} bold>0800 780 0606</CommonText>
                            <TouchableOpacity onPress={() => navigation.goBack()}>
                                <TextRow>
                                    <BackIcon />
                                    <CommonText type="small" color="dark" center>VOLTAR</CommonText>
                                </TextRow>
                            </TouchableOpacity>
                        </TextWrapper>
                    </CardBody>
                </Bottom>
                <ImageContainer>
                    <BottomImage source={images.manTablet} resizeMode="contain" />
                </ImageContainer>
            </Container>
        );
    }
}

export default Catalog;
