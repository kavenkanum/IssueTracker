import React, { useState } from "react";
import {
  Container,
  Input,
  Button,
  Form as SemanticForm,
  Header,
} from "semantic-ui-react";
import {
  Formik,
  Form,
  Field,
} from "formik";
import { addProject} from "./API";
import {Project} from "../features/projects/slice";

const addProjStyle = {
  background: "white",
  padding: "1em",
  margin: "2em",
  border: "1px solid #ddd",
  height: "90%",
  width: "50%"
};

const buttonStyle = {
  "border-radius": "25px",
  padding: "1em 5em 1em 5em",
  background: "#FF715B",
  color: "white"
};

export const AddProject: React.FC = () => {
  const initialValues: Project = { id: 0, name: "" };
  const [newProjectId, setNewProjectId] = useState<number>();

  return (
    <Container style={addProjStyle}>
        <Header>Add new project</Header>
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
                <Button style={buttonStyle}>Save</Button>
                <div>{JSON.stringify(newProjectId)}</div>
              </SemanticForm>
            </Form>
          )}
        />
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
