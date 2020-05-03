import { combineReducers } from '@reduxjs/toolkit';
import { createBrowserHistory } from 'history';
import { connectRouter } from 'connected-react-router';

import usersReducer from "../features/users/reducers";
import projectsReducer from "../features/projects/slice";

export const history = createBrowserHistory();

const rootReducer = combineReducers({
  router: connectRouter(history),
  user: usersReducer,
  project: projectsReducer.reducer
});

export type RootState = ReturnType<typeof rootReducer>;

export default rootReducer;