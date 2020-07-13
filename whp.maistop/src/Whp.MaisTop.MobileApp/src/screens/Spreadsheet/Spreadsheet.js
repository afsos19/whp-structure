import * as React from 'react';
import styled from 'styled-components/native';
import { Dimensions } from 'react-native';
import { connect } from 'react-redux';
import { SafeAreaView } from 'react-navigation';
import CommonText from '../../components/common/CommonText';
import Card from '../../components/common/Card';
import Tab from '../../components/common/Tab';
import Header from '../../components/common/Header';
import MenuModal from '../../components/Home/MenuModal';
import { imgNewsPath } from '../../utils/urls';
import images from '../../res/images';
import CardStep from '../../components/Spreadsheet/CardStep';
import LinearButton from '../../components/Spreadsheet/LinearButton';
import ROUTENAMES from '../../navigation/routeName';

const imageWidth = Dimensions.get('window').width * 0.9;

const Container = styled.View`
    flex: 1;
    background-color: ${p => p.theme.colors.background};
`;

const WrapperSteps = styled.View`
    margin-top: 30;
`;

const Scroll = styled.ScrollView.attrs({
    showsVerticalScrollIndicator: false
})`
    width: 100%;
`;

const CardCalendar = styled.View`
    margin-top: 20;
    align-self: center;
    justify-content: center;
    width: 90%;
    padding-horizontal: 10;
    padding-vertical: 10;
    background-color: ${p => p.theme.colors.background};
    border-radius: 6;
    border-width: 0.5;
    border-color: ${p => p.theme.colors.borderMyTeam};
`;

const WrapperTextEnd = styled.View`
    margin-vertical: 20;
    align-self: center;
    align-self: center;
    justify-content: center;
    width: 70%;
`;

const ImageCalendar = styled.Image`
    align-self: center;
    width: 65;
    height: 65;
`;

class Spreadsheet extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        menuVisible: false,
    };

    render() {
        const { menuVisible } = this.state;
        const { navigation } = this.props;

        return (
            <Container>
                {/* <SafeAreaView /> */}
                <MenuModal
                    navigation={navigation}
                    visible={menuVisible}
                    close={() => this.setState({ menuVisible: false })}
                />
                <Header navigation={navigation} color safeArea />
                <Scroll>
                    <CommonText type="h4" color="secondary" center>
                        Novo fluxo de informações
                    </CommonText>
                    <CommonText type="normal" color="dark" center marginTop={10}>
                        Veja o passo a passo para publicar planilhas e garantir sua pontuação mensal.
                    </CommonText>

                    <WrapperSteps />
                    <CardStep
                        number="1"
                        title="Acesso pelo site +TOP"
                        text={
                            <CommonText type="normal" color="dark" center>
                                Entre com seu login e senha e coloque na opção<CommonText type="normal" color="dark" bold> “publicar planilha”</CommonText>.
                            </CommonText>
                        }
                        image={images.spreadsheetStep1}
                    />
                    <CardStep
                        number="2"
                        title="Envie o arquivo de funcionários"
                        text={
                            <CommonText type="normal" color="dark" center>
                                O arquivo passará por uma validação do sistema em caso de aprovação, você receberá uma mensagem e a publicação do<CommonText type="normal" color="dark" bold> arquivo de vendas</CommonText> será liberada.
                            </CommonText>
                        }
                        image={images.spreadsheetStep2}
                    />
                    <CardStep
                        number="3"
                        title="Envie o arquivo de vendas"
                        text={
                            <CommonText type="normal" color="dark" center>
                                Novamente, o sistema avaliará o correto preenchimento das planilhas, se o arquivo for aprovado, você será informado por mensagem.<CommonText type="normal" color="dark" bold> Fique ligado!</CommonText>
                            </CommonText>
                        }
                        image={images.spreadsheetStep3}
                    />
                    <CardStep
                        number="4"
                        title="Processamento de pontos"
                        text={
                            <CommonText type="normal" color="dark" center>
                                O progresso foi finalizado com sucesso e os pontos dos participantes foram distribuídos!
                            </CommonText>
                        }
                        image={images.spreadsheetStep4}
                    />
                    <CommonText type="normal" color="dark" center marginTop={15}>
                        *Fluxo válido para publicação de planilhas a partir de junho de /2017 (fechamento Maio/2017).
                    </CommonText>
                    <CardCalendar>
                        <ImageCalendar source={images.spreadsheetCalendar} resizeMode="contain" />
                        <CommonText type="h4" color="primary" center marginTop={15}>
                            De olho no prazo
                        </CommonText>
                        <CommonText type="h5" color="dark" center>
                            Publique suas planilhas até o{' '}
                            <CommonText type="h5" color="dark" center bold>
                                5º dia útil
                            </CommonText>
                        </CommonText>
                        <CommonText type="h5" color="dark" center>
                            útil do mês subsequente e ganhe{' '}
                            <CommonText type="h5" color="dark" center bold>
                                200 pontos
                            </CommonText>
                            !
                        </CommonText>
                    </CardCalendar>
                    <LinearButton
                        color="primary"
                        image={images.spreadsheetEmployees}
                        text="ENVIAR ARQUIVOS DE FUNCIONÁRIOS"
                        onPress={() => navigation.navigate(ROUTENAMES.EMPLOYEES_FILE)}
                    />
                    <LinearButton
                        color="secondary"
                        image={images.spreadsheetSales}
                        text="ENVIAR ARQUIVOS DE VENDAS"
                        onPress={() => navigation.navigate(ROUTENAMES.SALES_FILE)}
                    />
                    <WrapperTextEnd>
                        <CommonText type="normal" color="dark" center marginTop={15}>
                            Dúvidas? Fale com o Atendimento ao Gestor da Informação: (11) 9.7961-6909 e-mail:
                            atendimentogi@programamaistop.com.br
                        </CommonText>
                    </WrapperTextEnd>
                </Scroll>
                <Tab openMenu={() => this.setState({ menuVisible: true })} navigation={navigation} />
            </Container>
        );
    }
}

export default Spreadsheet;
