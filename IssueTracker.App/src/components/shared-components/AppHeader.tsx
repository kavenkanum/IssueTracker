import React, { useState, Fragment } from "react";
import { Menu } from "semantic-ui-react";
import { Link } from "react-router-dom";
import { useSelector } from "react-redux";
import { isUserLogged, getUserFullName } from "./../../features/users/slice";

interface AppHeaderProps {
  logo: any;
}

export const AppHeader: React.FC<AppHeaderProps> = () => {
  const [] = useState(false);
  const isLoggedIn = useSelector(isUserLogged);
  const userFullName = useSelector(getUserFullName);

  return (
    <div className="top-menu">
      <Menu borderless>
        <Menu.Menu position="right">
          {isLoggedIn ? (
            <Fragment>
              <Menu.Item>{userFullName}</Menu.Item>
              <Menu.Item as={Link} to="/logout">
                Logout
              </Menu.Item>
            </Fragment>
          ) : (
            <Fragment>
              <Menu.Item as={Link} to="/register">
                Register
              </Menu.Item>
              <Menu.Item as={Link} to="/login">
                Login
              </Menu.Item>
            </Fragment>
          )}
        </Menu.Menu>
      </Menu>
    </div>
  );
};
