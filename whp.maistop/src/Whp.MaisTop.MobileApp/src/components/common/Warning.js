import React from 'react';
import styled from 'styled-components/native';
import CommonText from './CommonText';
import Icon from 'react-native-vector-icons/Ionicons';

const Modal = styled.Modal.attrs({
    animationType: 'fade',
    transparent: true,
    visible: p => p.visible,
})``;

const Wrapper = styled.View`
    align-items: center;
    justify-content: center;
    width: 100%;
    padding-top: 45;
    padding-bottom: 15;
    padding-horizontal: 20;
    ${p => {
        switch (p.type) {
            case 'success':
                return `background-color: ${p.theme.colors.warningSuccess}`;
            case 'error':
                return `background-color: ${p.theme.colors.warningError}`;
            default:
                return `background-color: ${p.theme.colors.warningSuccess}`;
        }
    }};
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
    /* position: absolute;
    top: 40;
    right: 5; */
`;

/* 
    success
    error
*/

const Warning = ({ type = 'success', text = '', visible = false, close, title = '' }) => {
    return (
        <Modal visible={visible}>
            <Wrapper type={type}>
                <WrapperClose onPress={close}>
                    <CloseIcon />
                </WrapperClose>
                <CommonText type="subtitle" color="light" center>
                    {title}
                </CommonText>
                <CommonText type="normal" color="light" marginTop={5} center>
                    {text}
                </CommonText>
            </Wrapper>
        </Modal>
    );
};

export default Warning;
