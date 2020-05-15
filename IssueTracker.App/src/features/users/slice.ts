import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import {RootState} from "../../store/root-reducer";

const initialState: UserState = {
  firstName: undefined,
  lastName: undefined,
  token: {
    accessToken: undefined,
    idToken: undefined,
  },
  isLogged: false,
};

const slice = createSlice({
  name: "user",
  initialState: initialState,
  reducers: {
    login(state: UserState, action: PayloadAction<User>) {
      state.firstName = action.payload.profile.given_name;
      state.lastName = action.payload.profile.family_name;
      state.token.accessToken = action.payload.access_token;
      state.token.idToken = action.payload.id_token;
      state.isLogged = true;
    },
    logout(state) {
      state.firstName = undefined;
      state.lastName = undefined;
      state.token.accessToken = undefined;
      state.token.idToken = undefined;
      state.isLogged = false;
    },
  },
});

export default slice;

export interface User {
    access_token: string;
    id_token: string;
    profile: {
      family_name: string;
      given_name: string;
}};

export interface UserState {
  firstName?: string;
  lastName?: string;
  token: {
    accessToken?: string;
    idToken?: string;
  };
  isLogged: boolean;
};

export const isUserLogged = (state: RootState) => state.user.isLogged;
export const getUserFullName = (state: RootState) => `${state.user.firstName} ${state.user.lastName}`;
export const getUserToken = (state: RootState) => `${state.user.token?.accessToken}`;
