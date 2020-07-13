import 'moment';
import moment from 'moment-timezone';
import 'moment/locale/pt-br';
import React from 'react';
import styled from 'styled-components/native';
import CommonText from '../common/CommonText';
import ExtractLineHighlight from './ExtractLineHighlight';
import ExtractLine from './ExtractLine';

const Wrapper = styled.View`
    align-self: center;
    margin-top: 30;
`;

const WrapperTextLeft = styled.View`
    align-self: flex-end;
`;

const ExtractDetails = ({ extract }) => {
    return (
        <Wrapper>
            <ExtractLine
                text="Créditos totais:"
                value={extract.credit.toString().replace('.', ',')}
            />
            <ExtractLine
                text="Débitos totais:"
                value={extract.debit.toString().replace('.', ',')}
            />
            <ExtractLine
                text="Pontos expirados:"
                value={extract.expiredPunctuation.toString().replace('.', ',')}
            />
            <ExtractLineHighlight
                text="Pontos a expirar:"
                value={extract.nextAmoutToExpire.toString().replace('.', ',')}
            />
            <WrapperTextLeft>
                <CommonText type="small" color="secondary" marginTop={5}>
                    Último crédito: {moment(extract.dateLastCredit).format('LLL')}
                </CommonText>
            </WrapperTextLeft>
        </Wrapper>
    );
};

export default ExtractDetails;
