import React from "react";
import { AuthConsumer } from "../../providers/AuthProvider";
import { Redirect } from "react-router";

export const LogoutCallback: React.FC = () => {
    return <AuthConsumer>
    {({ signOutRedirectCallback }) => {
        signOutRedirectCallback();
        return <Redirect to="/" />
    }}
    </AuthConsumer>
}