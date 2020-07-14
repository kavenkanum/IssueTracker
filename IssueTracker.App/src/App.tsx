import React from "react";
import "./App.css";
import { PersistGate } from "redux-persist/integration/react";
import { Layout } from "./components/Layout";
import { Provider } from "react-redux";
import store, { persistor } from "./store/index";
import { AuthProvider } from "./providers/AuthProvider";

export const App: React.FC = () => {
  return (
    <Provider store={store}>
      <PersistGate persistor={persistor}>
        <AuthProvider>
          <Layout />
        </AuthProvider>
      </PersistGate>
    </Provider>
  );
};
