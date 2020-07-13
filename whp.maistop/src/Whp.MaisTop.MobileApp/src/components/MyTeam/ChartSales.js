import React from 'react';
import { Dimensions } from 'react-native';
import { VictoryBar } from 'victory-native';
import styled from 'styled-components/native';
import theme from '../../theme';
import CommonText from '../common/CommonText';

const width = Dimensions.get('window').width;

const determineColor = type => {
    switch (type) {
        case 'participant':
            return theme.colors.chartTrainingThree;
        case 'superTop':
            return theme.colors.secondary;
        default:
            return theme.colors.chartTrainingThree;
    }
};

const WrapperLegend = styled.View`
    align-self: flex-start;
    align-items: flex-start;
    padding-bottom: 20;
    margin-left: 20;
`;

const LegendItem = styled.View`
    /* align-self: center;- */
    /* align-items: center; */
    flex-direction: row;
    margin-top: 10;
`;

const LegendColor = styled.View`
    align-self: center;
    background-color: ${p => p.color};
    width: 20;
    height: 20;
    border-radius: 10;
    margin-right: 20;
`;

const ChartSales = ({ data }) => {
    return (
        <React.Fragment>
            <VictoryBar
                data={data}
                width={width * 0.4}
                // heigth={width * 0.95}
                style={{
                    data: { fill: item => determineColor(item.type) },
                }}
            />
            <CommonText type="normal" color="dark" bold center marginTop={-20}>
                *Unidades vendidas
            </CommonText>
            <WrapperLegend>
                {data.map(i => {
                    return (
                        <LegendItem>
                            <LegendColor color={determineColor(i.type)} />
                            <CommonText type="normal" color="dark">
                                {i.title}
                            </CommonText>
                        </LegendItem>
                    );
                })}
            </WrapperLegend>
        </React.Fragment>
    );
};

export default ChartSales;
