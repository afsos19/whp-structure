import * as React from 'react';
import { TextInputMask } from 'react-native-masked-text';
import styled from 'styled-components/native';
import CommonText from './CommonText';

const StyledMaskInput = styled(TextInputMask)`
    height: 40px;
    border-width: 1;
    border-color: ${p => p.theme.colors.borderLight};
    border-radius: 9;
    margin-top: 5;
    margin-bottom: 5;
    padding-horizontal: 5;
    ${p => !p.enable && `background-color: ${p.theme.colors.disabled};`}
    font-family: ${p => p.theme.fontFamily.input};
    font-size: ${p => p.theme.fontSize.input};
    color: ${p => p.theme.colors.inputTextDark};
    text-align: ${p => (p.center ? 'center' : 'left')};
    width: 100%;
`;

const StyledInput = styled.TextInput`
    height: 40px;
    border-width: 1;
    border-color: ${p => p.theme.colors.borderLight};
    border-radius: 9;
    margin-top: 5;
    margin-bottom: 5;
    padding-horizontal: 5;
    ${p => !p.enable && `background-color: ${p.theme.colors.disabled};`}
    font-family: ${p => p.theme.fontFamily.input};
    font-size: ${p => p.theme.fontSize.input};
    color: ${p => p.theme.colors.inputTextDark};
    text-align: ${p => (p.center ? 'center' : 'left')};
    width: 100%;
`;

const Wrapper = styled.View`
    margin-top: ${p => p.marginTop};
    align-self: center;
    justify-content: center;
    width: ${p => p.width};
`;

const ErrorLabel = styled.Text`
    font-family: ${p => p.theme.fontFamily.regular};
    position: absolute;
    left: 0;
    bottom: -10;
    font-size: ${p => p.theme.fontSize.small};
    color: ${p => p.theme.colors.warning};
`;

class InputBorder extends React.Component {
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
            marginTop = 20,
            value,
            placeholder,
            onChange,
            errorMessage,
            mask = '',
            maxLength,
            enable = true,
            width = '80%',
            returnKeyType= 'next',
            inputRef = null,
            ...rest
        } = this.props;
        return (
            <Wrapper width={width} marginTop={marginTop}>
                <CommonText type="small" bold color="dark">
                    {placeholder.toUpperCase()}
                </CommonText>
                {mask ? (
                    <StyledMaskInput
                        ref={inputRef}
                        value={value}
                        editable={enable}
                        enable={enable}
                        maxLength={maxLength}
                        type={mask}
                        onChangeText={val => onChange(val)}
                        autoCapitalize="none"
                        isFocused={isFocused}
                        onFocus={this.handleFocus}
                        onBlur={this.handleBlur}
                        returnKeyType={returnKeyType}
                        {...rest}
                    />
                ) : (
                    <StyledInput
                        ref={inputRef}
                        value={value}
                        editable={enable}
                        enable={enable}
                        maxLength={maxLength}
                        type={mask}
                        onChangeText={val => onChange(val)}
                        autoCapitalize="none"
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

export default InputBorder;
