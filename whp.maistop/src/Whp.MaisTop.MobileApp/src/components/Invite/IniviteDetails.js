import React from 'react';
import styled from 'styled-components/native';
import Icon from 'react-native-vector-icons/Ionicons';
import CommonText from '../common/CommonText';
import images from '../../res/images';
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

const WrapperClose = styled.TouchableOpacity`
    align-self: flex-end;
`;

const Line = styled.View`
    background-color: ${p => p.theme.colors.borderMyTeam};
    height: 1;
    width: 100%;
    margin-top: 20;
`;

const WrapperCard = styled.View`
    align-self: center;
    align-items: center;
    width: 90%;
`;

const Title = styled(CommonText)`
    color: ${p => p.theme.colors.headerInviteDetail};
`;

const CloseIcon = styled(Icon).attrs({
    name: 'ios-close',
    size: 25,
    color: p => p.theme.colors.iconLight,
})`
    align-self: flex-end;
`;

const ImageIcon = styled.Image`
    align-self: flex-start;
    margin-top: 10;
    width: 20;
    height: 20;
`;

const IniviteDetails = ({ visible = false, close, item }) => {
    return (
        <Modal visible={visible}>
            <Wrapper>
                <WrapperClose onPress={close}>
                    <CloseIcon />
                </WrapperClose>
                <Card width="90%" marginTop={15}>
                    {item && 
                        <WrapperCard>
                            <Title type="normal" bold marginTop={20}>Nome</Title>
                            <CommonText type="normal" color="dark" bold>{item.result.name ? item.result.name : ""}</CommonText>
                            <CommonText type="normal" color={item.result.gotPunctuation ? "secondary" : "dark"}>{item.result.gotPunctuation ? "Cadastrado" : "Pendente"}</CommonText>
                            <Line />
                            <Title type="normal" bold marginTop={20}>Data da indicação</Title>
                            <CommonText type="normal" color="dark">{item.result.invitedAt ? item.result.invitedAt : ""}</CommonText>
                            {/* <Line />
                            <Title type="normal" bold marginTop={20}>Meio de indicação</Title>
                            <CommonText type="normal" color="dark">Código externo</CommonText> */}
                            <Line />
                            <Title type="normal" bold marginTop={20}>Data de cadastro</Title>
                            <CommonText type="normal" color="dark">{item.result.createdAt ? item.result.createdAt : ""}</CommonText>
                            
                            <Line />
                            <Title type="normal" bold marginTop={20}>Credito de pontos</Title>
                            <ImageIcon source={item.result.isCanceled ? images.inviteIcon3 : item.result.gotPunctuation ? images.inviteIcon1 : images.inviteIcon2 } resizeMode="contain" />
                            <Line />
                        </WrapperCard>
                    }
                </Card>
            </Wrapper>
        </Modal>
    );
}

export default IniviteDetails;
