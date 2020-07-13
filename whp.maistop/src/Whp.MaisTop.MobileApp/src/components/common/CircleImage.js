import React from 'react';
import styled from 'styled-components/native';

const CustomImage = styled.Image`
    align-self: center;
    justify-content: center;
    align-items: center;
    width: ${p => p.size};
    height: ${p => p.size};
    border-radius: ${p => p.size / 2};
    background-color: ${p => p.theme.colors.secondaryBackground};
`;

const CircleImage = ({ image = '', size = 180 }) => {
    return <CustomImage source={{ uri: image }} size={size} resizeMode="cover" />;
};

export default CircleImage;
