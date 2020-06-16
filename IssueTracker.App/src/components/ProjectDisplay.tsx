import React, { useState, useEffect } from "react";
import { Container, Header, Menu, Label, Button } from "semantic-ui-react";
import { Job, getJobs } from "./API";
import HeaderSubHeader from "semantic-ui-react/dist/commonjs/elements/Header/HeaderSubheader";
import { Link } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import slice, { loadJobDetails } from "../features/jobs/slice";
import { RootState } from "../store/root-reducer";

const segmentDisplay = {
  backgroundColor: "white",
  border: "1px solid #ddd",
  height: "600px",
  padding: "1em 1em",
};

const buttonStyle = {
  "border-radius": "25px",
  padding: "1em 5em 1em 5em",
  background: "#FF715B",
};

export const ProjectDisplay = (props: any) => {
  const [activeItem, setActiveItem] = useState<any>(0);
  const [jobs, setJobs] = useState<Array<Job>>([]);
  const dispatch = useDispatch();
  const currentProjectName = useSelector(
    (state: RootState) => state.project.selectedProjectName
  );
  const handleItemClick = (jobId: number) => {
    setActiveItem(jobId);
    dispatch(slice.actions.selectJob(jobId));
    dispatch(loadJobDetails(jobId));
  };

  useEffect(() => {
    getJobs(props.projectId).then((resp) => setJobs(resp));
  }, [props.projectId]);

  return (
    <Container style={segmentDisplay}>
      <Header as="h2">{currentProjectName}</Header>
      <Button primary as={Link} to="/job/addJob" style={buttonStyle}>
        Add task
      </Button>
        <Menu vertical style={{ width: "100%", "margin-top": "2em"}}>
          {jobs?.map((j) => (
            <Menu.Item
              key={j.jobId}
              name={j.name}
              index={j.jobId}
              active={activeItem === j.jobId}
              onClick={() => handleItemClick(j.jobId)}
            >
              <Label>{j.status}</Label>
              {j.name}
            </Menu.Item>
          ))}
        </Menu>
    </Container>
  );
};
