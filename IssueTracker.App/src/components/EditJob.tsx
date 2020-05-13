import React, { useEffect, useState, SyntheticEvent } from "react";
import {
  Container,
  Button,
  Form as SemanticForm,
  Input,
  Dropdown,
  DropdownItemProps,
} from "semantic-ui-react";
import { useSelector } from "react-redux";
import { RootState } from "../store/root-reducer";
import { Formik, Form, Field } from "formik";
import {
  EditedJob,
  Status,
  getEditJob,
  addProject,
  editJob,
  getUsers,
  User,
} from "./API";
import Calendar from "react-calendar";
import moment, { utc } from "moment";

export const EditJob: React.FC = () => {
  const selectedJobId = useSelector(
    (state: RootState) => state.job.selectedJobId
  );
  const [usersToAssign, setUsersToAssign] = useState<User[]>([]);
  const [users, setUsers] = useState<DropdownItemProps[]>([]);
  const usersDropdown = (users: User[]): DropdownItemProps[] =>
    users.map((u) => ({ key: u.userId, text: u.fullName, value: u.userId }));

  // const usersDropdown = [
  //   { key: 88421113, text: "Bob Smith", value: "Bob Smith" },
  //   { key: 818727, text: "Alice Smith", value: "Alice Smith" },
  // ];

  const [editedJobValues, setEditedJobValues] = useState<EditedJob>({
    name: "",
    description: "",
    assignedUserId: 0,
    jobId: selectedJobId,
    deadline: new Date(),
  });
  const initialValues: EditedJob = {
    name: editedJobValues.name,
    description: editedJobValues.description,
    assignedUserId: editedJobValues.assignedUserId,
    jobId: editedJobValues.jobId,
    deadline: editedJobValues.deadline,
  };
  useEffect(() => {
    getEditJob(selectedJobId).then((resp) => setEditedJobValues(resp));
    getUsers(selectedJobId).then((resp) => setUsersToAssign(resp));
  }, []);

  return (
    <Container>
      <Formik<EditedJob>
        initialValues={initialValues}
        onSubmit={(value) => {
          let deadline = moment(value.deadline).format();
          editJob(
            value.jobId,
            value.name,
            value.description,
            value.assignedUserId,
            deadline
          ).then((resp) => resp);
        }}
        render={(formikBag) => (
          <Form>
            <NameInput initialValues={initialValues} />
            <DescriptionInput initialValues={initialValues} />
            <AssignedUserInput usersDropdown={usersDropdown(usersToAssign)} />
            <CalendarInput />
            <Button>Save</Button>
          </Form>
        )}
      />
    </Container>
  );
};

const DescriptionInput = (props: any) => (
  <Field
    name="description"
    required
    render={({ field, form, meta }: any) => (
      <>
        <Input
          type="text"
          {...field}
          placeholder={
            props.initialValues?.description != null
              ? props.initialValues?.description
              : "Description"
          }
        />
        {meta.touched && meta.error && meta.error}
      </>
    )}
  />
);

const NameInput = (props: any) => (
  <Field
    name="name"
    required
    render={({ field, form, meta }: any) => (
      <>
        <Input type="text" {...field} placeholder={props.initialValues?.name} />
        {meta.touched && meta.error && meta.error}
      </>
    )}
  />
);

const CalendarInput = () => (
  <Field name="deadline" type="date">
    {({ field: { value }, form: { setFieldValue } }: any) => (
      <Calendar
        value={value}
        onChange={(date) => setFieldValue("deadline", date)}
      />
    )}
  </Field>
);

const AssignedUserInput = (props: any) => (
  <Field name="assignedUserId" as="select" placeholder="Assign user">
    {({ field: { value }, form: { setFieldValue } }: any) => (
      <Dropdown
        placeholder="Select user"
        fluid
        selection
        options={props.usersDropdown}
        onChange={(e, { value }) => setFieldValue("assignedUserId", value)}
      />
    )}
  </Field>
);
