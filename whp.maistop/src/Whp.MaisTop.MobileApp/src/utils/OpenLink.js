import { Linking } from 'react-native';

const openBrowser = url => {
    if (url) {
        Linking.canOpenURL(url).then(supported => {
            if (supported) {
                Linking.openURL(url);
            }
        });
    }
};

export default openBrowser;
