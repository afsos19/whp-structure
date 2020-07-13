import React from 'react';
import { Linking, Text } from 'react-native';

const LinkAction = ({ type, value, style }) => {
    const link = () => {
        Linking.openURL(`${type}:${value}`).catch(err => console.error('An error occurred', err));
    };

    return (
        <Text onPress={link} style={style}>
            {value}
        </Text>
    );
};

export default LinkAction;
