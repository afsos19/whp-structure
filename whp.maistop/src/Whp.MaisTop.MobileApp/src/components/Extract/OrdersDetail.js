/* eslint-disable no-nested-ternary */
import 'moment';
import moment from 'moment-timezone';
import 'moment/locale/pt-br';
import React from 'react';
import styled from 'styled-components/native';
import { ActivityIndicator } from 'react-native';
import CommonText from '../common/CommonText';
import OrderLine from './SalesLine';

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
    flex: 1;
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

const OrdersDetail = ({ orders, isLoading }) => {
    if (isLoading) {
        return (
            <Wrapper>
                <ActivityIndicator />
            </Wrapper>
        );
    }
    return (
        <Wrapper>
            {!orders ? (
                <CommonText>Nenhum resgate encontrado</CommonText>
            ) : (
                <React.Fragment>
                    <WrapperLine>
                        <WrapperInfoTitle>
                            <CommonText type="normal" color="dark" bold>
                                DATA
                            </CommonText>
                        </WrapperInfoTitle>
                        <WrapperValueTitle>
                            <CommonText type="normal" color="dark">
                                DESCRIÇÃO
                            </CommonText>
                        </WrapperValueTitle>
                        <WrapperInfoTitle>
                            <WrapperTextLeft>
                                <CommonText type="normal" color="dark" bold>
                                    VALOR
                                </CommonText>
                            </WrapperTextLeft>
                        </WrapperInfoTitle>
                    </WrapperLine>
                    {orders.map(item => {
                        return (
                            <OrderLine
                                key={item.id}
                                date={moment(item.createdAt).format('L')}
                                text={item.description}
                                value={item.total}
                            />
                        );
                    })}
                </React.Fragment>
            )}
        </Wrapper>
    );
};

export default OrdersDetail;
