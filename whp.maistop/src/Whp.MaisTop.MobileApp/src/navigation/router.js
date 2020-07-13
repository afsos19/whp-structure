import { createAppContainer, createStackNavigator, createSwitchNavigator } from 'react-navigation';
import FirstAccess from '../screens/FirstAccess/FirstAccess';
import Signup from '../screens/Signup/Signup';
import Sms from '../screens/Signup/Sms';
import Login from '../screens/Login/Login';
import ForgotPassword from '../screens/ForgotPassword/ForgotPassword';
import ROUTENAMES from './routeName';
import theme from '../theme';
import Home from '../screens/Home/Home';
import NewsDetail from '../screens/News/NewsDetail';
import AllNews from '../screens/News/AllNews';
import Products from '../screens/Products/Products';
import About from '../screens/About/About';
import Extract from '../screens/Extract/Extract';
import Profile from '../screens/Profile/Profile';
import NewOccurrence from '../screens/Occurrence/NewOccurrence';
import Faq from '../screens/Faq/Faq';
import AllOccurrence from '../screens/Occurrence/AllOccurrence';
import OccurrenceDetail from '../screens/Occurrence/OccurrenceDetail';
import MyTeam from '../screens/MyTeam/MyTeam';
import AllCampaings from '../screens/Campaign/AllCampaings';
import CampaignDetail from '../screens/Campaign/CampaignDetail';
import Spreadsheet from '../screens/Spreadsheet/Spreadsheet';
import SalesFile from '../screens/Spreadsheet/SalesFile';
import EmployeesFile from '../screens/Spreadsheet/EmployeesFile';
import Invite from '../screens/Invite/Invite';
import MyInvites from '../screens/Invite/MyInvites';
import PdfScreen from '../screens/PdfScreen/PdfScreen';
import NewPassword from '../screens/NewPassword/NewPassword';
import InviteRegister from '../screens/Invite/InviteRegister';
import Unauthorized from '../screens/Unauthorized/Unauthorized';
import Loading from '../screens/Loading/Loading';
import Catalog from '../screens/Catalog/Catalog';

const defaultNavigationOptions = {
    defaultNavigationOptions: {
        headerBackTitle: ' ',
        headerStyle: {
            elevation: 0,
            shadowOpacity: 0,
            borderBottomWidth: 0,
            backgroundColor: theme.colors.secondary,
        },
        headerTintColor: '#fff',
    },
};

const NonLoggedAppRouter = createStackNavigator(
    {
        [ROUTENAMES.LOGIN]: { screen: Login },
        [ROUTENAMES.NEW_PASSWORD]: { screen: NewPassword },
        [ROUTENAMES.FIRST_ACCESS]: { screen: FirstAccess },
        [ROUTENAMES.FORGOT_PASSWORD]: { screen: ForgotPassword },
        [ROUTENAMES.SIGNUP]: { screen: Signup },
        [ROUTENAMES.PDF_SCREEN]: { screen: PdfScreen },
        [ROUTENAMES.PDF_SCREEN_NO_AUTH]: { screen: PdfScreen },
        [ROUTENAMES.SMS]: { screen: Sms },
    },
    defaultNavigationOptions
);

const UnauthorizedAppRouter = createStackNavigator(
    {
        [ROUTENAMES.UNAUTHORIZED]: { screen: Unauthorized },
    },
    defaultNavigationOptions
);

const LoadingAppRouter = createStackNavigator(
    {
        [ROUTENAMES.LOADING]: { screen: Loading },
    },
    defaultNavigationOptions
);

const MgmAppRouter = createStackNavigator(
    {
        [ROUTENAMES.INVITE_REGISTER]: { screen: InviteRegister },
    },
    defaultNavigationOptions
);

const LoggedAppRouter = createStackNavigator(
    {
        [ROUTENAMES.HOME]: { screen: Home },
        [ROUTENAMES.NEWS_DETAIL]: { screen: NewsDetail },
        [ROUTENAMES.ALL_NEWS]: { screen: AllNews },
        [ROUTENAMES.PRODUCTS]: { screen: Products },
        [ROUTENAMES.ABOUT]: { screen: About },
        [ROUTENAMES.EXTRACT]: { screen: Extract },
        [ROUTENAMES.PROFILE]: { screen: Profile },
        [ROUTENAMES.NEW_OCCURERENCE]: { screen: NewOccurrence },
        [ROUTENAMES.ALL_OCCURERENCE]: { screen: AllOccurrence },
        [ROUTENAMES.OCCURERENCE_DETAIL]: { screen: OccurrenceDetail },
        [ROUTENAMES.FAQ]: { screen: Faq },
        [ROUTENAMES.MY_TEAM]: { screen: MyTeam },
        [ROUTENAMES.ALL_CAMPAIGNS]: { screen: AllCampaings },
        [ROUTENAMES.CAMPAIGN_DETAIL]: { screen: CampaignDetail },
        [ROUTENAMES.SPREADSHEET]: { screen: Spreadsheet },
        [ROUTENAMES.SALES_FILE]: { screen: SalesFile },
        [ROUTENAMES.EMPLOYEES_FILE]: { screen: EmployeesFile },
        [ROUTENAMES.INVITE]: { screen: Invite },
        [ROUTENAMES.MY_INVITES]: { screen: MyInvites },
        [ROUTENAMES.PDF_SCREEN]: { screen: PdfScreen },
        [ROUTENAMES.CATALOG]: { screen: Catalog },
        // AppStack,
    },
    { headerMode: 'none' }
);

const createRootNavigator = (token, isMgmUser) =>
    createAppContainer(
        createSwitchNavigator(
            {
                [ROUTENAMES.LOGGED_APP]: LoggedAppRouter,
                [ROUTENAMES.NON_LOGGED_APP]: NonLoggedAppRouter,
                [ROUTENAMES.UNAUTHORIZED_APP]: UnauthorizedAppRouter,
                [ROUTENAMES.LOADING_APP]: LoadingAppRouter,
                [ROUTENAMES.MGM_APP]: MgmAppRouter,
            },
            {
                initialRouteName: token ? ROUTENAMES.LOADING_APP : isMgmUser ? ROUTENAMES.MGM_APP : ROUTENAMES.NON_LOGGED_APP,
            }
        )
    );
export default createRootNavigator;
