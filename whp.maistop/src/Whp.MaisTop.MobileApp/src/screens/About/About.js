import * as React from 'react';
import styled from 'styled-components/native';
import { connect } from 'react-redux';
import { AsyncStorage } from 'react-native';
import { WebView } from 'react-native-webview';
import { SafeAreaView } from 'react-navigation';
import jwtDecode from 'jwt-decode';
import CommonText from '../../components/common/CommonText';
import Tab from '../../components/common/Tab';
import Header from '../../components/common/Header';
import MenuModal from '../../components/Home/MenuModal';
import Button from '../../components/common/Button';
import ROUTENAMES from '../../navigation/routeName';
import { pdfRegulation } from '../../utils/urls';

const Container = styled.View`
    flex: 1;
    background-color: ${p => p.theme.colors.tertiary};
`;

const Scroll = styled.ScrollView.attrs({
    showsVerticalScrollIndicator: false
})`
    width: 100%;
`;

class About extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        menuVisible: false,
        video: '',
    };

    componentDidMount() {
        AsyncStorage.getItem('token').then(value => {
            const decoded = jwtDecode(value);
            const role = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
            let video = '';
            switch (role) {
                case 'VENDEDOR':
                    video = 'EZuGI0NUOFU';
                    break;
                case 'GERENTE':
                    video = 'G-9xlT40cU0';
                    break;
                case 'GERENTE REGIONAL':
                    video = 'G-9xlT40cU0';
                    break;
                case 'GESTOR DA INFORMACAO':
                    video = 'EZuGI0NUOFU';
                    break;
                default:
                    video = 'EZuGI0NUOFU';
                    break;
            }
            this.setState({ video });
        });
    }

    render() {
        const { menuVisible, video } = this.state;
        const { navigation, network } = this.props;
        const source = {uri:'http://samples.leanpub.com/thereactnativebook-sample.pdf',cache:true};

        return (
            <Container>
                <SafeAreaView />
                <MenuModal
                    navigation={navigation}
                    visible={menuVisible}
                    close={() => this.setState({ menuVisible: false })}
                />
                <Header navigation={navigation} />
                <Scroll>
                    <CommonText type="h3" color="secondary" center marginTop={20}>
                        Sobre o +TOP
                    </CommonText>

                    <WebView
                        javaScriptEnabled={true}
                        source={{ uri: `https://www.youtube.com/embed/${video}?rel=0&autoplay=0&showinfo=0` }}
                        style={{ backgroundColor: "black", width: "100%", height: 300, marginTop: 32 }}
                    />

                    <Button
                        text="ver regulamento"
                        color="primary"
                        marginTop={40}
                        onPress={() => navigation.navigate(ROUTENAMES.PDF_SCREEN, { source: pdfRegulation(network.data[0].network.siteShortName.replace(/\s/g, '')) })}
                    />
                </Scroll>
                <Tab
                    openMenu={() => this.setState({ menuVisible: true })}
                    navigation={navigation}
                />
            </Container>
        );
    }
}

// Redux
const mapStateToProps = state => ({
    network: state.network,
});

export default connect(mapStateToProps)(About);
