import React from "react";
import { Routes } from "./Routes";
import { AppHeader } from "./shared-components/AppHeader";
import { AppFooter } from "./shared-components/AppFooter";
import { AppNavbar } from "./shared-components/AppNavbar";
import { Container, Grid } from "semantic-ui-react";
import { BrowserRouter } from "react-router-dom";

const myStyleNavBar = {
  background: "#18A999",
  padding: "1em 0em",
};
const myStyle = {
  padding: "0em 0em",
};

export const Layout: React.FC = () => {
  const logo = "https://img.icons8.com/plasticine/100/000000/dog-footprint.png";
  return (
    <BrowserRouter>
      <div>
        <Grid>
          <Grid.Row centered>
            <Grid.Column floated="left" width={3} style={myStyleNavBar}>
              <AppNavbar />
            </Grid.Column>
            <Grid.Column width={13} style={myStyle}>
              <AppHeader logo={logo}></AppHeader>
              <Routes />
              <AppFooter logo={logo}></AppFooter>
            </Grid.Column>
          </Grid.Row>
        </Grid>
      </div>
    </BrowserRouter>
  );
};
