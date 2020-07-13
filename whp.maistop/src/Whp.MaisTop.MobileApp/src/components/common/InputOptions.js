import * as React from 'react';
import { StyleSheet } from 'react-native';
import styled from 'styled-components/native';
import RNPickerSelect from 'react-native-picker-select';
import theme from '../../theme';
import CommonText from './CommonText';

const Wrapper = styled.View`
    margin-top: 20;
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

const pickerSelectStyles = StyleSheet.create({
    inputIOS: {
        fontSize: theme.fontSize.input,
        fontFamily: theme.fontFamily.input,
        height: 30,
        marginTop: 5,
        marginBottom: 5,
        borderWidth: 1,
        borderColor: theme.colors.borderLight,
        color: theme.colors.inputTextDark,
        borderRadius: 9,
        paddingRight: 30,
        paddingLeft: 5,
    },
    inputAndroid: {
        fontSize: theme.fontSize.input,
        fontFamily: theme.fontFamily.input,
        height: 30,
        marginTop: 5,
        marginBottom: 5,
        borderWidth: 1,
        borderColor: theme.colors.borderLight,
        borderRadius: 9,
        paddingRight: 30,
        paddingLeft: 5,
    },
});

const initialOption = {
    label: 'Selecione',
    value: '',
    color: theme.colors.input,
};

class InputOptions extends React.Component {
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
        const { isFocused } = this.state;
        const {
            value,
            placeholder = '',
            onChange,
            errorMessage,
            width = '80%',
            items,
        } = this.props;
        return (
            <Wrapper width={width}>
                <CommonText type="small" bold color="dark">
                    {placeholder.toUpperCase()}
                </CommonText>
                <RNPickerSelect
                    placeholder={initialOption}
                    items={items}
                    onValueChange={opt => {
                        this.setState({ isFocused: true });
                        onChange(opt);
                    }}
                    value={value}
                    style={pickerSelectStyles}
                />
                {isFocused && errorMessage && <ErrorLabel>{errorMessage}</ErrorLabel>}
            </Wrapper>
        );
    }
}

export default InputOptions;
