import React from "react";
import { AuthConsumer } from "../../providers/AuthProvider";

export const Login: React.FC = () => {
    return <AuthConsumer>
    {({ signInRedirect }) => {
        signInRedirect();
        return <span>Loading... (LOGIN)</span>;
    }}
    </AuthConsumer>
}