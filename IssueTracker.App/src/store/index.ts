import { createStore } from "redux";
import rootReducer from "./root-reducer";
import { composeWithDevTools } from 'redux-devtools-extension';
import { persistReducer, persistStore } from 'redux-persist';
import storage from 'redux-persist/lib/storage';

const initialState = {};

const store = createStore(persistReducer(persistConfig, rootReducer), initialState, composeWithDevTools());

const persistConfig = {
  key: 'root',
  storage,
  whitelist: ['user'],
};

export const persistor = persistStore(store);

export default store;
