import React from 'react';
import styled from 'styled-components/native';
import CommonText from '../common/CommonText';
import { imgCampignPath } from '../../utils/urls';
import Button from '../common/Button';
import ROUTENAMES from '../../navigation/routeName';
import AutoHeightImage from 'react-native-auto-height-image';

const Wrapper = styled.View`
    align-self: center;
    width: ${p => p.width};
    padding-bottom: 10;
    border-radius: 9;
    background-color: ${p => p.theme.colors.background};
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
`;

const Thumb = styled(AutoHeightImage)`
    width: ${p => p.width};
`;

const ThumbConatiner = styled.View`
    border-top-right-radius: 9;
    border-top-left-radius: 9;
    overflow: hidden;
`;

const ButtonWrapper = styled.View`
    align-self: flex-end;
`;

const WrapperText = styled.View`
    align-self: center;
    width: 90%;
`;

const CampaignCard = ({
    item,
    index,
    navigation,
    width = 200,
    color = 'primary',
    buttonSize = 'small',
}) => {
    return (
        <Wrapper key={index} width={width}>
            <ThumbConatiner>
                <Thumb
                    width={width}
                    source={{
                        uri: imgCampignPath + item.thumb,
                    }}
                />
            </ThumbConatiner>
            <WrapperText>
                <CommonText type="small" color="dark" numberOfLines={1} marginTop={10} bold>
                    {item.title}
                </CommonText>
                <CommonText type="small" color="dark" marginTop={5} numberOfLines={2}>
                    {item.subTitle}
                </CommonText>
                <ButtonWrapper>
                    <Button
                        text="Ver mais"
                        color={color}
                        marginTop={15}
                        type={buttonSize}
                        onPress={() => navigation.navigate(ROUTENAMES.CAMPAIGN_DETAIL, { index })}
                    />
                </ButtonWrapper>
            </WrapperText>
        </Wrapper>
    );
};

export default CampaignCard;
