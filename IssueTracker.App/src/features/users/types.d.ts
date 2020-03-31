export type LoginPayload = {
    firstName: string,
    lastName: string,
    token: {
        accessToken: string,
        idToken: string
    }
};