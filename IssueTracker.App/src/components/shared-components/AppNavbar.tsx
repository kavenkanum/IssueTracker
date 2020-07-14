import React, { useEffect } from "react";
import { Container, Menu, Header } from "semantic-ui-react";
import { Link, useHistory, useParams } from "react-router-dom";
import { useSelector, useDispatch } from "react-redux";
import slice, { loadProjects, Project } from "../../features/projects/slice";
import { RootState } from "../../store/root-reducer";
import jobSlice from "../../features/jobs/slice";
import { AppLogo } from "../AppLogo";

const menuStyle = {
  border: "none",
  borderRadius: 0,
  boxShadow: "none",
  width: "100%",
  padding: "1em 0em",
  height: "100%",
};

const fixedMenuStyle = {
  backgroundColor: "#18A999",
  color: "white",
  position: "relative",
  width: "100%",
};

const addNewProjButtonStyle = {
  color: "white",
  fontStyle: "italic",
  fontWeight: "normal",
  padding: "0em 0em 0em 1em",
};

export const AppNavbar: React.FC = () => {
  const { project } = useParams();
  const projectId = project ? parseInt(project) : 0;
  const history = useHistory();
  const projects = useSelector((state: RootState) => state.project.projectList);
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(loadProjects());
  }, []);

  const handleItemClick = (el: Project) => {
    dispatch(slice.actions.selectProject(el));
    dispatch(jobSlice.actions.removeSelectedJob());
    history.push(`/dashboard/${el.id}`);
  };

  return (
    <Container style={menuStyle}>
      <AppLogo />
      <Menu pointing vertical style={fixedMenuStyle}>
        {projects
          ? projects.map((p) => (
              <Menu.Item
                key={p.id}
                name={p.name}
                index={p.id}
                active={projectId === p.id}
                onClick={() => handleItemClick(p)}
                style={{ color: "white" }}
              />
            ))
          : null}
      </Menu>
      <Header style={addNewProjButtonStyle} as={Link} to="/project/add">
        Add new project
      </Header>
    </Container>
  );
};
