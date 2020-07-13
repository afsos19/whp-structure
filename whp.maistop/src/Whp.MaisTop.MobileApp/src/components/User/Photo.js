import React from 'react';
import styled from 'styled-components/native';
import ImagePicker from 'react-native-image-picker';
import CommonText from '../common/CommonText';
import Button from '../common/Button';
import CircleImage from '../common/CircleImage';

const options = {
    title: 'Selecione uma foto',
    // quality: 0,
    maxWidth: 200,
    maxHeight: 200,
    storageOptions: {
        skipBackup: true,
        path: 'images',
    },
};

const Wrapper = styled.View`
    align-self: center;
    flex-direction: column;
    width: 100%;
    justify-content: center;
    align-items: flex-start;
    padding-horizontal: 20;
    padding-vertical: 20;
`;

const Photo = ({ image, setImage }) => {
    return (
        <Wrapper>
            <CircleImage image={image} />
            <Button
                marginTop={20}
                color="primary"
                text="Carregar foto"
                onPress={() => {
                    ImagePicker.showImagePicker(options, response => {
                        if (response.didCancel) {
                            console.log('User cancelled image picker');
                        } else if (response.error) {
                            console.log('ImagePicker Error: ', response.error);
                        } else if (response.customButton) {
                            console.log('User tapped custom button: ', response.customButton);
                        } else {
                            setImage(response.uri);
                        }
                    });
                }}
            />
            <CommonText type="normal" color="dark" marginTop={10} center>
                ( Formatos aceitos .jpg .png) - 2mb
            </CommonText>
        </Wrapper>
    );
};

export default Photo;
