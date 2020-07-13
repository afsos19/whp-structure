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
    width: 90%;
`;

const WrapperInfo = styled.View`
    flex: 5;
    border-top-width: 1;
    border-color: ${p => p.theme.colors.border};
    align-self: center;
    align-items: center;
    justify-content: center;
    padding-top: 10;
    padding-bottom: 10;
    padding-left: 5;
`;
const WrapperValue = styled.View`
    flex: 2;
    border-top-width: 1;
    border-color: ${p => p.theme.colors.productsBackground};
    align-self: center;
    align-items: center;
    justify-content: center;
    padding-top: 10;
    padding-bottom: 10;
    background-color: ${p => p.theme.colors.productsBackground};
`;

const ExtractLine = ({ text, value }) => {
    return (
        <Wrapper>
            <WrapperInfo>
                <CommonText type="normal" color="dark">
                    {text}
                </CommonText>
            </WrapperInfo>
            <WrapperValue>
                <CommonText type="normal" color="light" center>
                    {value}
                </CommonText>
            </WrapperValue>
        </Wrapper>
    );
};

export default ExtractLine;
