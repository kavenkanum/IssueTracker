export type LoginPayload = {
    firstName: string,
    lastName: string,
    role: string,
    token: {
        accessToken: string,
        idToken: string
    }
};