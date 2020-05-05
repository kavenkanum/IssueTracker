import React, { useState, useEffect } from "react";
import { Container, Header, Menu, MenuItemProps } from "semantic-ui-react";
import { useSelector } from "react-redux";
import { RootState } from "../store/root-reducer";
import { JobComments } from "./JobComments";

const segmentDisplay = {
    backgroundColor: "white",
    border: "1px solid #ddd",
    height: "600px",
    margin: "0em 2em"
  };
  
export const JobDisplay = (props: any) => {
  const currentJobDetails = useSelector((state: RootState) => state.job.jobDetails);
  const currentJobId = useSelector((state: RootState) => state.job.selectedJobId);

  return (currentJobId !== 0 ? 
    (<Container style={segmentDisplay}>
      <Header>{currentJobDetails?.name}</Header>
  <p>{currentJobDetails?.descritpion}</p>
  <p>Comments:</p>
  <JobComments/>
    </Container>) : <Container style={segmentDisplay}></Container>
  );
};
