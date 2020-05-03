import { configureStore, Action, getDefaultMiddleware } from '@reduxjs/toolkit';
import { ThunkAction } from 'redux-thunk';
import { persistReducer, persistStore, PersistConfig } from 'redux-persist';
import storage from 'redux-persist/lib/storage';
import { routerMiddleware } from 'connected-react-router';

import rootReducer, { RootState, history } from './root-reducer';

const persistConfig: PersistConfig<any> = {
  key: 'root',
  storage,
  whitelist: ['auth'],
};

const store = configureStore({
  reducer: persistReducer(persistConfig, rootReducer),
  middleware: [
    ...getDefaultMiddleware({
      serializableCheck: {
        ignoredActions: ['persist/PERSIST', 'persist/REHYDRATE'],
      },
    }),
    routerMiddleware(history),
  ],
});

// if (process.env.NODE_ENV === 'development' && 
// module.hot
// ) {
//   module.hot.accept('./rootReducer', () => {
//     const newRootReducer = require('./rootReducer').default;
//     store.replaceReducer(newRootReducer);
//   });
// }

export type AppDispatch = typeof store.dispatch;
export type AppThunk = ThunkAction<void, RootState, undefined, Action<string>>;

export const persistor = persistStore(store);

export default store;

export { history };

/** Loads app initial state */
export const loadApp = (): AppThunk => async (dispatch) => {
  // const todayIso = DateTime.local().toISODate();
};