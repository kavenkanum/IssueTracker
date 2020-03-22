import React from 'react';
import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import { AppHeader } from './components/shared-components/AppHeader';
import { AppFooter } from './components/shared-components/AppFooter';
import { Home } from './components/Home';
import { LoginCallback } from './components/authentication/LoginCallback';
import { Logout } from './components/authentication/Logout';
import { Login } from './components/authentication/Login';
import { SilentRenew } from './components/authentication/SilentRenew';
import { LogoutCallback } from './components/authentication/LogoutCallback';
import { Register } from './components/Register';
import { ForgotPassword } from './components/ForgotPassword';
import { AddProject } from './components/AddProject';
import { NotFound } from './components/NotFound';
import { Provider } from 'react-redux';
import store from './store/index';
import { AuthProvider } from './providers/AuthProvider';

export const App: React.FC = () => {
  return <Provider store={store}>
    <AuthProvider>
      <BrowserRouter>
        <AppHeader logo={logo}></AppHeader>
        <Switch>
          <Route path="/" exact component={Home} />
          <Route path="/login" exact component={Login} />
          <Route path="/login/callback" exact component={LoginCallback} />
          <Route path="/logout" exact component={Logout} />
          <Route path="/logout/callback" exact component={LogoutCallback} />
          <Route path="/silent-renew" exact component={SilentRenew} />
          <Route path="/register" component={Register} />
          <Route path="/forgot-password" component={ForgotPassword} />
          <Route path="/project/add" exact component={AddProject} />

          <Route component={NotFound} />
        </Switch>
        <AppFooter logo={logo}></AppFooter>
      </BrowserRouter>
    </AuthProvider>
  </Provider>
}

