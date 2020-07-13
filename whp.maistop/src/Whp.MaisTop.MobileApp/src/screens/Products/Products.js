import * as React from 'react';
import styled from 'styled-components/native';
import { connect } from 'react-redux';
import { SafeAreaView } from 'react-navigation';
import { getFocusProductsAction, getAllProductsAction } from '../../redux/actions/products';
import GradientBackground from '../../components/common/GradientBackground';
import CommonText from '../../components/common/CommonText';
import Tab from '../../components/common/Tab';
import Header from '../../components/common/Header';
import MenuModal from '../../components/Home/MenuModal';
import Card from '../../components/common/Card';
import { returnMonth } from '../../utils/date';
import FocusProduct from '../../components/Products/FocusProduct';
import ProductLine from '../../components/Products/ProductLine';
import images from '../../res/images';

const Container = styled(GradientBackground)`
    flex: 1;
`;

const WrapperText = styled.View`
    align-self: center;
    width: 90%;
`;

const WrapperTextHeader = styled.View`
    flex-direction: row;
    align-self: center;
    justify-content: space-between;
    width: 100%;
`;

const WrapperAllText = styled.View`
    align-self: center;
    width: 80%;
`;

const Scroll = styled.ScrollView.attrs({
    showsVerticalScrollIndicator: false
})`
    width: 100%;
`;

const WrapperFocusProducts = styled.View`
    align-self: center;
    margin-top: 20;
    width: 100%;
`;

const WrapperAllProducts = styled.View`
    align-self: center;
    padding-top: 20;
    padding-bottom: 40;
    width: 100%;
    background-color: ${p => p.theme.colors.productsBackground};
`;

const DarkSide = styled.View`
    position: absolute;
    top: 0;
    bottom: 1;
    right: 0;
    width: 20%;
    padding-bottom: 1;
    background-color: rgba(93, 89, 89, 0.43);
`;

const BrandsImage = styled.Image`
    margin-top: 20;
    height: 10;
    width: 90;
`;

const WrapperLine = styled.View`
    align-self: center;
    flex-direction: row;
    justify-content: space-between;
    margin-top: 20;
    width: 80%;
`;

class Products extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        menuVisible: false,
    };

    render() {
        const { menuVisible } = this.state;
        const { navigation, products } = this.props;

        return (
            <Container isLoading={products.isLoading}>
                <MenuModal
                    navigation={navigation}
                    visible={menuVisible}
                    close={() => this.setState({ menuVisible: false })}
                />
                <Header navigation={navigation} />
                <Scroll>
                    {products.groupFocusProducts && products.groupAllProducts && (
                        <React.Fragment>
                            <Card marginBottom={40} width="90%" marginTop={10}>
                                <WrapperText>
                                    <WrapperTextHeader>
                                        <CommonText type="h5" color="secondary" bold>
                                            Supertops
                                        </CommonText>
                                        <CommonText type="h5" color="secondary" bold>
                                            20 pontos cada
                                        </CommonText>
                                    </WrapperTextHeader>
                                    <CommonText type="normal" color="darl">
                                        {products.groupFocusProducts.length > 0 &&
                                            `${returnMonth(products.groupFocusProducts[0].currentMonth)}/${products.groupFocusProducts[0].currentYear}`
                                        }
                                    </CommonText>
                                    <WrapperFocusProducts>
                                        {products.groupFocusProducts.map(focusProduct => {
                                            return (
                                                <FocusProduct
                                                    key={focusProduct.id}
                                                    product={focusProduct}
                                                    allFocus={products.focusProducts}
                                                />
                                            );
                                        })}
                                    </WrapperFocusProducts>
                                </WrapperText>
                            </Card>

                            <WrapperAllProducts>
                                <DarkSide />
                                <WrapperAllText>
                                    <CommonText type="h4" color="secondary">
                                        Produtos participantes
                                    </CommonText>

                                    <BrandsImage source={images.brands} resizeMode="contain" />
                                </WrapperAllText>

                                <WrapperLine>
                                    <CommonText type="normal" color="light">
                                        TIPO
                                    </CommonText>
                                    <CommonText type="normal" color="light">
                                        PTS
                                    </CommonText>
                                </WrapperLine>
                                {products.groupAllProducts.map(item => {
                                    return <ProductLine key={item.id} item={item} />;
                                })}
                            </WrapperAllProducts>
                        </React.Fragment>
                    )}
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
    products: state.products,
});

const mapDispatchToProps = dispatch => ({
    getFocusProducts: () => dispatch(getFocusProductsAction()),
    getAllProducts: () => dispatch(getAllProductsAction()),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Products);
