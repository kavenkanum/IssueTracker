import React, { useState } from "react";
import {
  Container,
  Input,
  Button,
  Form as SemanticForm,
} from "semantic-ui-react";
import { Formik, Form, Field } from "formik";
import { NewJob, addJob } from "./API";
import { useSelector } from "react-redux";
import { RootState } from "../store/root-reducer";
import { Link, Redirect, useHistory } from "react-router-dom";


export const AddJob: React.FC = () => {
  const currentProjectId = useSelector(
    (state: RootState) => state.project.selectedProjectId
  );
  const initialValues = { id: 0, name: "", projectId: 0 };
  const [newJobId, setNewJobId] = useState<number>(0);
  const [isSubmitted, setSubmit] = useState(false);
  const history = useHistory();

  return (
    <Container>
      <h1>Add new task</h1>
      <Formik<NewJob>
        initialValues={initialValues}
        onSubmit={(value) => {
          addJob(currentProjectId, value.name).then((resp) =>
          setNewJobId(resp));
          setSubmit(true);
          history.push(`/dashboard/${currentProjectId}`);
        }}
        render={(formikBag) => (
          <Form>
            <SemanticForm>
              <Field
                name="name"
                required
                render={({ field, form, meta }: any) => (
                  <SemanticForm.Field>
                    <Input type="text" {...field} placeholder="Task name" />
                    {meta.touched && meta.error && meta.error}
                  </SemanticForm.Field>
                )}
              />
              <Button disabled={isSubmitted}>Save</Button>
            </SemanticForm>
          </Form>
        )}
      />
    </Container>
  );
};
