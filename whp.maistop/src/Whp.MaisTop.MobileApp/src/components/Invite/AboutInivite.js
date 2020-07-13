import React from 'react';
import styled from 'styled-components/native';
import Icon from 'react-native-vector-icons/Ionicons';
import jwtDecode from 'jwt-decode';
import { AsyncStorage, Alert, ActivityIndicator } from 'react-native';
import { connect } from 'react-redux';
import { getShopAction, getAcademyAction, authReset } from '../../redux/actions/auth';
import CommonText from '../common/CommonText';
import Logo from '../common/Logo';
import images from '../../res/images';
import ROUTENAMES from '../../navigation/routeName';
import openBrowser from '../../utils/OpenLink';
import Card from '../common/Card';

const Modal = styled.Modal.attrs({
    animationType: 'slide',
    transparent: true,
    visible: p => p.visible,
})``;

const Wrapper = styled.View`
    align-items: center;
    justify-content: center;
    width: 100%;
    height: 100%;
    padding-top: 45;
    padding-bottom: 15;
    padding-horizontal: 20;
    background-color: ${p => p.theme.colors.modalTransparent};
`;

const WrapperBottom = styled.View`
    align-self: center;
    align-items: center;
    justify-content: center;
    flex-direction: row;
`;

const WrapperTouchNext = styled.TouchableOpacity`
    width: 50%;
    align-self: flex-end;
    margin-top: 15;
`;

const WrapperTouchPrevious = styled.TouchableOpacity`
    width: 50%;
    align-self: flex-start;
    margin-top: 15;
`;

const AboutImage = styled.Image`
    align-self: center;
    width: 75;
    height: 75;
`;

const WrapperText = styled.View`
    align-self: center;
    width: 90%;
`;

const WrapperCard = styled.View`
    align-self: center;
    align-items: center;
    width: 80%;
`;

const WrapperClose = styled.TouchableOpacity`
    align-self: flex-end;
`;

const CloseIcon = styled(Icon).attrs({
    name: 'ios-close',
    size: 25,
    color: p => p.theme.colors.iconLight,
})`
    align-self: flex-end;
`;

class AboutInivite extends React.Component {
    state = {
        card: 1,
    };

    renderCard1 = () => {
        return (
            <WrapperCard>
                <AboutImage source={images.aboutInvite1} resizeMode="contain"/>
                <CommonText type="h5" color="primary" marginTop={20} center>QUEM PODE INDICAR?</CommonText>
                <CommonText type="normal" color="dark" marginTop={10} center>Vendedores, Gerentes de Loja e Gerentes Regionais de lojas e redes cadastradas no +TOP.</CommonText>
                <CommonText type="h5" color="primary" marginTop={20} center>QUEM PODE SER INDICADO?</CommonText>
                <CommonText type="normal" color="dark" marginTop={10} center>Vendedores, Gerentes de Loja e Gerentes Regionais de revendas participantes que ainda não são cadastrados.</CommonText>
            </WrapperCard>
        )
    }

    renderCard2 = () => {
        return (
            <WrapperCard>
                <AboutImage source={images.aboutInvite2} resizeMode="contain"/>
                <CommonText type="h5" color="primary" marginTop={20} center>COMO FUNCIONA?</CommonText>
                <CommonText type="normal" color="dark" marginTop={10} center>Para indicar basta colocar o telefone com DDD de quem quiser indicar e clicar em enviar. Pronto! Seu amigo irá receber um SMS com o convite e o seu código para que você ganhe os pontos pela indicação.</CommonText>
                <CommonText type="normal" color="dark" marginTop={10} center>Se preferir, você também pode copiar o texto de convite com o seu código e enviar para o seu amigo via <CommonText type="normal" color="secondary">WHATSAPP</CommonText> ou E-MAIL. </CommonText>
            </WrapperCard>
        )
    }

    renderCard3 = () => {
        return (
            <WrapperCard>
                <AboutImage source={images.aboutInvite3} resizeMode="contain"/>
                <CommonText type="h5" color="primary" marginTop={20} center>COMO EU GANHO PONTOS?</CommonText>
                <CommonText type="normal" color="dark" marginTop={10} center>Os pontos irão aparecer na sua conta no mês seguinte, quando o cadastro do seu amigo for efetivado com sucesso e o Gestor da Informação acrescentar o seu indicado no Arquivo de Funcionários.</CommonText>
                <CommonText type="normal" color="dark" marginTop={10} center>Cadastro aprovado, <CommonText type="normal" color="primary" bold>10</CommonText> pontos para você <CommonText type="normal" color="primary" bold>+ 5</CommonText> pontos para o seu amigo indicado.</CommonText>
            </WrapperCard>
        )
    }

    renderCard4 = () => {
        return (
            <WrapperCard>
                <AboutImage source={images.aboutInvite4} resizeMode="contain"/>
                <CommonText type="h5" color="primary" marginTop={20} center>COMO EU POSSO VER SE DEU CERTO?</CommonText>
                <CommonText type="normal" color="dark" marginTop={10} center>
                    Na sua seção de indicações do site, você irá encontrar um painel de acompanhamento com todas as informações sobre todos os amigos indicados, se o cadastro está em processo, se a indicação não deu certo, ou, se o cadastro foi confirmado com sucesso.
                </CommonText>
            </WrapperCard>
        )
    }

    render() {
        const { card } = this.state;
        const { visible = false, close, navigation, auth, getShop, getAcademy } = this.props;

        return (
            <Modal visible={visible}>
                <Wrapper>
                    <WrapperClose onPress={close}>
                        <CloseIcon />
                    </WrapperClose>
                    <WrapperText>
                        <CommonText type="normal" color="light" bold>Conheça as regras:</CommonText>
                    </WrapperText>
                    <Card width="90%" marginTop={15}>
                        {card === 1 ? this.renderCard1() : 
                            card === 2 ? this.renderCard2() :
                            card === 3 ? this.renderCard3() :
                            this.renderCard4()
                         }
                    </Card>
                    <WrapperBottom>
                        <WrapperTouchPrevious onPress={() => this.setState({ card: card - 1})}>
                            {card > 1 &&
                                <CommonText type="normal" color="light" bold center>{'< '}Anterior</CommonText>
                            }
                        </WrapperTouchPrevious>
                        <WrapperTouchNext onPress={() => this.setState({ card: card + 1})}>
                            {card < 4 &&
                                <CommonText type="normal" color="light" bold center>Próximo{' >'}</CommonText>
                            }                        
                        </WrapperTouchNext>
                    </WrapperBottom>
                </Wrapper>
            </Modal>
        );
    }
}

export default AboutInivite;
