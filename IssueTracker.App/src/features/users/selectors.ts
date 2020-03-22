import { RootState } from "typesafe-actions";

export const isUserLogged = (state: RootState) => state.user.isLogged;
export const getUserFullName = (state: RootState) => `${state.user.firstName} ${state.user.lastName}`;