import React from 'react';
import styled from 'styled-components/native';
import images from '../../res/images';

const Wrapper = styled.View`
    margin-top: ${p => p.marginTop};
`;

const CustomImage = styled.Image`
    height: ${p => p.height};
    align-self: center;
`;

const Logo = ({ marginTop = 0, height = '100%', img }) => {
    return (
        <Wrapper marginTop={marginTop}>
            <CustomImage source={img} height={height} resizeMode="contain" />
        </Wrapper>
    );
};

export default Logo;
