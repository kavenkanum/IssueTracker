import React, { useState, useEffect } from "react";
import { Header, Button, Segment } from "semantic-ui-react";
import { Status, changeJobStatus } from "./API";
import {CustomButton} from "./elements/CustomButton";

export const JobStatusButton = (props: any) => {
  const jobStatus = () => {
    switch (props.status) {
      case 1:
        return <StartButton jobId={props.jobId}/>;
      case 2:
        return <FinishButton jobId={props.jobId}/>;
      case 3:
        return <FinishedJobElement />;
      default:
        return <Header>No status found</Header>;
    }
  };
  return <div>{jobStatus()}</div>;
};

const StartButton = (props: any) => {
  const handleStartJob = () => {
    changeJobStatus(props.jobId, Status.InProgress)
  }
  return (
    <CustomButton onClick={handleStartJob} text="Start task" />
  );
};

const FinishButton = (props: any) => {
  const handleStartJob = () => {
    changeJobStatus(props.jobId, Status.Done)
  }
  return (
    <CustomButton onClick={handleStartJob} text="Finish task"/>
  );
};

const FinishedJobElement = () => {
  return (
    <Segment style={{width: "7em"}}>Done task</Segment>
  )
}