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
    color: ${p => p.theme.colors.chatMyColor};
    margin-bottom: 5;
    align-self: flex-end;
    text-align: right;
`;

const ChatDetailDate = styled(CommonText)`
    margin-top: 5;
    color: ${p => p.theme.colors.chatMyColor};
`;

const WrapperMyMessageText = styled.View`
    align-self: center;
    width: 100%;
    justify-content: center;
    align-items: center;
    background-color: ${p => p.theme.colors.chatMyColor};
    border-radius: 6;
    padding: 10px;
`;

const WrapperMyMessage = styled.View`
    margin-top: 20;
    align-self: flex-end;
    width: 80%;
    justify-content: center;
    align-items: center;
`;

const ThumMessage = styled.Image`
    margin-top: 15;
    background-color: ${p => p.theme.colors.chatMyColor};
    width: 80%;
    height: 200;
`;

const MyMessage = ({ item }) => {
    return (
        item &&
            <WrapperMyMessage>
                <ChatDetailText numberOfLines={1}>{item.user.name}</ChatDetailText>
                <WrapperMyMessageText>
                    <ChatText type="normal">{item.message}</ChatText>
                    {item.file !== "" && (
                        <ThumMessage
                            source={{ uri: `${imgOccurrencePath + item.file}` }}
                            resizeMode="contain"
                            onError={(e) => { console.log("Error img> ", e) }}
                        />
                    )}
                </WrapperMyMessageText>
                <ChatDetailDate type="small">{moment(item.createdAt).format('lll')}</ChatDetailDate>
            </WrapperMyMessage>
        
    );
};

export default MyMessage;
