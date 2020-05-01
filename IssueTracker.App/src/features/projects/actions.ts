import { createAction } from 'typesafe-actions';
import { ProjectPayload } from './types';

// export const selectProject = (projectId: number) => ({
//     type: "SELECT_PROJECT",
//     projectId
// });

export const selectProject = createAction('PROJECTS/SELECT', (id?: number, name?: string) => 
({
    id: id,
    name: name
}))<ProjectPayload>();

// export const login = createAction('USERS/LOGIN', (firstName: string, lastName: string, role: string, access_token: string, id_token: string) =>
//     ({
//         firstName: firstName,
//         lastName: lastName,
//         role: role,
//         token: {
//             accessToken: access_token,
//             idToken: id_token
//         }
//     }))<LoginPayload>();

// export const logout = createAction('USERS/LOGOUT')();