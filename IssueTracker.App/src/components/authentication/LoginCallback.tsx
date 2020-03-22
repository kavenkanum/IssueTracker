import React from "react";
import { AuthConsumer } from "../../providers/AuthProvider";
import { Redirect } from "react-router";

export const LoginCallback: React.FC = () => {
    return <AuthConsumer>
    {({ signInRedirectCallback }) => {
        signInRedirectCallback();
        return <Redirect to="/" />
    }}
    </AuthConsumer>
}