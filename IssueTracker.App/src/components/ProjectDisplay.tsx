import React, { useState, useEffect } from "react";
import { Container, Header, Menu, Label, Button } from "semantic-ui-react";
import { Job, getJobs, getUsersFromProject, User, Status } from "./API";
import { Link, useHistory } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import slice from "../features/jobs/slice";
import { RootState } from "../store/root-reducer";
import { UserInitials } from "./elements/UserInitials";
import { JobsFilter } from "./JobsFilter";
import { PriorityIcon } from "./elements/PriorityIcon";
import { StatusIcon } from "./elements/StatusIcon";
import { loadProject } from "../features/projects/slice";

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
  const [jobsStatus, setJobsStatus] = useState<Status>(Status.None);
  const [users, setUsers] = useState<Array<User>>([]);
  const dispatch = useDispatch();
  const history = useHistory();
  const currentProjectName = useSelector(
    (state: RootState) => state.project.selectedProjectName
  );
  const handleItemClick = (jobId: number) => {
    setActiveItem(jobId);
    dispatch(slice.actions.selectJob(jobId));
    history.push(`/dashboard/${props.projectId}/${jobId}`);
  };

  useEffect(() => {
    getJobs(props.projectId, jobsStatus).then((resp) => setJobs(resp));
  }, [props.projectId, jobsStatus]);

  useEffect(() => {
    getUsersFromProject(props.projectId).then((resp) => setUsers(resp));
    dispatch(loadProject(props.projectId));
  }, [props.projectId]);

  return (
    <Container style={segmentDisplay}>
      <div>
        <Header as="h2" style={headerStyle}>
          {currentProjectName}
        </Header>
        <div className="job-menu">
          {users.map((u) => (
            <UserInitials key={u.userId} fullName={u.fullName} />
          ))}
        </div>
      </div>
      <Button primary as={Link} to="/job/addJob" style={buttonStyle}>
        Add task
      </Button>
      <Menu vertical style={{ width: "100%", marginTop: "2em" }}>
        {jobs?.map((j) => (
          <Menu.Item
            key={j.jobId}
            name={j.name}
            index={j.jobId}
            active={activeItem === j.jobId}
            onClick={() => handleItemClick(j.jobId)}
          >
            {j.name}
            <div className="jobs-menu-icons">
              <PriorityIcon priority={j.priority} />
              <StatusIcon status={j.status} />
            </div>
          </Menu.Item>
        ))}
      </Menu>
      <JobsFilter onChange={(value: any) => setJobsStatus(value)} />
    </Container>
  );
};
