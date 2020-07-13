import * as React from 'react';
import styled from 'styled-components/native';
import { AsyncStorage, Platform } from 'react-native';
import CommonText from '../../components/common/CommonText';
import GradientBackground from '../../components/common/GradientBackground';
import Card from '../../components/common/Card';
import Logo from '../../components/common/Logo';
import Button from '../../components/common/Button';
import Tab from '../../components/common/Tab';
import MenuModal from '../../components/Home/MenuModal';
import images from '../../res/images';
import ROUTENAMES from '../../navigation/routeName';


const Container = styled(GradientBackground)`
    flex: 1;
    align-items: center;
`;

const Scroll = styled.ScrollView.attrs({
    showsVerticalScrollIndicator: false
})`
    width: 100%;
`;

class Unauthorized extends React.Component {
    static navigationOptions = {
        header: null,
    };

    render() {
        const { navigation } = this.props;
        return (
            <Container >
                <Scroll ref="scrollView">
                    <Logo height="30px" img={images.logoClean} marginTop={Platform.OS === 'ios' ? 32 : 32 }/>
                    <Card marginBottom={40} width="90%" marginTop={10}>
                        <CommonText type="h3" color="primary" center>Sessão expirada</CommonText>
                        <CommonText type="normal" color="dark" marginTop={16} center>Desculpe, sua sessão expirou. Realize o Login novamente</CommonText>
                        <Button marginTop={16} text="Entrar" onPress={() => {
                            AsyncStorage.clear();
                            navigation.navigate(ROUTENAMES.LOGIN);
                        }}/>
                    </Card>
                </Scroll>
            </Container>
        );
    }
}

export default Unauthorized;
