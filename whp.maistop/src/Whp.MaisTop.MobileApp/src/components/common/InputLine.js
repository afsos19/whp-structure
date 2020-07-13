import * as React from 'react';
import { Platform } from 'react-native'
import { TextInputMask } from 'react-native-masked-text';
import styled from 'styled-components/native';

const StyledMaskInput = styled(TextInputMask).attrs({
    placeholderTextColor: p =>
        p.textColor === 'dark' ? p.theme.colors.inputTextDark : p.theme.colors.inputTextLight,
})`
    margin-bottom: ${Platform.OS == 'ios' ? 5 : -5 };
    font-family: ${p => p.theme.fontFamily.input};
    font-size: ${p => p.theme.fontSize.input};
    color: ${p =>
        p.textColor === 'dark' ? p.theme.colors.inputTextDark : p.theme.colors.inputTextLight};
    text-align: ${p => (p.center ? 'center' : 'left')};
    width: 100%;
`;

const StyledInput = styled.TextInput.attrs({
    placeholderTextColor: p =>
        p.textColor === 'dark' ? p.theme.colors.inputTextDark : p.theme.colors.inputTextLight,
})`
    margin-bottom: ${Platform.OS == 'ios' ? 5 : -5 };
    font-family: ${p => p.theme.fontFamily.input};
    font-size: ${p => p.theme.fontSize.input};
    color: ${p =>
        p.textColor === 'dark' ? p.theme.colors.inputTextDark : p.theme.colors.inputTextLight};
    text-align: ${p => (p.center ? 'center' : 'left')};
    width: 100%;
`;

const Wrapper = styled.View`
    margin-top: 20;
    align-self: ${p => (p.center ? 'center' : 'auto')};
    width: 80%;
    border-bottom-width: 1;
    border-bottom-color: ${p => p.theme.colors.borderLight};
`;

const ErrorLabel = styled.Text`
    font-family: ${p => p.theme.fontFamily.regular};
    position: absolute;
    left: 0;
    bottom: -18;
    font-size: ${p => p.theme.fontSize.small};
    color: ${p => p.theme.colors.warning};
`;

class InputLine extends React.Component {
    state = {
        isFocused: false,
        validate: false,
    };

    handleFocus = () => this.setState({ isFocused: true });

    handleBlur = () => {
        const { value } = this.props;
        this.setState({ validate: true });

        if (value === '') {
            this.setState({ isFocused: false });
        }
    };

    render() {
        const { validate, isFocused } = this.state;
        const {
            value,
            placeholder,
            onChange,
            isVisible = false,
            errorMessage,
            mask = '',
            maxLength,
            enable = true,
            center = true,
            textColor = 'dark',
            returnKeyType= 'next',
            nextComponent = null,
            inputRef = null,
            ...rest
        } = this.props;
        return (
            <Wrapper center={center}>
                {mask ? (
                    <StyledMaskInput
                        ref={inputRef}
                        center={center}
                        value={value}
                        placeholder={placeholder}
                        editable={enable}
                        enable={enable}
                        maxLength={maxLength}
                        type={mask}
                        onChangeText={val => onChange(val)}
                        secureTextEntry={isVisible}
                        autoCapitalize="none"
                        textColor={textColor}
                        isFocused={isFocused}
                        onFocus={this.handleFocus}
                        onBlur={this.handleBlur}
                        returnKeyType={returnKeyType}
                        {...rest}
                    />
                ) : (
                    <StyledInput
                        ref={inputRef}
                        center={center}
                        value={value}
                        placeholder={placeholder}
                        editable={enable}
                        enable={enable}
                        maxLength={maxLength}
                        type={mask}
                        onChangeText={val => onChange(val)}
                        secureTextEntry={isVisible}
                        autoCapitalize="none"
                        textColor={textColor}
                        isFocused={isFocused}
                        onFocus={this.handleFocus}
                        onBlur={this.handleBlur}
                        returnKeyType={returnKeyType}
                        {...rest}
                    />
                )}
                {validate && errorMessage && <ErrorLabel>{errorMessage}</ErrorLabel>}
            </Wrapper>
        );
    }
}

export default InputLine;
