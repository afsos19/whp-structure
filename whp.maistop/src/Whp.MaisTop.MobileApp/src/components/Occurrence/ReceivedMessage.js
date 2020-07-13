import 'moment';
import moment from 'moment-timezone';
import 'moment/locale/pt-br';
import React from 'react';
import styled from 'styled-components/native';
import CommonText from '../common/CommonText';
import { imgOccurrencePath } from '../../utils/urls';

const ChatText = styled(CommonText)`
    color: ${p => p.theme.colors.chatText};
`;

const ChatDetailText = styled(CommonText)`
    margin-bottom: 5;
`;

const ChatDetailDate = styled(CommonText)`
    margin-top: 5;

    align-self: flex-end;
    text-align: right;
`;

const WrapperMyMessageText = styled.View`
    align-self: center;
    width: 100%;
    justify-content: center;
    align-items: center;
    background-color: ${p => p.theme.colors.background};
    border-radius: 6;
    padding: 10px;
`;

const WrapperMyMessage = styled.View`
    margin-top: 20;
    align-self: flex-start;
    width: 80%;
    justify-content: center;
    align-items: center;
`;

const ThumMessage = styled.Image`
    margin-top: 15;
    background-color: ${p => p.theme.colors.background};
    width: 80%;
    height: 200;
`;

const ReceivedMessage = ({ item }) => {
    return item.file ? (
        <WrapperMyMessage>
            <ChatDetailText color="light" numberOfLines={1}>
                {item.user.name}
            </ChatDetailText>
            <WrapperMyMessageText>
                <ChatText type="normal">{item.message}</ChatText>
                <ThumMessage
                    source={{ uri: `${imgOccurrencePath + item.file}` }}
                    resizeMode="contain"
                />
            </WrapperMyMessageText>
            <ChatDetailDate color="light" type="small">
                {moment(item.createdAt).format('lll')}
            </ChatDetailDate>
        </WrapperMyMessage>
    ) : (
        <WrapperMyMessage>
            <ChatDetailText color="light" numberOfLines={1}>
                {item.user.name}
            </ChatDetailText>
            <WrapperMyMessageText>
                <ChatText type="normal">{item.message}</ChatText>
                {/* {item.file && (
                    <ThumMessage
                        source={{ uri: `${imgOccurrencePath + item.file}` }}
                        resizeMode="contain"
                    />
                )} */}
            </WrapperMyMessageText>
            <ChatDetailDate color="light" type="small">
                {moment(item.createdAt).format('lll')}
            </ChatDetailDate>
        </WrapperMyMessage>
    );
};

export default ReceivedMessage;
