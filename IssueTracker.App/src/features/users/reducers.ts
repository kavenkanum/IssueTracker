import { createReducer } from "typesafe-actions";
import { login, logout } from "./actions";

export type UserState = {
    firstName?: string,
    lastName?: string,
    token?: {
        accessToken: string,
        idToken: string
    },
    isLogged: boolean
};
export type Todo = {
    id: string;
    title: string;
};

export const todos = createReducer([
    {
        id: '0',
        title: 'You can add new todos using the form or load saved snapshot...',
    },
] as Todo[]);


export const usersReducer = createReducer({
    isLogged: false
} as UserState)
    .handleAction(logout, () => ({
        firstName: undefined,
        lastName: undefined,
        token: undefined,
        isLogged: false
    }))
    .handleAction(login, (state: UserState, action) => ({
        ...state,
        ...action.payload,
        isLogged: true
    }));
    

export default usersReducer;
export type UsersState = ReturnType<typeof usersReducer>;