import * as React from 'react';
import styled from 'styled-components/native';
import HTML from 'react-native-render-html';
import { SafeAreaView } from 'react-navigation';
import Accordion from 'react-native-collapsible/Accordion';
import Icon from 'react-native-vector-icons/AntDesign';
import GradientBackground from '../../components/common/GradientBackground';
import CommonText from '../../components/common/CommonText';
import Tab from '../../components/common/Tab';
import Header from '../../components/common/Header';
import MenuModal from '../../components/Home/MenuModal';
import Card from '../../components/common/Card';
import faq from '../../utils/faq';

const Container = styled(GradientBackground)`
    flex: 1;
`;

const Scroll = styled.ScrollView.attrs({
    showsVerticalScrollIndicator: false
})`
    width: 100%;
`;

const TitleWrapper = styled.View`
    align-self: center;
    width: 70%;
`;

const HeaderWrapper = styled.View`
    align-self: center;
    width: 80%;
`;

const HeaderList = styled.View`
    margin-top: 15;
    flex-direction: row;
    width: 90%;
    align-self: center;
    align-items: center;
    justify-content: space-between;
    padding-vertical: 20;
    padding-horizontal: 10;
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
    background-color: ${p =>
        p.active ? p.theme.colors.secondary : p.theme.colors.secondaryBackground};
    ${p =>
        !p.active
            ? `
                border-width: 1;
                border-radius: 6;
                border-color: ${p.theme.colors.border};`
            : `
                border-top-right-radius: 6;
                border-top-left-radius: 6;
            `};
`;

const ContentList = styled.View`
    width: 90%;
    padding-vertical: 10;
    align-self: center;
    align-items: center;
    justify-content: center;
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
    background-color: ${p => p.theme.colors.background};
    border-bottom-right-radius: 6;
    border-bottom-left-radius: 6;
`;

const PlusIcon = styled(Icon).attrs({
    name: 'plus',
    size: 25,
    color: p => p.theme.colors.primary,
})`
    align-self: center;
`;

const OkayIcon = styled(Icon).attrs({
    name: 'check',
    size: 25,
    color: p => p.theme.colors.iconLight,
})`
    align-self: center;
`;

class Faq extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        menuVisible: false,
        activeSections: [],
    };

    _renderHeader = (section, index) => {
        const { activeSections } = this.state;
        return (
            <HeaderList active={activeSections.includes(index)}>
                <HeaderWrapper>
                    <CommonText
                        type="normal"
                        color={activeSections.includes(index) ? 'light' : 'dark'}
                    >
                        {section.label}
                    </CommonText>
                </HeaderWrapper>
                {activeSections.includes(index) ? <OkayIcon /> : <PlusIcon />}
            </HeaderList>
        );
    };

    _renderContent = section => {
        return (
            <ContentList>
                <HeaderWrapper>
                    <HTML 
                        baseFontStyle={{ fontFamily: "Montserrat-Medium", fontSize: 13, color: '#AFAFAF' }}
                        ignoredStyles={["font-family", "letter-spacing"]}
                        tagsStyles={{
                            p:{ fontSize: 13, color: '#AFAFAF' },
                            span:{ fontSize: 13, color: '#AFAFAF' },
                        }}
                        html={section.value}
                    />
                    {/* <CommonText type="normal" color="dark" center>
                        {section.value.replace(/<(?:.|\n)*?>/gm, '')}
                    </CommonText> */}
                </HeaderWrapper>
            </ContentList>
        );
    };

    _updateSections = activeSections => {
        this.setState({ activeSections });
    };

    render() {
        const { menuVisible } = this.state;
        const { navigation } = this.props;

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
                    <Card marginBottom={40} width="90%" marginTop={20}>
                        <TitleWrapper>
                            <CommonText type="h4" color="primary" center>
                                Veja as dúvidas frequentes sobre o programa +TOP
                            </CommonText>
                        </TitleWrapper>
                        <Accordion
                            underlayColor="transparent"
                            sections={faq}
                            activeSections={this.state.activeSections}
                            // renderSectionTitle={this._renderSectionTitle}
                            renderHeader={this._renderHeader}
                            renderContent={this._renderContent}
                            onChange={this._updateSections}
                        />
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

export default Faq;
