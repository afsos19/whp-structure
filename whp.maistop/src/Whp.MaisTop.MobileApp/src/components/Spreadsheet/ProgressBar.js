import React from 'react';
import styled from 'styled-components/native';
import { Dimensions } from 'react-native';
import CommonText from '../common/CommonText';

const width = Dimensions.get('window').width * 0.8;

const Wrapper = styled.View`
    margin-top: 15;
    align-self: center;
    align-items: center;
    justify-content: space-between;
    padding-horizontal: 10;
    flex-direction: row;
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
    width: ${width};
    height: 35;
    background-color: ${p => p.theme.colors.background};
    border-width: .5;
    border-color: ${p => p.theme.colors.border};
    border-radius: 6;
`;

const Progress = styled.View`
    position: absolute;
    top: 0;
    left: 0;
    width: ${p => p.progress};
    height: 35;
    background-color: ${p =>
        (p.color === 'primary' && p.theme.colors.primary) ||
        (p.color === 'secondary' && p.theme.colors.secondary) ||
        (p.color === 'dark' && p.theme.colors.tertiary)};
    border-radius: 6;
`;

const ProgressBar = ({ percent, color }) => {
    return (
        <Wrapper>
            <Progress progress={width * (percent / 100)} color={color} />
            <CommonText type="normal" color={percent > 30 ? 'light' : 'dark'} center bold>
                Progresso
            </CommonText>

            <CommonText type="normal" color={percent >= 100 ? 'light' : 'dark'} center bold>
                {percent}%
            </CommonText>
        </Wrapper>
    );
};

export default ProgressBar;
