import React, { useEffect } from "react";
import "./App.css";
import { BrowserRouter, Route, Switch } from "react-router-dom";
import { PersistGate } from "redux-persist/integration/react";
import { Layout } from "./components/Layout";
import { Provider } from "react-redux";
import store, { persistor } from "./store/index";
import { AuthProvider } from "./providers/AuthProvider";
import { Container, Grid, Segment } from "semantic-ui-react";

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
