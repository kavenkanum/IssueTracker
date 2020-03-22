import { combineReducers } from "redux";
import usersReducer from "../features/users/reducers";

const rootReducer = combineReducers({
    user: usersReducer
});

export default rootReducer;