import React from "react";
import { AuthConsumer } from "../../providers/AuthProvider";

export const Logout: React.FC = () => {
    return <AuthConsumer>
    {({ logout }) => {
        logout();
        return <span>Loading... (LOGOUT)</span>;
    }}
    </AuthConsumer>
}