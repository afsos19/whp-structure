import React from 'react';
import styled from 'styled-components/native';
import Icon from 'react-native-vector-icons/Ionicons';
import LinearGradient from 'react-native-linear-gradient';
import CommonText from '../common/CommonText';
import images from '../../res/images';
import Card from '../common/Card';
import ROUTENAMES from '../../navigation/routeName';

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

const AboutImage = styled.Image`
    align-self: center;
    width: 75;
    height: 75;
`;


const WrapperCard = styled.View`
    align-self: center;
    align-items: center;
    width: 80%;
`;

const WrapperClose = styled.TouchableOpacity`
    align-self: flex-end;
`;

const WrapperCollumn = styled.View`
    flex-direction: row;
`;

const Collumn = styled.View`
    flex: 1;
    align-items: center;
    padding: 8px;
`;

const ButtonUpdate = styled.TouchableOpacity`
    align-items: center;
    justify-content: center;
    margin-top: 32px;
    margin-bottom: 32px;
    width: 80%;
    height: 30px;
    background-color: ${p => p.theme.colors.secondary};
    border-radius: 4px;
    padding: 8px;
`;

const IconModal = styled.Image`
    align-self: center;
    width: 70px;
    height: 70px;
`;

const CollumnGradient = styled(LinearGradient).attrs({
    colors: ["#8d8980", "#d9d5cd"],
    start: {x: 1, y: 0},
    end: {x: 0, y: 0}
    // linear-gradient(to left,#8d8980,#d9d5cd)
})`
    flex: 1;
    align-items: center;
    justify-content: center;
    padding: 8px;
    border-top-left-radius: 17;
    border-bottom-left-radius: 17;
`

const CloseIcon = styled(Icon).attrs({
    name: 'ios-close',
    size: 40,
    color: p => p.theme.colors.iconDark,
})`
    align-self: flex-end;
    margin-right: 8px;
`;

class UpdateRegister extends React.Component {
    render() {
        const { visible = false, close, navigation } = this.props;

        return (
            <Modal visible={visible}>
                <Wrapper>
                    <Card width="90%" noPaddingTop>
                        <WrapperCollumn>
                            <CollumnGradient>
                                <IconModal source={images.updateUser} />
                                <CommonText type="h3" color="black" marginTop={20} center>ATUALIZE SEUS DADOS</CommonText>                            
                            </CollumnGradient>
                            <Collumn>
                                <WrapperClose onPress={close}>
                                    <CloseIcon />
                                </WrapperClose>
                                <CommonText type="normal" color="dark" marginTop={16} center>
                                    <CommonText type="normal" color="dark" marginTop={16} center bold>Parceiro,</CommonText>
                                    você ainda não atualizou seus dados.
                                </CommonText>
                                <CommonText type="normal" color="dark" marginTop={8} center>
                                    Atualize agora e garanta a entrega de seus prêmios e benefícios do programa +TOP!
                                </CommonText>
                                <ButtonUpdate onPress={() => {
                                    close()
                                    navigation.navigate(ROUTENAMES.PROFILE, { editAdress: true })
                                }}>
                                    <CommonText type="small" color="light" center>
                                        ATUALIZAR AGORA
                                    </CommonText>
                                </ButtonUpdate>
                            </Collumn>
                        </WrapperCollumn>
                    </Card>
                </Wrapper>
            </Modal>
        );
    }
}

export default UpdateRegister;
