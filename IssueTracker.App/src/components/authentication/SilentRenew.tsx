import React from "react";
import { AuthConsumer } from "../../providers/AuthProvider";

export const SilentRenew: React.FC = () => {
    return <AuthConsumer>
    {({ signInSilentCallback }) => {
        signInSilentCallback();
        return <span>Loading... (SILENT RENEW)</span>;
    }}
    </AuthConsumer>
}