import React from 'react';
import { Dimensions } from 'react-native';
import Carousel from 'react-native-snap-carousel';
import styled from 'styled-components/native';
import Card from '../common/Card';
import CommonText from '../common/CommonText';
import OverviewCard from './OverviewCard';

const sliderWidth = Dimensions.get('window').width;

const Overview = ({ users, sales, training }) => {
    return (
        <Carousel
            containerCustomStyle={{
                marginTop: 20,
                alignSelf: 'center',
            }}
            contentContainerCustomStyle={{ paddingVertical: 20 }}
            data={[users, training, sales]}
            renderItem={({ item, index }) => (
                <OverviewCard item={item} index={index} width={sliderWidth * 0.8} />
            )}
            sliderWidth={sliderWidth}
            itemWidth={sliderWidth * 0.8}
        />
    );
};

export default Overview;
