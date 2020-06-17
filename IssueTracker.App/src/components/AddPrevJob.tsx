import React, { useState, useEffect, useRef } from "react";
import {
  Container,
  Button,
  Dropdown,
  DropdownItemProps,
  Input,
  Checkbox,
  Radio,
  Item,
} from "semantic-ui-react";
import { Select, MenuItem } from "@material-ui/core";
import {
  Formik,
  Field,
  FieldArray,
  SharedRenderProps,
  FieldArrayRenderProps,
  FieldAttributes,
  useField,
  FieldConfig,
} from "formik";
import * as yup from "yup";
import { RootState } from "../store/root-reducer";
import { useSelector } from "react-redux";
import { getJobs, addPrevJobs } from "./API";
import { useHistory, Redirect } from "react-router-dom";

type Jobs = {
  jobs: {
    jobId: number;
    name: string;
  }[];
};

export const AddPrevJob: React.FC = () => {
  const currentJobId = useSelector(
    (state: RootState) => state.job.selectedJobId
  );
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
  const dropdownJobs: DropdownItemProps[] = jobs
    .filter((j) => j.jobId !== currentJobId)
    .map((j) => ({
      text: j.name,
      value: j.jobId,
      key: j.jobId,
    }));

  useEffect(() => {
    getJobs(currentProjectId).then((resp) => setJobs(resp));
  }, []);

  return (
    <Container>
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
          <form onSubmit={handleSubmit}>
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
            <Button disabled={isSubmitting} type="submit">
              Submit
            </Button>
            <pre>{JSON.stringify(values, null, 2)}</pre>
            <pre>JOBS {JSON.stringify(values.jobs.flat(), null, 2)}</pre>
          </form>
        )}
      </Formik>
    </Container>
  );
};
