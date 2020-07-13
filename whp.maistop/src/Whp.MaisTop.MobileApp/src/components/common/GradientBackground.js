import React from 'react';
import styled from 'styled-components/native';
import LinearGradient from 'react-native-linear-gradient';
import { SafeAreaView } from 'react-navigation';
import { ActivityIndicator } from 'react-native';

const Wrapper = styled(LinearGradient).attrs({
    colors: p => [p.theme.colors.gradientDarkStart, p.theme.colors.gradientDarkEnd],
})`
    flex: 1;
`;

const GradientBackground = ({ children, isLoading }) => {
    return (
        <Wrapper>
            <SafeAreaView />
            {isLoading ? <ActivityIndicator /> : children}
        </Wrapper>
    );
};

export default GradientBackground;
