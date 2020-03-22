import { createAction } from 'typesafe-actions';
import { LoginPayload } from './types';

export const login = createAction('USERS/LOGIN', (firstName: string, lastName: string, access_token: string, id_token: string) =>
    ({
        firstName: firstName,
        lastName: lastName,
        token: {
            accessToken: access_token,
            idToken: id_token
        }
    }))<LoginPayload>();

export const logout = createAction('USERS/LOGOUT')();