import React from "react";
import { BrowserRouter, Switch, Route } from "react-router-dom";
import { NewError } from "./errors/NewError";
import { Home } from "./Home";
import { LoginCallback } from "./authentication/LoginCallback";
import { Logout } from "./authentication/Logout";
import { Login } from "./authentication/Login";
import { SilentRenew } from "./authentication/SilentRenew";
import { LogoutCallback } from "./authentication/LogoutCallback";
import { Register } from "./Register";
import { ForgotPassword } from "./ForgotPassword";
import { AddProject } from "./AddProject";
import { NotFound } from "./NotFound";
import { Dashboard } from "./Dashboard";
import { AddJob } from "./AddJob";
import { EditJob } from "./EditJob";
import { AddPrevJob } from "./AddPrevJob";

export const Routes: React.FC = () => {
  return (
    <div style={{width: "100%", padding: "0em 3em 1em 0em"}}>
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
        <Route path="/dashboard" exact component={Dashboard} />
        <Route path="/dashboard/:project" exact component={Dashboard} />
        <Route path="/job/addJob" exact component={AddJob} />
        <Route path="/job/editJob" exact component={EditJob} />
        <Route path="/job/addPrevJobs" exact component={AddPrevJob} />
        <Route path="/error" component={NewError} />
        <Route component={NotFound} />
      </Switch>
    </div>
  );
};
