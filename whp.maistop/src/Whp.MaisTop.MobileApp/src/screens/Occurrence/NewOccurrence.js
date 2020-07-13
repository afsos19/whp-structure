import * as React from 'react';
import styled from 'styled-components/native';
import { connect } from 'react-redux';
import { SafeAreaView } from 'react-navigation';
import { Formik } from 'formik';
import * as yup from 'yup';
import Icon from 'react-native-vector-icons/Ionicons';
import ImagePicker from 'react-native-image-picker';
import { newOccurrenceAction, occurrenceReset } from '../../redux/actions/occurrence';
import GradientBackground from '../../components/common/GradientBackground';
import CommonText from '../../components/common/CommonText';
import Tab from '../../components/common/Tab';
import Header from '../../components/common/Header';
import MenuModal from '../../components/Home/MenuModal';
import Card from '../../components/common/Card';
import Button from '../../components/common/Button';
import InputOptions from '../../components/common/InputOptions';
import occurrenceOptions from '../../utils/occurrenceOptions';
import InputArea from '../../components/common/InputArea';
import FaqCall from '../../components/Faq/FaqCall';
import ROUTENAMES from '../../navigation/routeName';
import Warning from '../../components/common/Warning';

const options = {
    title: 'Selecione uma foto',
    // quality: 0,
    maxWidth: 400,
    maxHeight: 400,
    storageOptions: {
        skipBackup: true,
        path: 'images',
    },
};

const validationSchema = yup.object().shape({
    about: yup.string().required('Assunto é obrigatório'),
    message: yup.string().required('Mensagem é obrigatório'),
});

const Container = styled(GradientBackground)`
    flex: 1;
`;

const Scroll = styled.ScrollView.attrs({
    showsVerticalScrollIndicator: false
})`
    width: 100%;
`;

const ButtonContainer = styled.View`
    width: 90%;
    align-self: center;
    align-items: flex-end;
    justify-content: flex-end;
    flex-direction: row;
`;

const TouchIcon = styled.TouchableOpacity`
    margin-right: 10;
`;

const AttachmentIcon = styled(Icon).attrs({
    name: 'ios-attach',
    size: 30,
    color: p => p.theme.colors.iconDark,
})`
    align-self: center;
`;

const AttachmentIconPrimary = styled(Icon).attrs({
    name: 'ios-attach',
    size: 30,
    color: p => p.theme.colors.primary,
})`
    align-self: center;
`;

const TextAttachment = styled.View`
    align-self: center;
    margin-right: 10;
    width: 40%;
`;

class NewOccurrence extends React.Component {
    static navigationOptions = {
        header: null,
    };

    state = {
        menuVisible: false,
        attachments: null,
        fileName: null,
        warningVisible: false,
    };

    componentDidUpdate(nextProps) {
        const { occurrence, reset } = this.props;
        if (occurrence.successMsg && occurrence.successMsg !== nextProps.occurrence.successMsg) {
            reset();
            this.refs.formik.setFieldValue('about', '');
            this.refs.formik.setFieldValue('message', '');
            this.setState({ attachments: null, warningVisible: true });
        }
    }

    handleAttachment = () => {
        ImagePicker.showImagePicker(options, response => {
            if (response.didCancel) {
                console.log('User cancelled image picker');
            } else if (response.error) {
                console.log('ImagePicker Error: ', response.error);
            } else if (response.customButton) {
                console.log('User tapped custom button: ', response.customButton);
            } else {
                console.log('RESPONSE:> ', response);
                this.setState({ attachment: response.uri, fileName: response.fileName });
            }
        });
    };

    handleNewOccurrence = values => {
        const { attachment } = this.state;
        const { newOccurrence } = this.props;

        const formdata = new FormData();

        formdata.append('OccurrenceContactTypeId', 2);
        formdata.append('OccurrenceSubjectId', parseInt(values.about));
        formdata.append('OccurrenceMessage.Message', values.message);
        formdata.append('OccurrenceMessage.OccurrenceMessageTypeId', 2);
        // start FornOne
        if (attachment) {
            formdata.append('formFile', {
                uri: values.image,
                name: 'image.jpg',
                type: 'multipart/form-data',
            });
        }
        newOccurrence(formdata);
    };

    render() {
        const { menuVisible, attachment, fileName, warningVisible } = this.state;
        const { navigation, occurrence } = this.props;

        return (
            <Container>
                <SafeAreaView />
                <Warning
                    type="success"
                    title="SUCESSO!"
                    text="Sua mensagem foi enviada!"
                    visible={warningVisible}
                    close={() => this.setState({ warningVisible: false })}
                />
                <MenuModal
                    navigation={navigation}
                    visible={menuVisible}
                    close={() => this.setState({ menuVisible: false })}
                />
                <Header navigation={navigation} />
                <Scroll>
                    <Card marginBottom={40} width="90%" marginTop={10} noPadding>
                        <CommonText type="h4" color="secondary" center>
                            Fale conosco
                        </CommonText>
                        <CommonText type="normal" color="dark" marginTop={5} center>
                            Para abrir um novo chamado, selecione o assunto e escreva sua mensagem no formulário abaixo.
                        </CommonText>
                        <Button
                            text="HISTÓRICO DE CHAMADOS"
                            color="secondary"
                            type="full"
                            marginTop={20}
                            onPress={() => navigation.navigate(ROUTENAMES.ALL_OCCURERENCE)}
                        />

                        <Formik
                            ref="formik"
                            initialValues={{ about: '', message: '' }}
                            onSubmit={values => this.handleNewOccurrence(values)}
                            validationSchema={validationSchema}
                        >
                            {({ values, handleChange, handleSubmit, isValid }) => (
                                <React.Fragment>
                                    <InputOptions
                                        placeholder="Assunto"
                                        width="90%"
                                        onChange={handleChange('about')}
                                        value={values.about}
                                        items={occurrenceOptions}
                                    />
                                    <InputArea
                                        value={values.message}
                                        onChange={handleChange('message')}
                                        placeholder="Mensagem"
                                        width="90%"
                                    />
                                    <ButtonContainer>
                                        {attachment ? (
                                            <TextAttachment>
                                                <CommonText type="small" color="primary">
                                                    {fileName}
                                                </CommonText>
                                            </TextAttachment>
                                        ) : (
                                            <TextAttachment>
                                                <CommonText
                                                    type="small"
                                                    color="dark"
                                                    // numberOfLines={1}
                                                >
                                                    Arquivos aceitos: .png, .jpg e .jpeg
                                                </CommonText>
                                            </TextAttachment>
                                        )}
                                        <TouchIcon onPress={() => this.handleAttachment()}>
                                            {attachment ? <AttachmentIconPrimary /> : <AttachmentIcon />}
                                        </TouchIcon>
                                        <Button
                                            onPress={handleSubmit}
                                            text="Enviar"
                                            disabled={!isValid}
                                            color="primary"
                                            isLoading={occurrence.isLoading}
                                        />
                                    </ButtonContainer>
                                </React.Fragment>
                            )}
                        </Formik>

                        <FaqCall navigation={navigation} />
                    </Card>
                </Scroll>
                <Tab openMenu={() => this.setState({ menuVisible: true })} navigation={navigation} />
            </Container>
        );
    }
}

// Redux
const mapStateToProps = state => ({
    occurrence: state.occurrence,
});

const mapDispatchToProps = dispatch => ({
    newOccurrence: data => dispatch(newOccurrenceAction(data)),
    reset: () => dispatch(occurrenceReset()),
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(NewOccurrence);
