import React from 'react';
import styled from 'styled-components/native';
import CommonText from '../common/CommonText';

const Wrapper = styled.View`
    align-self: flex-start;
    width: 70;
    align-items: flex-start;
    justify-content: center;
`;

const Card = styled.View`
    margin-bottom: 10;
    align-self: center;
    align-items: center;
    justify-content: center;
    background-color: ${p => (p.selected ? p.theme.colors.primary : p.theme.colors.background)};
    width: 65;
    height: 65;
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
    border-radius: 6;
`;

const Image = styled.Image`
    align-self: center;
    width: 30;
    height: 30;
`;

const EmployeesStep = ({ selected = false, image, text }) => {
    return (
        <Wrapper>
            <Card selected={selected}>
                <Image source={image} resizeMode="contain" />
            </Card>
            <CommonText type="small" color="dark" center>
                {text}
            </CommonText>
        </Wrapper>
    );
};

export default EmployeesStep;
