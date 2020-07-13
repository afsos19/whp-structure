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
import { imgNewsPath } from '../../utils/urls';
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

const NewsImage = styled(AutoHeightImage)`
    align-self: center;
    width: ${imageWidth};
    margin-bottom: 16;
`;

class NewsDetail extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        newsIndex: null,
        menuVisible: false,
        video: false,
        videoTag: null,
    };

    componentDidMount() {
        const { navigation, news } = this.props;

        const newsIndex = navigation.getParam('newsIndex', 0);
        this.setState({ newsIndex });

        const data = news.data[newsIndex].description;

        if (data.match(/src="([^"]+)"/)) {
            const pieces = data.match(/src="([^"]+)"/)[1].split('/');
            const videoTag = pieces[pieces.length - 1];
            this.setState({ video: true, videoTag });
        }
    }

    render() {
        const { menuVisible, newsIndex, video, videoTag } = this.state;
        const { navigation, news } = this.props;

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
                        {news.data[newsIndex] ? (
                            <React.Fragment>
                                <CommonText type="h4" color="secondary" center>
                                    {news.data[newsIndex].title}
                                </CommonText>
                                    <React.Fragment>
                                        <NewsImage
                                            source={{
                                                uri: imgNewsPath + news.data[newsIndex].photo,
                                            }}
                                            width={imageWidth}
                                            // resizeMode="cover"
                                        />
                                        <WrapperText>
                                            <HTML 
                                                baseFontStyle={{ fontFamily: "Montserrat-Medium", fontSize: 13, color: '#AFAFAF' }}
                                                ignoredStyles={["font-family", "letter-spacing"]}
                                                tagsStyles={{
                                                    p:{ fontSize: 13, color: '#AFAFAF' },
                                                    span:{ fontSize: 13, color: '#AFAFAF' },
                                                }}
                                                html={`${news.data[newsIndex].description}`} imagesMaxWidth={imageWidth}
                                            />
                                        </WrapperText>
                                    </React.Fragment>
                                
                            </React.Fragment>
                        ) : (
                            <CommonText type="h4" color="secondary" center>
                                Ops, não conseguimos carregar a novidade.
                            </CommonText>
                        )}
                    </Card>
                </Scroll>
                <Tab openMenu={() => this.setState({ menuVisible: true })} navigation={navigation} />
            </Container>
        );
    }
}

// Redux
const mapStateToProps = state => ({
    news: state.news,
});
export default connect(mapStateToProps)(NewsDetail);
