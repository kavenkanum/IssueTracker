import React, {  } from "react";
import { Container, Header, Button } from "semantic-ui-react";
import { useSelector } from "react-redux";
import { RootState } from "../store/root-reducer";
import { JobComments } from "./JobComments";
import { Link } from "react-router-dom";

const segmentDisplay = {
    backgroundColor: "white",
    border: "1px solid #ddd",
    height: "600px",
    margin: "0em 2em"
  };

  const headerStyle = {
    "font-size": "20px",
    padding: "1em 1em"
  };
  
export const JobDisplay = () => {
  const currentJobDetails = useSelector((state: RootState) => state.job.jobDetails);
  const currentJobId = useSelector((state: RootState) => state.job.selectedJobId);
  const prevJobs = useSelector((state:RootState) => state.job.previousJobs);

  return (currentJobId !== 0 ? 
    (<Container style={segmentDisplay}>
      <Header style={headerStyle}>{currentJobDetails?.name}</Header>
      <Header>{currentJobDetails?.descritpion}</Header>
      <Header>Task to do before:</Header>
      {prevJobs.map(j => (
        <div>{j.name}</div>
      ))}
      <Button primary as={Link} to="/job/editJob">Edit task</Button>
      <Button primary as={Link} to="/job/addPrevJobs">Previous jobs</Button>
  <p>{currentJobDetails?.descritpion}</p>
  <p>Comments:</p>
  <JobComments/>
    </Container>) : <Container style={segmentDisplay}></Container>
  );
};
