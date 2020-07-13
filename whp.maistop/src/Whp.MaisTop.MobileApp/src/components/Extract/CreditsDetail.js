/* eslint-disable no-nested-ternary */
import React from 'react';
import styled from 'styled-components/native';
import { ActivityIndicator } from 'react-native';
import CommonText from '../common/CommonText';
import CreditLine from './CreditLine';

const Wrapper = styled.View`
    align-self: center;
    margin-top: 30;
`;

const WrapperTextLeft = styled.View`
    align-self: flex-end;
`;

const WrapperLine = styled.View`
    align-self: center;
    flex-direction: row;
    align-items: center;
    justify-content: center;
    width: 90%;
`;

const WrapperInfoTitle = styled.View`
    flex: 5;
    align-self: center;
    align-items: center;
    justify-content: center;
    padding-top: 20;
    padding-left: 5;
`;

const WrapperValueTitle = styled.View`
    flex: 2;
    align-self: center;
    align-items: center;
    justify-content: center;
    padding-top: 20;
`;

const CreditsDetail = ({ credits, isLoading }) => {
    if (isLoading) {
        return (
            <Wrapper>
                <ActivityIndicator />
            </Wrapper>
        );
    }
    return (
        <Wrapper>
            {!credits ? (
                <CommonText>Nenhum crédito encontrado</CommonText>
            ) : (
                <React.Fragment>
                    <WrapperLine>
                        <WrapperInfoTitle>
                            <CommonText type="normal" color="dark" bold>
                                TIPO
                            </CommonText>
                        </WrapperInfoTitle>
                        <WrapperValueTitle>
                            <WrapperTextLeft>
                                <CommonText type="normal" color="dark" bold>
                                    PONTOS
                                </CommonText>
                            </WrapperTextLeft>
                        </WrapperValueTitle>
                    </WrapperLine>
                    {credits.map(item => {
                        return (
                            <CreditLine
                                key={item.id}
                                text={item.description}
                                value={item.punctuation.toString().replace('.', ',')}
                            />
                        );
                    })}
                </React.Fragment>
            )}
        </Wrapper>
    );
};

export default CreditsDetail;
