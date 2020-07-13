import React from 'react';
import styled from 'styled-components/native';
import CommonText from '../common/CommonText';
import { imgNewsPath } from '../../utils/urls';
import Button from '../common/Button';
import ROUTENAMES from '../../navigation/routeName';

const Wrapper = styled.View`
    align-self: center;
    flex-direction: row;
    align-items: center;
    justify-content: center;
    border-bottom-width: 1;
    border-color: ${p => p.theme.colors.border};
    width: 90%;
`;

const WrapperTextLeft = styled.View`
    align-self: flex-end;
`;

const WrapperInfo = styled.View`
    flex: 5;
    align-self: center;
    align-items: center;
    justify-content: center;
    padding-top: 20;
    padding-bottom: 10;
    padding-left: 5;
`;

const WrapperValue = styled.View`
    flex: 2;
    align-self: center;
    align-items: center;
    justify-content: center;
    padding-top: 20;
    padding-bottom: 10;
`;

const CreditLine = ({ text, value }) => {
    return (
        <Wrapper>
            <WrapperInfo>
                <CommonText type="normal" color="dark">
                    {text}
                </CommonText>
            </WrapperInfo>
            <WrapperValue>
                <WrapperTextLeft>
                    <CommonText type="normal" color="secondary" bold center>
                        {value}
                    </CommonText>
                </WrapperTextLeft>
            </WrapperValue>
        </Wrapper>
    );
};

export default CreditLine;
