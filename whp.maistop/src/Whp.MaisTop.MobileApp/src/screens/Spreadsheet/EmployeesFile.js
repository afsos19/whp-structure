import * as React from 'react';
import styled from 'styled-components/native';
import { connect } from 'react-redux';
import { Platform } from 'react-native';
import { DocumentPicker, DocumentPickerUtil } from 'react-native-document-picker';
import { getEmployeesFileAction, sendEmployeesFileAction } from '../../redux/actions/employeesFile';
import CommonText from '../../components/common/CommonText';
import InputOptions from '../../components/common/InputOptions';
import Tab from '../../components/common/Tab';
import Header from '../../components/common/Header';
import MenuModal from '../../components/Home/MenuModal';
import Button from '../../components/common/Button';
import images from '../../res/images';
import { allMonths, allYears } from '../../utils/date';
import EmployeesStep from '../../components/Spreadsheet/EmployeesStep';
import ProgressBar from '../../components/Spreadsheet/ProgressBar';

const Container = styled.View`
    flex: 1;
    background-color: ${p => p.theme.colors.background};
`;

const Scroll = styled.ScrollView.attrs({
    showsVerticalScrollIndicator: false
})`
    width: 100%;
`;

const CardWarning = styled.View`
    margin-top: 30;
    align-self: center;
    justify-content: center;
    width: 90%;
    padding-horizontal: 10;
    padding-vertical: 10;
    background-color: ${p => p.theme.colors.background};
    border-radius: 6;
    border-width: 0.5;
    border-color: ${p => p.theme.colors.primary};
`;

const WrapperText = styled.View`
    align-self: center;
    align-self: center;
    justify-content: center;
    width: 70%;
`;

const ImageWaning = styled.Image`
    align-self: center;
    width: 65;
    height: 65;
`;

const InputsRow = styled.View`
    align-self: center;
    width: 100%;
    justify-content: space-around;
    flex-direction: row;
`;

const ButtonsRow = styled.View`
    margin-top: 20;
    align-self: center;
    width: 90%;
    justify-content: flex-end;
    align-items: flex-end;
    flex-direction: row;
`;

const CardSearch = styled.View`
    align-self: center;
    margin-top: 30;
    padding-bottom: 10;
    shadow-color: ${p => p.theme.colors.shadow};
    shadow-offset: {width: 0, height: 1};
    shadow-opacity: 0.15;
    shadow-radius: 4;
    elevation: 1;
    width: 90%;
    padding-horizontal: 20;
    background-color: ${p => p.theme.colors.background};
    border-radius: 6;
`;
const WrapperImport = styled.View`
    align-self: center;
    width: 80%;
    margin-top: 20;
`;

const WrapperFileSteps = styled.View`
    align-self: center;
    flex-direction: row;
    align-items: center;
    justify-content: space-around;
    margin-top: 20;
`;

class EmployeesFile extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        menuVisible: false,
        month: '',
        year: '',
        months: [],
        years: [],
    };

    componentDidMount() {
        const months = allMonths();
        const years = allYears();

        this.setState({ months, years });
    }

    handleSearch = () => {
        const { month, year } = this.state;
        const { getEmployeesFile } = this.props;
        getEmployeesFile({
            currentMonth: month,
            currentYear: year,
        });
    };

    handleFile = () => {
        DocumentPicker.show(
            {
                filetype: [DocumentPickerUtil.allFiles()],
            },
            (error, res) => {
                const { sendEmployeesFile } = this.props;
                const { month, year } = this.state;

                const realPath =
                    Platform.OS === 'ios'
                        ? res.uri.replace('file://', '')
                        : res.uri.split('raw%3A')[1].replace(/\%2F/gm, '/');
                const formdata = new FormData();

                formdata.append('CurrentMonth', month);
                formdata.append('CurrentYear', year);
                formdata.append('formFile', {
                    uri: realPath,
                    name: res.fileName,
                    type: 'multipart/form-data',
                });
                sendEmployeesFile(formdata);
            }
        );
    };

    handlePercent = name => {
        switch (name) {
            case 'PENDENTE':
                return 0;
            case 'EM ANDAMENTO':
                return 50;
            case 'FINALIZADO COM SUCESSO':
                return 100;
            default:
                return 0;
        }
    };

    render() {
        const { menuVisible, month, year, months, years } = this.state;
        const { navigation, employeesFile } = this.props;

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
                    <WrapperText>
                        <CommonText type="h4" color="primary" center>
                            Siga as etapas abaixo para importar o arquivo de funcionários:
                        </CommonText>
                        <CommonText type="normal" color="dark" center margin marginTop={15}>
                            1. SELECIONE O MÊS CORRESPONDENTE AO ARQUIVO QUE SERÁ ENVIADO;{' '}
                        </CommonText>
                        <CommonText type="normal" color="dark" center marginTop={15}>
                            2. CLIQUE EM “BUSCAR” PARA IMPORTAR O ARQUIVO;
                        </CommonText>
                        <CommonText type="normal" color="dark" center marginTop={15}>
                            3. AGUARDE A VALIDAÇÃO DO SISTEMA (NOTIFICAÇÃO VIA E-MAIL).
                        </CommonText>
                    </WrapperText>
                    <CardWarning>
                        <ImageWaning source={images.spreadsheetWraning} resizeMode="contain" />
                        <CommonText type="normal" color="dark" center>
                            <CommonText type="normal" color="dark" center bold>
                                ATENÇÃO:{' '}
                            </CommonText>
                            em caso de erro detectado pelo sistema, faça os ajustes solicitados e repita a importação do
                            arquivo.
                        </CommonText>
                    </CardWarning>
                    <CardSearch>
                        <CommonText type="normal" color="dark" bold marginTop={10}>
                            Dados de importação
                        </CommonText>
                        <InputsRow>
                            <InputOptions
                                width="50%"
                                onChange={m => this.setState({ month: m })}
                                value={month}
                                items={months}
                            />
                            <InputOptions
                                width="40%"
                                onChange={y => this.setState({ year: y })}
                                value={year}
                                items={years}
                            />
                        </InputsRow>
                    </CardSearch>
                    <ButtonsRow>
                        <Button
                            text="BUSCAR"
                            color="primary"
                            disabled={month === '' || year === ''}
                            onPress={() => this.handleSearch()}
                        />
                        <Button text="VOLTAR" color="dark" />
                    </ButtonsRow>
                    {employeesFile.error && (
                        <WrapperImport>
                            <CommonText type="normal" color="dark" center>
                                Não encontramos nenhum arquivo importado para esta data. Importar ?
                            </CommonText>
                            <Button
                                text="Selecionar arquivo"
                                color="primary"
                                marginTop={15}
                                onPress={() => this.handleFile()}
                            />
                        </WrapperImport>
                    )}
                    {employeesFile.success && employeesFile.data && (
                        <React.Fragment>
                            <WrapperFileSteps>
                                <EmployeesStep
                                    image={
                                        employeesFile.data.fileStatus.description === 'PENDENTE'
                                            ? images.salesLight1
                                            : images.salesDark1
                                    }
                                    text="UPLOAD"
                                    selected={employeesFile.data.fileStatus.description === 'PENDENTE'}
                                />
                                <EmployeesStep
                                    image={
                                        employeesFile.data.fileStatus.description === 'EM ANDAMENTO'
                                            ? images.salesLight2
                                            : images.salesDark2
                                    }
                                    text="VALIDANDO DADOS"
                                    selected
                                    selected={employeesFile.data.fileStatus.description === 'EM ANDAMENTO'}
                                />
                                <EmployeesStep
                                    image={
                                        employeesFile.data.fileStatus.description === 'FINALIZADO COM SUCESSO'
                                            ? images.salesLight3
                                            : images.salesDark3
                                    }
                                    text="IMPORTAÇÃO"
                                    selected={employeesFile.data.fileStatus.description === 'FINALIZADO COM SUCESSO'}
                                />
                            </WrapperFileSteps>
                            {employeesFile.data.fileStatus.description === 'FINALIZADO COM ERRO' && (
                                <WrapperText>
                                    <CommonText type="normal" color="dark">
                                        <CommonText type="normal" color="dark" bold>
                                            ATENÇÃO:{' '}
                                        </CommonText>
                                        O processo foi finalizado com erro, por favor acesse o site, verifique o erro e
                                        tente novamente.
                                    </CommonText>
                                </WrapperText>
                            )}
                            <ProgressBar
                                percent={this.handlePercent(employeesFile.data.fileStatus.description)}
                                color={
                                    employeesFile.data.fileStatus.description === 'FINALIZADO COM ERRO'
                                        ? 'dark'
                                        : 'primary'
                                }
                            />
                        </React.Fragment>
                    )}
                </Scroll>
                <Tab openMenu={() => this.setState({ menuVisible: true })} navigation={navigation} />
            </Container>
        );
    }
}

// Redux
const mapStateToProps = state => ({
    employeesFile: state.employeesFile,
});

const mapDispatchToProps = dispatch => ({
    getEmployeesFile: data => dispatch(getEmployeesFileAction(data)),
    sendEmployeesFile: data => dispatch(sendEmployeesFileAction(data)),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(EmployeesFile);
