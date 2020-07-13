import * as React from 'react';
import { ActivityIndicator, KeyboardAvoidingView, Platform } from 'react-native';
import styled from 'styled-components/native';
import { connect } from 'react-redux';
import { SafeAreaView } from 'react-navigation';
import { getMsgOccurrenceAction, sendMsgOccurrenceAction } from '../../redux/actions/occurrence';
import CommonText from '../../components/common/CommonText';
import Header from '../../components/common/Header';
import MenuModal from '../../components/Home/MenuModal';
import InputChat from '../../components/Occurrence/InputChat';
import MyMessage from '../../components/Occurrence/MyMessage';
import ReceivedMessage from '../../components/Occurrence/ReceivedMessage';

const Container = styled.KeyboardAvoidingView`
    flex: 1;
    background-color: ${p => p.theme.colors.tertiary};
    width: 100%;
`;

const Scroll = styled.ScrollView.attrs({
    showsVerticalScrollIndicator: false
})`
    align-self: center;
    width: 80%;
`;

const List = styled.FlatList`
    align-self: center;
    width: 100%;
    padding-bottom: 15;
`;

class OccurrenceDetail extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        menuVisible: false,
        occurrenceItem: null,
    };

    componentDidMount() {
        const { navigation, getMsgOccurrence } = this.props;

        const occurrenceItem = navigation.getParam('item', null);
        this.setState({ occurrenceItem });
        getMsgOccurrence(occurrenceItem.id);
    }

    handleSendMsg = (msg, attachment) => {
        const { occurrenceItem } = this.state;
        const { sendMsgOccurrence } = this.props;

        const formdata = new FormData();

        formdata.append('Occurrence.Id', occurrenceItem.id);
        formdata.append('OccurrenceMessageTypeId', 2);
        formdata.append('Message', msg);
        // start FornOne
        if (attachment) {
            formdata.append('formFile', {
                uri: attachment,
                name: 'image.jpg',
                type: 'multipart/form-data',
            });
        }
        sendMsgOccurrence(formdata);
    };

    render() {
        const { menuVisible, occurrenceItem } = this.state;
        const { occurrence, navigation } = this.props;

        return (
            <Container behavior={Platform.OS === 'ios' && 'padding'} enabled>
                <SafeAreaView />
                <MenuModal
                    navigation={navigation}
                    visible={menuVisible}
                    close={() => this.setState({ menuVisible: false })}
                />
                <Header navigation={navigation} />
                {occurrence.isLoading ? (
                    <ActivityIndicator />
                ) : (
                    <React.Fragment>
                        <Scroll>
                            <CommonText type="h4" color="secondary">
                                Chamado
                            </CommonText>
                            <CommonText type="h4" color="secondary">
                                {occurrenceItem && occurrenceItem.code}
                            </CommonText>
                            <CommonText type="normal" color="light">
                                {occurrenceItem &&
                                occurrenceItem.occurrenceStatus.description === 'FECHADO'
                                    ? 'Finalizado'
                                    : 'Em andamento'}
                                -
                            </CommonText>
                            {occurrence.messages && (
                                <List
                                    ref="myFlatListRef"
                                    onLayout={() => {
                                        this.refs.myFlatListRef.scrollToEnd({ animated: true });
                                    }}
                                    onContentSizeChange={() => {
                                        this.refs.myFlatListRef.scrollToEnd({ animated: true });
                                    }}
                                    data={occurrence.messages}
                                    renderItem={
                                        ({ item }) => {
                                            if (item.internal) {
                                                return null
                                            }
                                            if (
                                                item.occurrenceMessageType.description ===
                                                'PARTICIPANTE'
                                            ) {
                                                return <MyMessage item={item} />;
                                            }
                                            return <ReceivedMessage item={item} />;
                                        }
                                    }
                                />
                            )}
                        </Scroll>
                        <InputChat
                            placeholder="Digite aqui sua mensagem"
                            isLoading={occurrence.isLoadingMsg}
                            sendMsg={(msg, attachment) => this.handleSendMsg(msg, attachment)}
                        />
                    </React.Fragment>
                )}
            </Container>
        );
    }
}

// Redux
const mapStateToProps = state => ({
    occurrence: state.occurrence,
});

const mapDispatchToProps = dispatch => ({
    getMsgOccurrence: id => dispatch(getMsgOccurrenceAction(id)),
    sendMsgOccurrence: data => dispatch(sendMsgOccurrenceAction(data)),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(OccurrenceDetail);
