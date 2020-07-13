import React from 'react';
import { Keyboard, Platform } from 'react-native';
import styled from 'styled-components/native';
import Icon from 'react-native-vector-icons/Ionicons';
import ImagePicker from 'react-native-image-picker';
import CommonText from '../common/CommonText';
import Button from '../common/Button';

const options = {
    title: 'Selecione uma foto',
    // quality: 0,
    maxWidth: 400,
    maxHeight: 400,
    storageOptions: {
        skipBackup: true,
        path: 'images',
    },
};

const Wrapper = styled.View`
    background-color: ${p => p.theme.colors.background};
    justify-content: flex-start;
    align-items: center;
    padding-left: 30;
    height: ${p => (p.isFocused ? '200px' : '60px')};
    padding-bottom: 30;
    padding-top: 10;
    width: 100%;
`;

const StyledInput = styled.TextInput.attrs({
    placeholderTextColor: p => p.theme.colors.inputTextDark,
})`
    height: ${p => (p.isFocused ? '140px' : Platform.OS === 'ios' ? '30px' : '40px')};
    margin-bottom: 5;
    font-family: ${p => p.theme.fontFamily.input};
    font-size: ${p => p.theme.fontSize.input};
    color: ${p => p.theme.colors.inputTextDark};
    width: 100%;
`;

const ButtonContainer = styled.View`
    width: 90%;
    align-self: center;
    align-items: flex-end;
    justify-content: flex-end;
    flex-direction: row;
`;

const TouchIcon = styled.TouchableOpacity`
    margin-right: 10;
`;

const AttachmentIcon = styled(Icon).attrs({
    name: 'ios-attach',
    size: 20,
    color: p => p.theme.colors.iconDark,
})`
    align-self: center;
`;

const TextAttachment = styled.View`
    align-self: center;
    margin-right: 10;
`;

class InputChat extends React.Component {
    state = {
        isFocused: false,
        msg: '',
        attachment: null,
        fileName: null,
    };

    handleFocus = () => this.setState({ isFocused: true });

    handleBlur = () => this.setState({ isFocused: false });

    handleAttachment = () => {
        ImagePicker.showImagePicker(options, response => {
            if (response.didCancel) {
                console.log('User cancelled image picker');
            } else if (response.error) {
                console.log('ImagePicker Error: ', response.error);
            } else if (response.customButton) {
                console.log('User tapped custom button: ', response.customButton);
            } else {
                this.setState({ attachment: response.uri, fileName: response.fileName });
            }
        });
    };

    handleSend = () => {
        const { msg, attachment } = this.state;
        const { sendMsg } = this.props;

        sendMsg(msg, attachment);
        this.setState({
            msg: '',
            attachment: null,
        });
        Keyboard.dismiss();
    };

    render() {
        const { isFocused, msg, attachment, fileName } = this.state;
        const { placeholder, isLoading, sendMsg } = this.props;

        return (
            <Wrapper isFocused={isFocused}>
                <StyledInput
                    multiline
                    textAlignVertical="top"
                    isFocused={isFocused}
                    value={msg}
                    placeholder={placeholder}
                    editable
                    enable
                    onChangeText={val => this.setState({ msg: val })}
                    autoCapitalize="none"
                    // isFocused={isFocused}
                    onFocus={this.handleFocus}
                    onBlur={this.handleBlur}
                />
                {isFocused && (
                    <ButtonContainer>
                        {attachment && (
                            <TextAttachment>
                                <CommonText type="small" color="primary">
                                    {fileName}
                                </CommonText>
                            </TextAttachment>
                        )}
                        <TouchIcon onPress={() => this.handleAttachment()}>
                            <AttachmentIcon />
                        </TouchIcon>
                        <Button
                            onPress={() => this.handleSend()}
                            text="Enviar"
                            type="small"
                            disabled={msg === ''}
                            color="primary"
                            isLoading={isLoading}
                        />
                    </ButtonContainer>
                )}
                {/* <CommonText type="normal" color="dark">
                    {placeholder}
                </CommonText> */}
            </Wrapper>
        );
    }
}

export default InputChat;
