import React, { useState, useEffect } from "react";
import {
  Container,
  Button,
  Dropdown,
  DropdownItemProps,
  Header,
  List,
} from "semantic-ui-react";
import { Formik, Form, FieldArray } from "formik";
import { RootState } from "../store/root-reducer";
import { useSelector } from "react-redux";
import { addPrevJobs, getAvailablePrevJobs } from "./API";
import { useHistory } from "react-router-dom";

type Jobs = {
  jobs: {
    jobId: number;
    name: string;
  }[];
};

const addPrevjobsStyle = {
  background: "white",
  padding: "1em",
  margin: "2em",
  border: "1px solid #ddd",
  height: "90%",
  width: "50%",
};

const buttonStyle = {
  borderRadius: "25px",
  padding: "1em 5em 1em 5em",
  marginTop: "1em",
  background: "#FF715B",
  color: "white",
};

export const AddPrevJob: React.FC = () => {
  const currentJobId = useSelector(
    (state: RootState) => state.job.selectedJobId
  );
  const currentJobDetails = useSelector(
    (state: RootState) => state.job.jobDetails
  );
  const prevJobs = useSelector((state: RootState) => state.job.previousJobs);
  const currentProjectId = useSelector(
    (state: RootState) => state.project.selectedProjectId
  );
  const [jobs, setJobs] = useState<Jobs["jobs"]>([]);
  const initialValues: Jobs = {
    jobs: jobs.map((j) => ({
      jobId: j.jobId,
      name: j.name,
    })),
  };

  const history = useHistory();
  const dropdownJobs: DropdownItemProps[] = jobs.map((j) => ({
    text: j.name,
    value: j.jobId,
    key: j.jobId,
  }));

  useEffect(() => {
    getAvailablePrevJobs(currentJobId).then((resp) => setJobs(resp));
  }, []);

  return (
    <Container style={addPrevjobsStyle}>
      <Header>Add previous jobs</Header>
      <Header as="h4" style={{ margin: "1em 0em 0em 0em" }}>
        {currentJobDetails?.name}
      </Header>
      {prevJobs.length > 0 ? (
        <Header as="h5" style={{ margin: "1em 0em 0em 0em" }}>
          Task to do before:
        </Header>
      ) : (
        <Header as="h5" style={{ margin: "1em 0em 0em 0em" }}>
          No prev task required to do before
        </Header>
      )}
      <List>
        {prevJobs.map((j) => (
          <List.Item>
            <List.Icon name="chevron right" />
            <List.Content>{j.name}</List.Content>
          </List.Item>
        ))}
      </List>
      <Formik<Jobs>
        initialValues={initialValues}
        onSubmit={(data, { setSubmitting }) => {
          setSubmitting(true);
          addPrevJobs(currentJobId, data.jobs.flat())
            .then(() => {
              history.push(`/dashboard/${currentProjectId}`);
            })
            .catch((err) => {
              history.push("/error");
              console.log(`Error found - ${err}`);
            });
          setSubmitting(false);
        }}
      >
        {({ values, isSubmitting, handleSubmit }) => (
          <Form onSubmit={handleSubmit}>
            <FieldArray name="jobs">
              {(arrayHelpers) => (
                <div>
                  <Dropdown
                    placeholder="Jobs"
                    fluid
                    multiple
                    selection
                    options={dropdownJobs}
                    onChange={(event, data) =>
                      arrayHelpers.replace(0, data.value)
                    }
                  />
                </div>
              )}
            </FieldArray>
            <Button disabled={isSubmitting} type="submit" style={buttonStyle}>
              Submit
            </Button>
            <p>{JSON.stringify(values)}</p>
          </Form>
        )}
      </Formik>
    </Container>
  );
};
