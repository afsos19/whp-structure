import React from 'react';
import styled from 'styled-components/native';

const Wrapper = styled.View`
    align-self: center;
    background-color: ${p => p.theme.colors.background};
    border-radius: 17;
    width: ${p => p.width};
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
    padding-vertical: 30;
    padding-horizontal: 5;
    margin-top: ${p => p.marginTop};
    margin-bottom: ${p => p.marginBottom};
    ${p =>
        p.noPadding || p.noPaddingTop &&
        `padding-top: ${p.noPaddingTop ? 0 : 30};
        padding-horizontal: 0;
        padding-bottom: 0;`};
`;

const Card = ({ children, width = '80%', marginTop = 0, marginBottom = 0, noPadding = false, noPaddingTop = false }) => {
    return (
        <Wrapper
            width={width}
            marginTop={marginTop}
            marginBottom={marginBottom}
            noPadding={noPadding}
            noPaddingTop={noPaddingTop}
        >
            {children}
        </Wrapper>
    );
};

export default Card;
