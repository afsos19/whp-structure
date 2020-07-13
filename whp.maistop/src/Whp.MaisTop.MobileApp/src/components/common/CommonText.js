import React from 'react';
import styled from 'styled-components/native';

const Text = styled.Text`
    margin-top: ${p => p.marginTop};
    ${p => {
        switch (p.color) {
            case 'dark':
                return `color: ${p.theme.colors.textDark}`;
            case 'black':
                return `color: ${p.theme.colors.black}`;
            case 'light':
                return `color: ${p.theme.colors.textLight}`;
            case 'primary':
                return `color: ${p.theme.colors.primaryText}`;
            case 'secondary':
                return `color: ${p.theme.colors.secondaryText}`;
            case 'secondaryDark':
                return `color: ${p.theme.colors.secondaryTextDark}`;
            default:
                return `color: ${p.theme.colors.textDark}`;
        }
    }};
    font-size: ${p => p.theme.fontSize.normal};
    font-family: ${p => p.theme.fontFamily.regular};
    ${p => {
        switch (p.type) {
            case 'normal':
                return `
                  font-family: ${p.theme.fontFamily.regular};
                  font-size: ${p.theme.fontSize.normal};
                `;
            case 'normalBold':
                return `
                  font-family: ${p.theme.fontFamily.bold};
                  font-size: ${p.theme.fontSize.normal};
                `;
            case 'small':
                return `
                  font-family: ${p.theme.fontFamily.light};
                  font-size: ${p.theme.fontSize.small};
                `;
            case 'subtitle':
                return `
                  font-family: ${p.theme.fontFamily.bold};
                  font-size: ${p.theme.fontSize.subtitle};
                `;
            case 'h1':
                return `
                  font-family: ${p.theme.fontFamily.bold};
                  font-size: ${p.theme.fontSize.h1};
                `;
            case 'h2':
                return `
                  font-family: ${p.theme.fontFamily.bold};
                  font-size: ${p.theme.fontSize.h2};
                `;
            case 'h3':
                return `
                  font-family: ${p.theme.fontFamily.bold};
                  font-size: ${p.theme.fontSize.h3};
                `;
            case 'h4':
                return `
                  font-family: ${p.theme.fontFamily.bold};
                  font-size: ${p.theme.fontSize.h4};
                `;
            case 'h5':
                return `
                  font-family: ${p.theme.fontFamily.regular};
                  font-size: ${p.theme.fontSize.h5};
                `;
            default:
                return `
                  font-family: ${p.theme.fontFamily.regular};
                  font-size: ${p.theme.fontSize.normal};
                `;
        }
    }};
    ${p => (p.center ? `text-align: center; align-self: center;` : `text-align: left; align-self: flex-start;`)};
    ${p => p.bold && `font-family: ${p.theme.fontFamily.bold};`};
`;

/*
  h1 
  h2
  h3
  h4
  h5
  subtitle
  normal
  noramlBold
  small
*/

const CommonText = ({
    children,
    type = 'normal',
    color = 'dark',
    bold = false,
    center = false,
    multiline = true,
    marginTop = 0,
    ...rest
}) => {
    return (
        <Text
            marginTop={marginTop}
            type={type}
            center={center}
            color={color}
            multiline={multiline}
            bold={bold}
            {...rest}
        >
            {children}
        </Text>
    );
};

export default CommonText;
