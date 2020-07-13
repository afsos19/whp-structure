import * as React from 'react';
import { Platform } from 'react-native'
import { TextInputMask } from 'react-native-masked-text';
import styled from 'styled-components/native';
import CommonText from '../common/CommonText';

const StyledInput = styled.TextInput`
    margin-bottom: ${Platform.OS == 'ios' ? 5 : -5 };
    font-family: ${p => p.theme.fontFamily.input};
    font-size: ${p => p.theme.fontSize.h3};
    color: ${p => p.theme.colors.inputTextDark };
    text-align: center;
    width: 30;
    border-bottom-width: 1;
    border-bottom-color: ${p => p.theme.colors.borderLight};
    margin-horizontal: 5;
`;

const Wrapper = styled.View`
    flex-direction: row;
    align-items: center;
    justify-content: center;
    margin-top: 20;
    align-self: ${p => (p.center ? 'center' : 'auto')};
    width: 80%;
    /* 
     
    */
`;

class InputCode extends React.Component {
    state = {
        code: ['', '', '', '', '', '', ]
    };

    render() {
        const { code } = this.state;
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
                <StyledInput
                    ref={ref => this.input1 = ref}
                    center={true}
                    maxLength={1}
                    autoCapitalize="none"
                    value={code[0]}
                    onChangeText={(text) => {
                        code[0] = text
                        this.setState({ code })
                        onChange(code)
                        if (text) {
                            this.input2.focus() 
                        }
                    }}
                />
                <StyledInput
                    ref={ref => this.input2 = ref}
                    center={true}
                    maxLength={1}
                    autoCapitalize="none"
                    value={code[1]}
                    onChangeText={(text) => {
                        code[1] = text
                        this.setState({ code })
                        onChange(code)
                        if (text) {
                            this.input3.focus() 
                        }                        
                    }}
                />
                <StyledInput
                    ref={ref => this.input3 = ref}
                    center={true}
                    maxLength={1}
                    autoCapitalize="none"
                    value={code[2]}
                    onChangeText={(text) => {
                        code[2] = text
                        this.setState({ code })
                        onChange(code)
                        if (text) {
                            this.input4.focus() 
                        }
                    }}
                />
                <StyledInput
                    ref={ref => this.input4 = ref}
                    center={true}
                    maxLength={1}
                    autoCapitalize="none"
                    value={code[3]}
                    onChangeText={(text) => {
                        code[3] = text
                        this.setState({ code })
                        onChange(code)
                        if (text) {
                            this.input5.focus() 
                        }
                    }}
                />
                <StyledInput
                    ref={ref => this.input5 = ref}
                    center={true}
                    maxLength={1}
                    autoCapitalize="none"
                    value={code[4]}
                    onChangeText={(text) => {
                        code[4] = text
                        this.setState({ code })
                        onChange(code)
                        if (text) {
                            this.input6.focus() 
                        }
                    }}
                />
                <StyledInput
                    ref={ref => this.input6 = ref}
                    center={true}
                    maxLength={1}
                    autoCapitalize="none"
                    value={code[5]}
                    onChangeText={(text) => {
                        code[5] = text
                        this.setState({ code })
                        onChange(code)
                    }}
                />
            </Wrapper>
        );
    }
}

export default InputCode;
