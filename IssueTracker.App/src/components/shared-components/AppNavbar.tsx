import React, { useState, Fragment, useEffect } from "react";
import {
  Container,
  Image,
  Menu,
  Visibility,
  MenuItemProps,
} from "semantic-ui-react";
import { Link } from "react-router-dom";
import { useSelector } from "react-redux";
import {
  isUserLogged,
  getUserFullName,
  getUserRole,
} from "./../../features/users/selectors";
import {getProjects, Project} from "../API";


const menuStyle = {
  border: "none",
  borderRadius: 0,
  boxShadow: "none",
  transition: "box-shadow 0.5s ease, padding 0.5s ease",
  width: "90%",
  height: "800px",
  backgroundColor: "white",
  padding: "1em 0em"
};

const fixedMenuStyle = {
  backgroundColor: "#fff",
  border: "1px solid #ddd",
  boxShadow: "0px 3px 5px rgba(0, 0, 0, 0.2)",
  width: "100%"
};

interface AppNavbarProps {
  logo: any;
}

export const AppNavbar: React.FC<AppNavbarProps> = (props: AppNavbarProps) => {
  const [activeItem, setActiveItem] = useState<any>(1);
  const [projects, setProjects] = useState<Array<Project>>([])
  const isLoggedIn = useSelector(isUserLogged);
  
  useEffect( () => {
    getProjects().then(res => setProjects(res));
  }, []);

  const handleItemClick = (
    e: React.MouseEvent<HTMLAnchorElement, MouseEvent>,
    { index }: MenuItemProps
  ) => setActiveItem(index);

  return (
    <Container style={menuStyle}>
      <Menu pointing vertical style={fixedMenuStyle}>
        {projects.map(p => <Menu.Item
          key={p.id}
          name={p.name}
          index={p.id}
          active={activeItem === p.id}
          onClick={handleItemClick}
        />)}
      </Menu>
    </Container>
  );
};
