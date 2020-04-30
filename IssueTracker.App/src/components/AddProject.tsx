import React, { useState, useEffect } from "react";
import {
  Container,
  Input,
  Button,
  Form as SemanticForm,
} from "semantic-ui-react";
import {
  Formik,
  FormikHelpers,
  FormikProps,
  Form,
  Field,
  FieldProps,
} from "formik";
import { addProject, Project } from "./API";

export const AddProject: React.FC = () => {
  const initialValues: Project = { id: 0, name: "" };
  const [newProjectId, setNewProjectId] = useState<number>();
  // const saveNewProject = () => {
  //   useEffect(() => {
  //   addProject("SecondTRY").then(resp => setNewProjectId(resp));
  // }, [])}

  return (
    <Container>
      <div style={{ maxWidth: "33%" }}>
        <h1>My Example</h1>
        <Formik<Project>
          initialValues={initialValues}
          onSubmit={(value) => {
            addProject(value.name).then((resp) => setNewProjectId(resp));
          }}
          render={(formikBag) => (
            <Form>
              <SemanticForm>
                <Field
                  name="name"
                  required
                  render={({ field, form, meta }: any) => (
                    <SemanticForm.Field>
                      <Input
                        type="text"
                        {...field}
                        placeholder="Project name"
                      />
                      {meta.touched && meta.error && meta.error}
                    </SemanticForm.Field>
                  )}
                />
                <Button>Save</Button>
                <div>{JSON.stringify(newProjectId)}</div>
              </SemanticForm>
            </Form>
          )}
        />
      </div>
    </Container>
  );
};

//   const [input, setInput] = useState("");
//   return (
//     <Container>
//       <form>
//         <Input
//           required
//           value={input}
//           onChange={(ev) => setInput(ev.currentTarget.value)}
//           placeholder="Search..."
//         />
//         <p>{input}</p>
//         <Button>save</Button>
//       </form>
//     </Container>
//   );
