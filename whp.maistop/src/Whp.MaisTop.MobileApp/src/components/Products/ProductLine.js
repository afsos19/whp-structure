import React from 'react';
import styled from 'styled-components/native';
import CommonText from '../common/CommonText';

const Wrapper = styled.View`
    align-self: center;
    flex-direction: row;
    justify-content: space-between;
    margin-top: 10;
    width: 80%;
    padding-bottom: 5;
    border-bottom-width: 1;
    border-bottom-color: ${p => p.theme.colors.borderLight};
`;

const ProductLine = ({ item }) => {
    return (
        <Wrapper>
            <CommonText type="normal" color="light" bold>
                {item.product.categoryProduct.name}
            </CommonText>
            <CommonText type="normal" color="secondary" bold>
                {item.punctuation}
            </CommonText>
        </Wrapper>
    );
};

export default ProductLine;
