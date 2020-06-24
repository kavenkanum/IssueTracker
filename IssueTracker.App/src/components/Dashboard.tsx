import React, { useEffect } from "react";
import { Grid } from "semantic-ui-react";
import {ProjectDisplay} from "./ProjectDisplay";
import {JobDisplay} from "./JobDisplay";
import { useParams } from "react-router-dom";

const dashboardSegment = {
  padding: "0em 0em",
  margin: "0em 0em",
  width: "100%"
};

export const Dashboard: React.FC = () => {
  const { project } = useParams();
  const projectId = (project ? parseInt(project) : 0);
  
  return (
    <Grid divided="vertically" style={dashboardSegment}>
      <Grid.Row columns={2}>
        <Grid.Column style={{padding: "1em 0em 1em 2em"}}>
          <ProjectDisplay projectId={projectId}/>
        </Grid.Column>
        <Grid.Column style={{padding: "1em 0em 1em 2em"}}>
          <JobDisplay />
        </Grid.Column>
      </Grid.Row>
    </Grid>
  );
};
