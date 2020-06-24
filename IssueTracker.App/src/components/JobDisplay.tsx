import React from "react";
import { Container, Header, Button, Image, Dropdown } from "semantic-ui-react";
import { useSelector } from "react-redux";
import { RootState } from "../store/root-reducer";
import { JobComments } from "./JobComments";
import { Link } from "react-router-dom";
import { Status } from "./API";
import { JobStatusButton } from "./JobStatusButton";

const menuIcon = (
  <Image
    src="https://img.icons8.com/carbon-copy/100/000000/menu.png"
    inline
    size="mini"
  />
);

const dropdownOptions = [
  { key: "edit", text: "Add previous tasks", as: Link, to: "/job/addPrevJobs" },
];

const segmentDisplay = {
  backgroundColor: "white",
  border: "1px solid #ddd",
  margin: "0em 2em",
  padding: "1em 1em",
};

const headerStyle = {
  fontSize: "20px",
  display: "inline",
};

export const JobDisplay = () => {
  const currentJobDetails = useSelector(
    (state: RootState) => state.job.jobDetails
  );
  const currentJobId = useSelector(
    (state: RootState) => state.job.selectedJobId
  );
  const prevJobs = useSelector((state: RootState) => state.job.previousJobs);

  return currentJobId !== 0 ? (
    <Container style={segmentDisplay}>
      <Header style={headerStyle}>{currentJobDetails?.name}</Header>
      <div className="job-menu">
        <Image
          src="https://img.icons8.com/plasticine/100/000000/edit-property.png"
          inline
          size="mini"
          as={Link}
          to="/job/editJob"
          style={{ height: "35px" }}
        />
        <Dropdown
          trigger={menuIcon}
          options={dropdownOptions}
          pointing="top right"
          icon={null}
        />
      </div>
      <div>{currentJobDetails?.description}</div>
      {prevJobs.length > 0 ? (
        <Header as="h4">Task to do before:</Header>
      ) : (
        <Header as="h4">No prev task required to do before</Header>
      )}
      {prevJobs.map((j) => (
        <div>{j.name}</div>
      ))}
        <JobStatusButton
          status={currentJobDetails?.status}
          jobId={currentJobDetails?.jobId}
        />
      <JobComments />
    </Container>
  ) : (
    <Container style={segmentDisplay}></Container>
  );
};
