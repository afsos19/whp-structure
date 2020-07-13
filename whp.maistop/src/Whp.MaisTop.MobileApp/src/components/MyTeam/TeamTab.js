import 'moment';
import moment from 'moment-timezone';
import 'moment/locale/pt-br';
import React from 'react';
import styled from 'styled-components/native';
import CommonText from '../common/CommonText';
import { imgOccurrencePath } from '../../utils/urls';

const Wrapper = styled.View`
    align-self: center;
    flex-direction: row;
    width: 100%;
    height: 40;
    align-items: center;
    justify-content: space-around;
`;

const CardTab = styled.TouchableOpacity`
    width: 25%;
    align-self: center;
    align-items: center;
    justify-content: center;
    height: 40;
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
    background-color: ${p => (p.active ? p.theme.colors.primary : p.theme.colors.background)};
`;

const TeamTab = ({ activeItem, changeTab }) => {
    return (
        <Wrapper>
            <CardTab active={activeItem === 1} onPress={() => changeTab(1)}>
                <CommonText type="small" color={activeItem === 1 ? 'light' : 'dark'} center>
                    Visão geral
                </CommonText>
            </CardTab>
            <CardTab active={activeItem === 2} onPress={() => changeTab(2)}>
                <CommonText type="small" color={activeItem === 2 ? 'light' : 'dark'} center>
                    Cadastros
                </CommonText>
            </CardTab>
            <CardTab active={activeItem === 3} onPress={() => changeTab(3)}>
                <CommonText type="small" color={activeItem === 3 ? 'light' : 'dark'} center>
                    Treinamentos
                </CommonText>
            </CardTab>
            <CardTab active={activeItem === 4} onPress={() => changeTab(4)}>
                <CommonText type="small" color={activeItem === 4 ? 'light' : 'dark'} center>
                    Vendas
                </CommonText>
            </CardTab>
        </Wrapper>
    );
};

export default TeamTab;
