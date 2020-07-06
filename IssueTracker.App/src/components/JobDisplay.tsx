import React, { useEffect } from "react";
import { Container, Header, List, Image, Dropdown } from "semantic-ui-react";
import { useSelector, useDispatch } from "react-redux";
import { RootState } from "../store/root-reducer";
import { JobComments } from "./JobComments";
import { Link, useParams } from "react-router-dom";
import { Status } from "./API";
import { JobStatusButton } from "./JobStatusButton";
import slice, { loadJobDetails } from "../features/jobs/slice";
import moment from "moment";

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

export const JobDisplay = (props: any) => {
  const currentJobDetails = useSelector(
    (state: RootState) => state.job.jobDetails
  );
  const currentJobId = useSelector(
    (state: RootState) => state.job.selectedJobId
  );
  const prevJobs = useSelector((state: RootState) => state.job.previousJobs);
  const dispatch = useDispatch();
  const { job } = useParams();
  const jobId = job ? parseInt(job) : 0;
  const dupa = "wednesday";

  useEffect(() => {
    dispatch(loadJobDetails(jobId, props.projectId));
  }, [jobId]);

  return currentJobId !== 0 ? (
    <Container style={segmentDisplay}>
      <Header style={headerStyle}>{currentJobDetails?.name}</Header>
      <div className="job-menu">
        <Image
          src="https://img.icons8.com/plasticine/100/000000/calendar.png"
          inline
          size="mini"
          style={{ height: "35px" }}
        />
        <span>{moment(currentJobDetails?.deadlineDate).format("MMMM Do ")}</span>
        {currentJobDetails?.status !== Status.Done ? (
          <Image
            src="https://img.icons8.com/plasticine/100/000000/edit-property.png"
            inline
            size="mini"
            as={Link}
            to="/job/editJob"
            style={{ height: "35px" }}
            title="Edit job"
          />
        ) : (
          <></>
        )}
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
      <List>
        {prevJobs.map((j) => (
          <List.Item>
            <List.Icon name="chevron right" />
            <List.Content>{j.name}</List.Content>
          </List.Item>
        ))}
      </List>
      <JobStatusButton
        status={currentJobDetails?.status}
        jobId={currentJobDetails?.jobId}
      />
      <JobComments />
    </Container>
  ) : (
    <Container></Container>
  );
};
