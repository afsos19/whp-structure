import * as React from 'react';
import styled from 'styled-components/native';
import { Dimensions } from 'react-native';
import { SafeAreaView } from 'react-navigation';
import Header from '../../components/common/Header';
// import Pdf from 'react-native-pdf';
import PDFView from 'react-native-view-pdf';
// import PDFView from 'react-native-pdf-view';
import CommonText from '../../components/common/CommonText';

const Container = styled.View`
    flex: 1;
    background-color: ${p => p.theme.colors.tertiary};
`;

const Scroll = styled.ScrollView.attrs({
    showsVerticalScrollIndicator: false
})`
    width: 100%;
`;

const PdfWrapper = styled(PDFView)`
    flex:1;
    /* width: ${Dimensions.get('window').width}; */
`;

class PdfScreen extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        source: null,
    };

    componentDidMount() {
        const { navigation } = this.props;
        const source = navigation.getParam('source', '');
        this.setState({ source });
    }

    render() {
        const { source } = this.state;
        const { navigation } = this.props;
        
        return (
            <Container>
                <SafeAreaView />
                <Header navigation={navigation} />
                {source &&
                    <PdfWrapper

                        // ref={(pdf)=>{this.pdfView = pdf;}}
                        // src={source}
                        // onLoadComplete = {(pageCount)=>{
                        //     this.pdfView.setNativeProps({
                        //         zoom: 1.5
                        //     });
                        // }}

                        fadeInDuration={250.0}
                        resource={source}
                        resourceType="url"
                        onLoad={() => console.log(`PDF rendered from url`)}
                        onError={() => console.log('Cannot render PDF', error)}

                        // source={{uri: source, cache:true}}
                        // onLoadComplete={(numberOfPages,filePath)=>{
                        //     console.log(`number of pages: ${numberOfPages}`);
                        // }}
                        // onPageChanged={(page,numberOfPages)=>{
                        //     console.log(`current page: ${page}`);
                        // }}
                        // onError={(error)=>{
                        //     console.log(error);
                        // }}
                        // style={styles.pdf}
                    />
                }
                {!source &&
                    <CommonText type="h4" color="light" center>Erro ao carregar o PFD.</CommonText>
                }
            </Container>
        );
    }
}

export default PdfScreen;
