import React from "react";
import { Routes } from "./Routes";
import { AppHeader } from "./shared-components/AppHeader";
import { AppFooter } from "./shared-components/AppFooter";
import { AppNavbar } from "./shared-components/AppNavbar";
import { Container, Grid } from "semantic-ui-react";
import { BrowserRouter } from "react-router-dom";


export const Layout: React.FC = () => {
const logo = "https://img.icons8.com/plasticine/100/000000/dog-footprint.png";
    return (
        <BrowserRouter>
        <AppHeader logo={logo}></AppHeader>
            <Container fluid className="main-display">
              <Grid>
                <Grid.Row>
                  <Grid.Column floated="left" width={4}>
                    <AppNavbar></AppNavbar>
                  </Grid.Column>
                  <Grid.Column width={12}>
                    <Routes />
                  </Grid.Column>
                </Grid.Row>
              </Grid>
            </Container>
            <AppFooter logo={logo}></AppFooter>
            </BrowserRouter>
    )
}