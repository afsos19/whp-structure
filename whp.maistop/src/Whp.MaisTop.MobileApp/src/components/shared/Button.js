import React from 'react';
import { Text, StyleSheet, TouchableOpacity } from 'react-native';

import colors from 'res/colors';

const Button = ({ bgColor, label, onPress, disabled }) => {
    return (
        <TouchableOpacity style={[styles.button, { backgroundColor: bgColor }]} onPress={onPress} disabled={disabled}>
            <Text style={styles.text}>{label.toUpperCase()}</Text>
        </TouchableOpacity>
    );
};

const ButtonPrimary = ({ label, onPress, disabled }) => (
    <Button bgColor={colors.primary} label={label} onPress={onPress} disabled={disabled} />
);
const ButtonSecondary = ({ label, onPress }) => <Button bgColor={colors.secondary} label={label} onPress={onPress} />;

const styles = StyleSheet.create({
    button: {
        justifyContent: 'center',
        alignItems: 'center',
        borderRadius: 17,
        width: 133,
        height: 35.5,
        marginHorizontal: 3,
    },
    text: {
        fontFamily: 'Montserrat-Regular',
        fontSize: 10,
        color: 'white',
        textAlignVertical: 'center',
    },
});

export { ButtonPrimary, ButtonSecondary };
