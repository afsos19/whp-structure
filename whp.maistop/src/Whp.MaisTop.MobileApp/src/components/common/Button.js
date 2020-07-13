import React from 'react';
import { ActivityIndicator } from 'react-native';
import styled from 'styled-components/native';

const Wrapper = styled.TouchableOpacity`
    align-self: center;
    margin-top: ${p => p.marginTop};
    background-color: ${p =>
        (p.color === 'primary' && p.theme.colors.primary) ||
        (p.color === 'secondary' && p.theme.colors.secondary) ||
        (p.color === 'dark' && p.theme.colors.buttonDark) ||
        (p.color === 'light' && p.theme.colors.secondaryBackground) ||
        (p.color === 'transparent' && p.theme.colors.transparent)};
    justify-content: center;
    align-items: center;
    margin-horizontal: 5;
    border: ${p => (p.color === 'transparent' ? `1px solid ${p.theme.colors.textLight}` : '0')};
    margin-top: ${p => p.marginTop};
    ${p => {
        switch (p.type) {
            case 'normal':
                return `
                    width: 133;
                    height: 35.5;
                    border-radius: 17;
                `;
            case 'small':
                return `
                    width: 90;
                    height: 20;
                    border-radius: 10;
                `;
            case 'full':
                return `
                    width: 90%;
                    height: 40;
                    border-radius: 20;
                `;
            default:
                return `
                    width: 133;
                    height: 35.5;
                    border-radius: 17;
                `;
        }
    }};
`;

const Title = styled.Text`
    font-family: ${p => p.theme.fontFamily.button};
    font-size: ${p => p.theme.fontSize.button};
    color: ${p => (p.color === 'light' ? p.theme.colors.textDark : p.theme.colors.textLight)};
    color: ${p =>
        (p.color === 'primary' && p.theme.colors.textLight) ||
        (p.color === 'secondary' && p.theme.colors.textLight) ||
        (p.color === 'dark' && p.theme.colors.secondary) ||
        (p.color === 'light' && p.theme.colors.textDark) ||
        (p.color === 'transparent' && p.theme.colors.textLight)};
    text-align: center;
    ${p => {
        switch (p.type) {
            case 'normal':
                return `font-size: ${p.theme.fontSize.button};`;
            case 'small':
                return `font-size: ${p.theme.fontSize.button};`;
            case 'full':
                return `font-size: ${p.theme.fontSize.normal};`;
            default:
                return `font-size: ${p.theme.fontSize.button};`;
        }
    }};
    ${p => p.bold && `font-family: ${p.theme.fontFamily.bold};`};
`;

/*
        normal
        small
        full
*/
const Button = ({
    onPress,
    text,
    type = 'normal',
    disabled = false,
    isLoading = false,
    color = 'primary',
    marginTop = 0,
    bold = false,
}) => {
    return (
        <Wrapper
            color={color}
            onPress={onPress}
            disabled={disabled || isLoading}
            marginTop={marginTop}
            type={type}
        >
            {isLoading ? (
                <ActivityIndicator />
            ) : (
                <Title bold={bold} color={color} disabled={disabled} type={type}>
                    {text.toUpperCase()}
                </Title>
            )}
        </Wrapper>
    );
};

export default Button;
