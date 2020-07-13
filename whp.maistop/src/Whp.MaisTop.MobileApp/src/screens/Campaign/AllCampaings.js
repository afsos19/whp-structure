import * as React from 'react';
import styled from 'styled-components/native';
import { ActivityIndicator, Dimensions } from 'react-native';
import { connect } from 'react-redux';
import { SafeAreaView } from 'react-navigation';
import Carousel from 'react-native-snap-carousel';
import { getCampaignsAction } from '../../redux/actions/campaing';
import CommonText from '../../components/common/CommonText';
import Tab from '../../components/common/Tab';
import Header from '../../components/common/Header';
import MenuModal from '../../components/Home/MenuModal';
import CampaignCard from '../../components/Campaign/CampaignCard';

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

const WrapperText = styled.View`
    align-self: center;
    width: 80%;
`;

class AllCampaings extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        menuVisible: false,
    };

    componentDidMount() {
        const { getCampaigns } = this.props;

        getCampaigns();
        // news.data.filter(
        //     (item, index) => item.ordernation === 1 && this.setState({ indexSpotlight: index })
        // );
    }

    render() {
        const { menuVisible } = this.state;
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
                        <WrapperText>

                            <CommonText type="h3" color="secondary" center marginTop={20}>
                                Campanhas
                            </CommonText>
                            <CommonText type="normal" color="light" center marginTop={20}>
                                Bem-vindo ao histórico de campanhas +TOP!
                            </CommonText>
                            {!campaign.data &&
                                <CommonText type="small" color="light" center marginTop={20}>
                                    Nenhuma campanha encontrada.
                                </CommonText>
                            }
                        </WrapperText>
                        <MarginTop />
                        {campaign.data && 
                            <React.Fragment>
                                <CampaignCard
                                    item={campaign.data[0]}
                                    index={0}
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
                                    data={campaign.data}
                                    renderItem={({ item, index }) => (
                                        <CampaignCard
                                            item={item}
                                            index={index}
                                            navigation={navigation}
                                        />
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
    campaign: state.campaign,
});

const mapDispatchToProps = dispatch => ({
    getCampaigns: () => dispatch(getCampaignsAction()),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(AllCampaings);
