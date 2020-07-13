import * as React from 'react';
import { Clipboard } from 'react-native';
import styled from 'styled-components/native';
import { connect } from 'react-redux';
import { SafeAreaView } from 'react-navigation';
import GradientBackground from '../../components/common/GradientBackground';
import CommonText from '../../components/common/CommonText';
import Tab from '../../components/common/Tab';
import Header from '../../components/common/Header';
import MenuModal from '../../components/Home/MenuModal';
import Card from '../../components/common/Card';
import Button from '../../components/common/Button';
import ROUTENAMES from '../../navigation/routeName';
import images from '../../res/images';
import BoxInvite from '../../components/Invite/BoxInvite';
import InputBorder from '../../components/common/InputBorder';
import AboutInivite from '../../components/Invite/AboutInivite';
import { mgmSmsAction, mgmReset } from '../../redux/actions/mgm';
import Warning from '../../components/common/Warning';

const Container = styled(GradientBackground)`
    flex: 1;
`;

const Scroll = styled.ScrollView.attrs({
    showsVerticalScrollIndicator: false
})`
    width: 100%;
`;

const ButtonInvite = styled.TouchableOpacity`
    margin-top: 15;
    align-self: center;
    flex-direction: row;
    align-items: center;
    justify-content: center;
    width: 60%;
    height: 56;
    border-radius: 28;
    background-color: ${p => p.theme.colors.background};
    border-width: 1;
    border-color: ${p => p.theme.colors.border};
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
`;

const InviteImage = styled.Image`
    align-self: center;
    width: 30;
    height: 30;
    margin-right: 10;
`;

const TextBowWrapper = styled.View`
    align-self: center;
    width: 80%;
`;

const WrapperButtonRight = styled.View`
    align-self: flex-end;
    margin-right: 30;
    margin-bottom: 20;
`;

class Invite extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        menuVisible: false,
        aboutVisible: false,
        phone: '',
        warningVisible: false,
        warningType: 'error',
        warningTitle: '',
        warningMessage: '',
    };

    handleSms = () => {
        const { sendSmsInvite } = this.props;
        const { phone } = this.state;
        if (phone && phone.length >= 14) {
            sendSmsInvite({  cellphone: phone })
        }

    }

    componentDidUpdate(nextProps) {
        const { reset, mgm } = this.props;
        const { phone } = this.state;
        if (mgm.success && mgm.success !== nextProps.mgm.success) {
            this.setState({
                warningVisible: true,
                warningType: 'success',
                warningTitle: 'SUCESSO!',
                warningMessage: `Mensagem enviada para o numero: ${phone}`,
            });
            this.setState({ phone: "" })
            reset();
            // profileReset();
            // navigation.navigate(ROUTENAMES.HOME)
        } else if (mgm.error && mgm.error !== nextProps.mgm.error) {
            this.setState({
                warningVisible: true,
                warningType: 'error',
                warningTitle: 'ERRO!',
                warningMessage: mgm.errorMessage,
            });
            reset();
        }
    }

    render() {
        const { menuVisible, aboutVisible, phone, warningVisible, warningType, warningTitle, warningMessage } = this.state;
        const { navigation, mgm } = this.props;

        return (
            <Container>
                <SafeAreaView />
                <Warning
                    type={warningType}
                    title={warningTitle}
                    text={warningMessage}
                    visible={warningVisible}
                    close={() => this.setState({ warningVisible: false })}
                />
                <MenuModal
                    navigation={navigation}
                    visible={menuVisible}
                    close={() => this.setState({ menuVisible: false })}
                />
                <AboutInivite
                    navigation={navigation}
                    visible={aboutVisible}
                    close={() => this.setState({ aboutVisible: false })}
                />
                <Header navigation={navigation} />
                <Scroll>
                    <Card marginBottom={40} width="90%" marginTop={10} noPadding>
                        <TextBowWrapper>
                            <CommonText type="h3" color="primary" center>
                                Convide amigos e ganhe.
                            </CommonText>
                            <CommonText type="normal" color="dark" marginTop={5} center>
                                São 10 pontos para quem indica e 5 pontos para quem também quer ser
                                +TOP!
                            </CommonText>
                        </TextBowWrapper>
                        <ButtonInvite onPress={() => this.setState({ aboutVisible: true })}>
                            <CommonText type="normal" color="dark" bold center>
                                CONHEÇA AS REGRAS
                            </CommonText>
                        </ButtonInvite>
                        <ButtonInvite onPress={() => navigation.navigate(ROUTENAMES.MY_INVITES)}>
                            <InviteImage source={images.inviteIcon} resizeMode="contain" />
                            <CommonText type="normal" color="dark" bold center>
                                MEUS AMIGOS
                            </CommonText>
                        </ButtonInvite>
                        <BoxInvite
                            color="primary"
                            childrenTop={
                                <TextBowWrapper>
                                    <CommonText type="normal" color="dark" center>
                                        Para indicar basta colocar o telefone com DDD de quem quiser
                                        indicar e clicar em enviar. Pronto! Seu amigo irá receber um
                                        SMS com o convite e o seu código para que você ganhe os
                                        pontos pela indicação.
                                    </CommonText>
                                </TextBowWrapper>
                            }
                            childrenBottom={
                                <TextBowWrapper>
                                    <CommonText type="normal" color="light" center>
                                        A indicação só será válida para amigos que foram indicados e
                                        ainda não fazem parte do programa.
                                    </CommonText>
                                </TextBowWrapper>
                            }
                        />
                        <BoxInvite
                            color="secondary"
                            childrenTop={
                                <TextBowWrapper>
                                    <CommonText type="normal" color="dark" center>
                                        Digite aqui o celular do seu amigo
                                    </CommonText>
                                    <InputBorder
                                        value={phone}
                                        mask="cel-phone"
                                        onChange={val => this.setState({ phone: val })}
                                        placeholder="Celular (com DDD)*:"
                                        width="70%"
                                        maxLength={15}
                                    />
                                    <Button isLoading={mgm.isLoading} text="Enviar" color="primary" marginTop={15} onPress={() => this.handleSms()}/>
                                </TextBowWrapper>
                            }
                            childrenBottom={
                                <TextBowWrapper>
                                    <CommonText type="normal" color="light" center>
                                        Meu código:
                                    </CommonText>
                                    <CommonText type="subtitle" color="dark" center>
                                        {mgm.data ? mgm.data : "Erro ao buscar cod."}
                                    </CommonText>
                                </TextBowWrapper>
                            }
                        />
                        <TextBowWrapper>
                            <CommonText type="h3" color="primary" center marginTop={20}>
                                Prefere indicar por e-mail ou WhatsApp?{' '}
                            </CommonText>
                            <CommonText type="normla" color="dark" center marginTop={10}>
                                Copie o texto abaixo e envie via WhatsApp ou e-mail para seus amigos
                            </CommonText>
                        </TextBowWrapper>
                        <BoxInvite
                            color="secondary"
                            childrenTop={
                                <TextBowWrapper>
                                    <CommonText type="normal" color="dark" center>
                                        Olá você foi indicado para fazer parte do Programa +TOP.
                                        Para participar basta clicar neste link https://maistop.page.link/mgm e colocar o código
                                        no campo “Código”:{' '}
                                        <CommonText type="normal" color="dark" center bold>
                                            {mgm.data ? mgm.data : ""}
                                        </CommonText>
                                    </CommonText>
                                </TextBowWrapper>
                            }
                        />
                        <WrapperButtonRight>
                            <Button text="copiar" color="primary" marginTop={10} onPress={async () => {
                                const textClipboard = `Olá você foi indicado para fazer parte do Programa +TOP. Para participar basta clicar neste link https://maistop.page.link/mgm e colocar o código no campo Código: ${mgm.data ? mgm.data : ""}`
                                await Clipboard.setString(textClipboard);
                            }}/>
                        </WrapperButtonRight>
                    </Card>
                </Scroll>
                <Tab
                    openMenu={() => this.setState({ menuVisible: true })}
                    navigation={navigation}
                />
            </Container>
        );
    }
}

// Redux
const mapStateToProps = state => ({
    mgm: state.mgm,
});

const mapDispatchToProps = dispatch => ({
    sendSmsInvite: data => dispatch(mgmSmsAction(data)),
    reset: () => dispatch(mgmReset()),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Invite);
