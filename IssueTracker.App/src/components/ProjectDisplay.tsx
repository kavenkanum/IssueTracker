import React, { useState, useEffect } from "react";
import { Container, Header, Menu, Label, Button } from "semantic-ui-react";
import { Job, getJobs } from "./API";
import HeaderSubHeader from "semantic-ui-react/dist/commonjs/elements/Header/HeaderSubheader";
import { Link } from "react-router-dom";
import { useDispatch } from "react-redux";
import slice, { loadJobDetails } from "../features/jobs/slice";

const segmentDisplay = {
  backgroundColor: "white",
  border: "1px solid #ddd",
  height: "600px",
  margin: "0em 2em",
};

export const ProjectDisplay = (props: any) => {
  const [activeItem, setActiveItem] = useState<any>(0);
  const [jobs, setJobs] = useState<Array<Job>>([]);
  const dispatch = useDispatch();
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
      <Button primary as={Link} to="/job/addJob">
        Add task
      </Button>
      <Header>Project Name will be here</Header>
      <HeaderSubHeader>Jobs' list</HeaderSubHeader>
      <Menu vertical>
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
