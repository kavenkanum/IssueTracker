import React, {  } from "react";
import { Grid } from "semantic-ui-react";
import {ProjectDisplay} from "./ProjectDisplay";
import {JobDisplay} from "./JobDisplay";
import { useParams } from "react-router-dom";

const dashboardSegment = {
  padding: "1em 0em"
};

export const Dashboard: React.FC = () => {
  const { project } = useParams();
  //const projectId = useSelector(getProjectId);
  const projectId = (project ? parseInt(project) : 0)
  
  return (
    <Grid divided="vertically" style={dashboardSegment}>
      <Grid.Row columns={2}>
        <Grid.Column style={{padding: "1em 1em"}}>
          <ProjectDisplay projectId={projectId}/>
        </Grid.Column>
        <Grid.Column style={{padding: "1em 1em"}}>
          <JobDisplay projectId={projectId}/>
        </Grid.Column>
      </Grid.Row>
    </Grid>
  );
};
