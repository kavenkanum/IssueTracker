import { combineReducers } from '@reduxjs/toolkit';
import { createBrowserHistory } from 'history';
import { connectRouter } from 'connected-react-router';

import usersReducer from "../features/users/slice";
import projectsReducer from "../features/projects/slice";
import jobsReducer from "../features/jobs/slice";

export const history = createBrowserHistory();

const rootReducer = combineReducers({
  router: connectRouter(history),
  user: usersReducer.reducer,
  project: projectsReducer.reducer,
  job: jobsReducer.reducer
});

export type RootState = ReturnType<typeof rootReducer>;

export default rootReducer;