import { applyMiddleware, combineReducers, createStore } from 'redux';
import logger from 'redux-logger';
import thunk from 'redux-thunk';

// Reducers
import auth from './reducers/auth';
import resetPassword from './reducers/resetPassword';
import firstAccess from './reducers/firstAccess';
import signup from './reducers/signup';
import sms from './reducers/sms';
import user from './reducers/user';
import network from './reducers/network';
import ponctuation from './reducers/ponctuation';
import news from './reducers/news';
import products from './reducers/products';
import occurrence from './reducers/occurrence';
import team from './reducers/team';
import sales from './reducers/sales';
import orders from './reducers/orders';
import campaign from './reducers/campaign';
import salesFile from './reducers/salesFile';
import employeesFile from './reducers/employeesFile';
import mgm from './reducers/mgm';

const rootReducer = combineReducers({
    auth,
    signup,
    resetPassword,
    firstAccess,
    sms,
    user,
    network,
    ponctuation,
    news,
    products,
    occurrence,
    team,
    sales,
    orders,
    campaign,
    salesFile,
    employeesFile,
    mgm,
});

export default createStore(rootReducer, applyMiddleware(thunk, logger));
