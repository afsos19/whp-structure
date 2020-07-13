import React, { Component } from 'react';
import { AsyncStorage, View } from 'react-native';
import { ThemeProvider } from 'styled-components';
import { Provider } from 'react-redux';
import createRootNavigator from './navigation/router';
import theme from './theme';
import store from './redux';
import firebase from 'react-native-firebase'
import NavigationService from './utils/NavigationService';

console.disableYellowBox = true;

export default class App extends Component {
    state = {
        token: '',
        isTokenRetrieved: false,
        isMgmUser: false,
        urlRetrieved: false,
    };

    componentDidMount() {
        // AsyncStorage.clear();
        AsyncStorage.getItem('token').then(value => {
            this.setState({
                token: value,
                isTokenRetrieved: true,
            });
        });
        firebase.links()
            .getInitialLink()
            .then((url) => {
                if (url) {
                    AsyncStorage.clear();
                    this.setState({ isMgmUser: true, urlRetrieved: true, token: '' });
                } else {
                    // app NOT opened from a url
                    this.setState({ urlRetrieved: true });
                    // console.log("app NOT opened from a url")
                }
            });
    }
    
    render() {
        const { isTokenRetrieved, urlRetrieved, token, isMgmUser } = this.state;

        const Launch = createRootNavigator(token, isMgmUser);
        return (
            <ThemeProvider theme={theme}>
                <Provider store={store}>{isTokenRetrieved && urlRetrieved ? 
                    <Launch ref={navigatorRef => { NavigationService.setTopLevelNavigator(navigatorRef);}} /> : 
                    <View />
                }
                </Provider>
            </ThemeProvider>
        );
    }
}
