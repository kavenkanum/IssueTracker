import React, { useState, useEffect } from "react";
import { Container, Header, Menu, Label, Button } from "semantic-ui-react";
import { Job, getJobs, getUsersFromProject, User, Status } from "./API";
import { Link } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import slice, { loadJobDetails } from "../features/jobs/slice";
import { RootState } from "../store/root-reducer";
import { UserInitials } from "./elements/UserInitials";
import {JobsFilter} from "./JobsFilter";

const headerStyle = {
  display: "inline",
};

const segmentDisplay = {
  backgroundColor: "white",
  border: "1px solid #ddd",
  height: "600px",
  padding: "1em 1em",
};

const buttonStyle = {
  borderRadius: "25px",
  padding: "1em 5em 1em 5em",
  margin: "1em 0em",
  background: "#FF715B",
};

export const ProjectDisplay = (props: any) => {
  const [activeItem, setActiveItem] = useState<any>(0);
  const [jobs, setJobs] = useState<Array<Job>>([]);
  const [jobsStatus, setJobsStatus] = useState<Status>(Status.None)
  const [users, setUsers] = useState<Array<User>>([]);
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
    getJobs(props.projectId, jobsStatus).then((resp) => setJobs(resp));
    getUsersFromProject(props.projectId).then((resp) => setUsers(resp));
  }, [props.projectId, jobsStatus]);

  return (
    <Container style={segmentDisplay}>
      <div>
        <Header as="h2" style={headerStyle}>
          {currentProjectName}
        </Header>
        <div className="job-menu">
          {users.map((u) => (
            <UserInitials fullName={u.fullName} />
          ))}
        </div>
      </div>
      <Button primary as={Link} to="/job/addJob" style={buttonStyle}>
        Add task
      </Button>
      <Menu vertical style={{ width: "100%", "margin-top": "2em" }}>
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
      <JobsFilter onChange={(value: any) => setJobsStatus(value)}/>
    </Container>
  );
};
