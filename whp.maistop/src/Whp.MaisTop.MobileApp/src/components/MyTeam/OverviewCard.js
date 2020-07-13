import React from 'react';
import styled from 'styled-components/native';
import { VictoryPie, VictoryLabel } from 'victory-native';
import CommonText from '../common/CommonText';
import theme from '../../theme';
import ChartUsers from './ChartUsers';
import ChartTraining from './ChartTraining';
import ChartSales from './ChartSales';

const Wrapper = styled.View`
    align-self: center;
    align-items: center;
    justify-content: flex-start;
    width: ${p => p.width};
    padding-bottom: 10;
    border-radius: 9;
    background-color: ${p => p.theme.colors.background};
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
    border-width: .5;
    border-color: ${p => p.theme.colors.borderMyTeam};
    min-height: 460;
`;

const OverviewCard = ({ item, index, width }) => {
    const title =
        (index === 0 && 'Equipe Cadastrada') || (index === 1 && 'Treinamentos') || 'Vendas';
    return (
        <Wrapper width={width}>
            <CommonText type="subtitle" color="dark" marginTop={15} bold center>
                {title}
            </CommonText>
            {item ? (
                (index === 0 && <ChartUsers data={item} />) ||
                (index === 1 && <ChartTraining data={item} />) ||
                (index === 2 && <ChartSales data={item} />)
            ) : (
                <CommonText type="subtitle" color="dark" marginTop={15} bold center>
                    Sem dados
                </CommonText>
            )}
        </Wrapper>
    );
};

export default OverviewCard;
