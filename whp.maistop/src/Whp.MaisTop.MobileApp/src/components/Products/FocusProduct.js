import React from 'react';
import styled from 'styled-components/native';
import CommonText from '../common/CommonText';
import { imgProductsPath } from '../../utils/urls';

const WrapperTextFocusProducts = styled.View`
    margin-top: 32;
    flex-direction: row;
    justify-content: space-between;
    flex-wrap: wrap;
    width: 60%;
`;

const WrapperPointsFocusDetails = styled.View`
    flex: 1;
    width: 100%;
    flex-direction: row;
    align-self: center;
`;

const WrapperCollumOne = styled.View`
    flex-direction: column;
`

const WrapperFocusProductsLine = styled.View`
    margin-top: 20;
    flex-direction: row;
    align-items: center;
    width: 100%;
`;

const CardImage = styled.View`
    margin-top: 16;
    width: 80;
    height: 80;
    background-color: ${p => p.theme.colors.background};
    margin-right: 20;
    border-radius: 4;
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 2;
`;

const ProductImage = styled.Image`
    width: 75;
    height: 75;
    align-self: center;
`;

const WrapperText = styled.View`
    width: 50%;
`;

const FocusProduct = ({ product, allFocus }) => {
    const allSKUS = [];

    allFocus.map(p => {
        if (p.groupProduct.id === product.groupProduct.id) {
            allSKUS.push(p.product.sku);
        }
    });
    
    return (
        <WrapperFocusProductsLine>
            <WrapperPointsFocusDetails>
                <WrapperCollumOne>
                    <CommonText type="normal" color="primary" bold>
                        {product.groupProduct.name}
                    </CommonText>
                    <CardImage>
                        <ProductImage
                            source={{
                                uri: imgProductsPath + product.product.photo,
                            }}
                            resizeMode="contain"
                        />
                    </CardImage>
                </WrapperCollumOne>
                <WrapperTextFocusProducts>
                        {allSKUS.map((sku, index) => {
                            return (
                                <WrapperText key={index}>
                                    <CommonText type="small" color="dark" bold marginTop={4}>
                                        {sku}
                                    </CommonText>
                                </WrapperText>
                            );
                        })}
                </WrapperTextFocusProducts>
            </WrapperPointsFocusDetails>
        </WrapperFocusProductsLine>
    );
};

export default FocusProduct;
