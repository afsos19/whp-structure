/* eslint-disable react/prefer-stateless-function */
import * as React from 'react';
import { StyleSheet } from 'react-native';
import styled from 'styled-components/native';
import RNPickerSelect from 'react-native-picker-select';
import Icon from 'react-native-vector-icons/MaterialIcons';
import theme from '../../theme';

const Wrapper = styled.View`
    margin-top: 20;
    align-self: center;
    justify-content: center;
    width: ${p => p.width};
`;

const ArrowIcon = styled(Icon).attrs({
    color: p => p.theme.colors.primary,
    name: 'keyboard-arrow-down',
    size: 24,
})``;

const pickerSelectStyles = StyleSheet.create({
    inputIOS: {
        fontSize: theme.fontSize.input,
        fontFamily: theme.fontFamily.input,
        height: 30,
        marginTop: 5,
        marginBottom: 5,
        borderBottomWidth: 1,
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
        borderBottomWidth: 1,
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

class InputOptionsLine extends React.Component {
    render() {
        const { value, onChange, width = '80%', items } = this.props;
        return (
            <Wrapper width={width}>
                <RNPickerSelect
                    placeholder={initialOption}
                    items={items}
                    onValueChange={opt => onChange(opt)}
                    value={value}
                    style={pickerSelectStyles}
                    Icon={() => {
                        return <ArrowIcon />;
                    }}
                />
                {/* {isFocused && errorMessage && <ErrorLabel>{errorMessage}</ErrorLabel>} */}
            </Wrapper>
        );
    }
}

export default InputOptionsLine;
