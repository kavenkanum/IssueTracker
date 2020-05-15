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
  
export const JobDisplay = () => {
  const currentJobDetails = useSelector((state: RootState) => state.job.jobDetails);
  const currentJobId = useSelector((state: RootState) => state.job.selectedJobId);

  return (currentJobId !== 0 ? 
    (<Container style={segmentDisplay}>
      <Header>{currentJobDetails?.name}</Header>
      <Button primary as={Link} to="/job/editJob">Edit task</Button>
  <p>{currentJobDetails?.descritpion}</p>
  <p>Comments:</p>
  <JobComments/>
    </Container>) : <Container style={segmentDisplay}></Container>
  );
};
