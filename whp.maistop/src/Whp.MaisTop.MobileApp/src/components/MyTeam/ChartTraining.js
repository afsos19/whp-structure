import React from 'react';
import { Dimensions } from 'react-native';
import { VictoryPie } from 'victory-native';
import styled from 'styled-components/native';
import theme from '../../theme';
import CommonText from '../common/CommonText';

const width = Dimensions.get('window').width;

const determineColor = type => {
    switch (type) {
        case 'trainingTwoDone':
            return theme.colors.chartTrainingOne;
        case 'trainingOneDone':
            return theme.colors.chartTrainingTwo;
        case 'trainingNotDone':
            return theme.colors.chartTrainingThree;
        default:
            return theme.colors.chartTrainingOne;
    }
};

const WrapperLegend = styled.View`
    align-self: flex-start;
    padding-bottom: 20;
    margin-left: 20;
`;

const LegendItem = styled.View`
    align-self: center;
    align-items: center;
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

const ChartTraining = ({ data }) => {
    return (
        <React.Fragment>
            <VictoryPie
                innerRadius={100}
                data={data}
                width={width * 0.95}
                heigth={width * 0.95}
                style={{
                    data: { fill: item => determineColor(item.type) },
                }}
            />
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

export default ChartTraining;
