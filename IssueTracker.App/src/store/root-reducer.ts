import { combineReducers } from "redux";
import usersReducer from "../features/users/reducers";
import projectsReducer from "../features/projects/reducers";

const rootReducer = combineReducers({
    user: usersReducer,
    project: projectsReducer
});

export default rootReducer;