import * as React from 'react';
import styled from 'styled-components/native';
import { Dimensions } from 'react-native';
import { connect } from 'react-redux';
import { SafeAreaView } from 'react-navigation';
import Carousel from 'react-native-snap-carousel';
import CommonText from '../../components/common/CommonText';
import Tab from '../../components/common/Tab';
import Header from '../../components/common/Header';
import MenuModal from '../../components/Home/MenuModal';
import NewsCard from '../../components/News/NewsCard';

const imageWidth = Dimensions.get('window').width * 0.9;

const Container = styled.View`
    flex: 1;
    background-color: ${p => p.theme.colors.tertiary};
`;

const Scroll = styled.ScrollView.attrs({
    showsVerticalScrollIndicator: false
})`
    width: 100%;
`;

const MarginTop = styled.View`
    margin-top: 30;
`;

class AllNews extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        menuVisible: false,
        indexSpotlight: 0,
    };

    componentDidMount() {
        const { news } = this.props;

        if (news.data) {
            news.data.filter(
                (item, index) => item.ordernation === 1 && this.setState({ indexSpotlight: index })
            );
        }
    }

    render() {
        const { menuVisible, indexSpotlight } = this.state;
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
                    <CommonText type="h3" color="secondary" center marginTop={20}>
                        Novidades
                    </CommonText>
                    {!news.data &&
                        <CommonText type="small" color="light" center marginTop={20}>
                            Nenhuma novidade encontrada.
                        </CommonText>
                    }
                    {news.data &&
                        <React.Fragment>
                            <MarginTop />
                            <NewsCard
                            item={news.data[indexSpotlight]}
                            index={indexSpotlight}
                            navigation={navigation}
                            width={imageWidth}
                            buttonSize="normal"
                            color="secondary"
                            />
                            <Carousel
                            containerCustomStyle={{
                                marginTop: 20,
                                alignSelf: 'center',
                            }}
                            contentContainerCustomStyle={{ paddingVertical: 20 }}
                            data={news.data}
                            renderItem={({ item, index }) => (
                                <NewsCard item={item} index={index} navigation={navigation} />
                                )}
                                sliderWidth={imageWidth}
                                itemWidth={200}
                                />
                        </React.Fragment>
                    }
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
    news: state.news,
});
export default connect(mapStateToProps)(AllNews);
