import React from 'react';
import { AuthService } from '../services/AuthService';

const AuthContext = React.createContext({
    createSignInRequest: () => ({}),
    signInRedirect: () => ({}),
    signInRedirectCallback: () => ({}),
    signInSilentCallback: () => ({}),
    logout: () => ({}),
    signOutRedirectCallback: () => ({})
});

export const AuthConsumer = AuthContext.Consumer;

export const AuthProvider: React.FC = ({ children }) => {
    let authService = new AuthService();

    return <AuthContext.Provider value={authService}>{children}</AuthContext.Provider>;
}