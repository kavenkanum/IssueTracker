import React, { Fragment } from "react";
import {
  Button,
  Container,
  Divider,
  Grid,
  Header,
  Icon,
} from "semantic-ui-react";
import { Link } from "react-router-dom";
import { useSelector } from "react-redux";
import { isUserLogged } from "../features/users/selectors";

export const Home: React.FC = () => {
  const isLoggedIn = useSelector(isUserLogged);

  return (
    <Container fluid textAlign="center" style={{ padding: "1em 0em"}}>
      {isLoggedIn ? (
        <Container fluid style={{border: "1px solid #ddd", backgroundColor: "white" }}>
          <Header as="h2" icon>
            <Icon name="users" />
            You are logged in
            <Header.Subheader>NICE</Header.Subheader>
          </Header>
          <Container>
            <Button primary as={Link} to="/project/add">
              Add project
            </Button>
            <Button primary as={Link} to="/dashboard">
              dashboard
            </Button>
          </Container>
        </Container>
      ) : (
        <Container>
          <Header as="h2" icon>
            <Icon name="fort awesome" />
            Login or register
            <Header.Subheader>What would you like to do?</Header.Subheader>
          </Header>
          <Divider inverted section />
          <Grid columns={2} divided>
            <Grid.Row>
              <Grid.Column>
                <Header as="h4">I want to log in to my existing account</Header>
                <Fragment>
                  <Button primary as={Link} to="/login">
                    Login now
                  </Button>
                  <Link
                    to="/forgot-password"
                    style={{ marginTop: "1em", display: "block" }}
                  >
                    Forgot password?
                  </Link>
                </Fragment>
              </Grid.Column>
              <Grid.Column>
                <Header as="h4">
                  I don't have an account and want to create one
                </Header>
                <Button primary as={Link} to="/register">
                  Register a new account
                </Button>
              </Grid.Column>
            </Grid.Row>
          </Grid>
        </Container>
      )}
    </Container>
  );
};
