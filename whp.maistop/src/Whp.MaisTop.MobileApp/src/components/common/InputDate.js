import 'moment';
import moment from 'moment-timezone';
import 'moment/locale/pt-br';
import * as React from 'react';
import DateTimePicker from 'react-native-modal-datetime-picker';
import styled from 'styled-components/native';
import CommonText from './CommonText';

const StyledInput = styled.TouchableOpacity`
    justify-content: center;
    height: 30px;
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
const InputText = styled(CommonText)`
    font-size: ${p => p.theme.fontSize.input};
    font-family: ${p => p.theme.fontFamily.input};
    color: ${p => p.theme.colors.inputTextDark};
`;

class InputDate extends React.Component {
    state = {
        text: '',
        isFocused: false,
        // isDateTimePickerVisible: false,
    };

    componentDidMount() {
        const { initialValue } = this.props;

        if (initialValue) {
            this.setState({ text: moment(initialValue).format('L') });
        }
    }

    showDateTimePicker = () => this.setState({ isDateTimePickerVisible: true });

    hideDateTimePicker = () => this.setState({ isDateTimePickerVisible: false });

    handleDatePicked = date => {
        const { setField, fieldName } = this.props;
        setField(fieldName, moment(date).format());
        this.setState({ text: moment(date).format('L') });
        this.hideDateTimePicker();
    };

    render() {
        const { placeholder, errorMessage, width, enable = true, inputRef = null } = this.props;
        const { isFocused, isDateTimePickerVisible, text } = this.state;

        return (
            <Wrapper width={width}>
                <CommonText type="small" bold color="dark">
                    {placeholder.toUpperCase()}
                </CommonText>
                <StyledInput
                    ref={inputRef}
                    enable={enable}
                    isError={errorMessage && isFocused}
                    onPress={() => {
                        this.showDateTimePicker();
                        this.setState({ isFocused: true });
                    }}
                >
                    <InputText>{text}</InputText>
                </StyledInput>
                <DateTimePicker
                    isVisible={isDateTimePickerVisible}
                    onConfirm={this.handleDatePicked}
                    onCancel={this.hideDateTimePicker}
                />
                {errorMessage && isFocused && <ErrorLabel>{errorMessage}</ErrorLabel>}
            </Wrapper>
        );
    }
}

export default InputDate;
