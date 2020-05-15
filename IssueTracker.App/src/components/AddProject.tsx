import React, { useState } from "react";
import {
  Container,
  Input,
  Button,
  Form as SemanticForm,
} from "semantic-ui-react";
import {
  Formik,
  Form,
  Field,
} from "formik";
import { addProject} from "./API";
import {Project} from "../features/projects/slice";

export const AddProject: React.FC = () => {
  const initialValues: Project = { id: 0, name: "" };
  const [newProjectId, setNewProjectId] = useState<number>();

  return (
    <Container>
      <div style={{ maxWidth: "33%" }}>
        <h1>Add new project</h1>
        <Formik<Project>
          initialValues={initialValues}
          onSubmit={(value) => {
            addProject(value.name).then((resp) => setNewProjectId(resp));
          }}
          render={() => (
            <Form>
              <SemanticForm>
                <Field
                  name="name"
                  required
                  render={({ field, meta }: any) => (
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
