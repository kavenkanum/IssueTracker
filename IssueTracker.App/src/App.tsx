import React, { useEffect } from "react";
import logo from "./logo.svg";
import "./App.css";
import { BrowserRouter, Route, Switch } from "react-router-dom";
import { PersistGate } from "redux-persist/integration/react";
import { AppHeader } from "./components/shared-components/AppHeader";
import { AppFooter } from "./components/shared-components/AppFooter";
import { AppNavbar } from "./components/shared-components/AppNavbar";
import { Home } from "./components/Home";
import { LoginCallback } from "./components/authentication/LoginCallback";
import { Logout } from "./components/authentication/Logout";
import { Login } from "./components/authentication/Login";
import { SilentRenew } from "./components/authentication/SilentRenew";
import { LogoutCallback } from "./components/authentication/LogoutCallback";
import { Register } from "./components/Register";
import { ForgotPassword } from "./components/ForgotPassword";
import { AddProject } from "./components/AddProject";
import { NotFound } from "./components/NotFound";
import { Dashboard } from "./components/Dashboard";
import { Provider } from "react-redux";
import store, { persistor } from "./store/index";
import { AuthProvider } from "./providers/AuthProvider";
import { Container, Grid, Segment } from "semantic-ui-react";

export const App: React.FC = () => {
  return (
    <Provider store={store}>
      <PersistGate persistor={persistor}>
        <AuthProvider>
          <BrowserRouter>
            <AppHeader logo={logo}></AppHeader>
            <Container fluid className="main-display">
              <Grid>
                <Grid.Row>
                  <Grid.Column floated="left" width={4}>
                    <AppNavbar logo={logo}></AppNavbar>
                  </Grid.Column>
                  <Grid.Column width={12}>
                    <Switch>
                      <Route path="/" exact component={Home} />
                      <Route path="/login" exact component={Login} />
                      <Route
                        path="/login/callback"
                        exact
                        component={LoginCallback}
                      />
                      <Route path="/logout" exact component={Logout} />
                      <Route
                        path="/logout/callback"
                        exact
                        component={LogoutCallback}
                      />
                      <Route
                        path="/silent-renew"
                        exact
                        component={SilentRenew}
                      />
                      <Route path="/register" component={Register} />
                      <Route
                        path="/forgot-password"
                        component={ForgotPassword}
                      />
                      <Route path="/project/add" exact component={AddProject} />
                      <Route path="/dashboard" exact component={Dashboard} />
                      <Route component={NotFound} />
                    </Switch>
                  </Grid.Column>
                </Grid.Row>
              </Grid>
            </Container>
            <AppFooter logo={logo}></AppFooter>
          </BrowserRouter>
        </AuthProvider>
      </PersistGate>
    </Provider>
  );
};
