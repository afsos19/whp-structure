import * as React from 'react';
import styled from 'styled-components/native';
import { Dimensions } from 'react-native';
import { connect } from 'react-redux';
import { SafeAreaView } from 'react-navigation';
import CommonText from '../../components/common/CommonText';
import Card from '../../components/common/Card';
import Tab from '../../components/common/Tab';
import Header from '../../components/common/Header';
import MenuModal from '../../components/Home/MenuModal';
import { imgCampignPath } from '../../utils/urls';
import AutoHeightImage from 'react-native-auto-height-image';
import HTML from 'react-native-render-html';

const imageWidth = Dimensions.get('window').width * 0.9;

const Container = styled.View`
    flex: 1;
    background-color: ${p => p.theme.colors.quaternaryBackground};
`;

const Scroll = styled.ScrollView.attrs({
    showsVerticalScrollIndicator: false
})`
    width: 100%;
`;

const WrapperText = styled.View`
    align-self: center;
    width: 90%;
`;

const CampaignImage = styled(AutoHeightImage)`
    align-self: center;
    width: ${imageWidth};
    margin-bottom: 16;
`;

class CampaignDetail extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        index: null,
        menuVisible: false,
        video: false,
        videoTag: null,
    };

    componentDidMount() {
        const { navigation } = this.props;

        const index = navigation.getParam('index', 0);
        this.setState({ index });
    }

    render() {
        const { menuVisible, index } = this.state;
        const { navigation, campaign } = this.props;

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
                    <Card marginBottom={40} width="90%" marginTop={10}>
                        {campaign.data[index] ? (
                            <React.Fragment>
                                <CommonText type="h4" color="secondary" center>
                                    {campaign.data[index].title}
                                </CommonText>

                                <CampaignImage
                                    source={{
                                        uri: imgCampignPath + campaign.data[index].photo,
                                    }}
                                    width={imageWidth}
                                />
                                <WrapperText>
                                    <HTML 
                                        baseFontStyle={{ fontFamily: "Montserrat-Medium", fontSize: 13, color: '#AFAFAF' }}
                                        ignoredStyles={["font-family", "letter-spacing"]}
                                        tagsStyles={{
                                            p:{ fontSize: 13, color: '#AFAFAF' },
                                            span:{ fontSize: 13, color: '#AFAFAF' },
                                        }}
                                        html={campaign.data[index].description} imagesMaxWidth={imageWidth}
                                    />

                                    {/* <CommonText type="normal" color="dark" marginTop={15}>
                                        {campaign.data[index].description.replace(
                                            /<(?:.|\n)*?>/gm,
                                            ''
                                        ).replace(/&nbsp;/g, ' ')}
                                    </CommonText> */}
                                </WrapperText>
                            </React.Fragment>
                        ) : (
                            <CommonText type="h4" color="secondary" center>
                                Ops, não conseguimos carregar a novidade.
                            </CommonText>
                        )}
                    </Card>
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
    campaign: state.campaign,
});
export default connect(mapStateToProps)(CampaignDetail);
