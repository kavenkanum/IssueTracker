import React, { useState, Fragment, useEffect } from "react";
import {
  Container,
  Image,
  Menu,
  Visibility,
  MenuItemProps,
} from "semantic-ui-react";
import { Link, useHistory, useParams } from "react-router-dom";
import { useSelector, connect, useDispatch } from "react-redux";
import slice, {loadProjects, Project} from "../../features/projects/slice";
import { RootState } from "../../store/root-reducer";
import jobSlice from "../../features/jobs/slice";

const menuStyle = {
  border: "none",
  borderRadius: 0,
  boxShadow: "none",
  transition: "box-shadow 0.5s ease, padding 0.5s ease",
  width: "90%",
  height: "800px",
  backgroundColor: "white",
  padding: "1em 0em",
};

const fixedMenuStyle = {
  backgroundColor: "#fff",
  border: "1px solid #ddd",
  boxShadow: "0px 3px 5px rgba(0, 0, 0, 0.2)",
  width: "100%",
};

export const AppNavbar: React.FC = (props) => {
  const { project } = useParams();
  const projectId = (project ? parseInt(project) : 0);
  const history = useHistory();
  const projects = useSelector((state: RootState) => state.project.projectList);
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(loadProjects());
  }, []);

  const handleItemClick = (el: Project) => {
    dispatch(slice.actions.selectProject(el.id));
    dispatch(jobSlice.actions.removeSelectedJob());
    history.push(`/dashboard/${el.id}`);
  };

  return (
    <Container style={menuStyle}>
      <Menu pointing vertical style={fixedMenuStyle}>
        {projects ? projects.map((p) => (
          <Menu.Item
            key={p.id}
            name={p.name}
            index={p.id}
            active={projectId === p.id}
            onClick={() => handleItemClick(p)}
          />
        )) : null}
      </Menu>
    </Container>
  );
};
