import React, { useEffect, useState } from "react";
import {
  Container,
  Button,
  Input,
  Dropdown,
  DropdownItemProps,
} from "semantic-ui-react";
import { useSelector } from "react-redux";
import { RootState } from "../store/root-reducer";
import { Formik, Form, Field } from "formik";
import {
  EditedJob,
  Priority,
  getEditJob,
  editJob,
  getUsers,
  User,
} from "./API";
import Calendar from "react-calendar";
import moment from "moment";

export const EditJob: React.FC = () => {
  const selectedJobId = useSelector(
    (state: RootState) => state.job.selectedJobId
  );
  const [usersToAssign, setUsersToAssign] = useState<User[]>([]);
  const [] = useState<DropdownItemProps[]>([]);
  const usersDropdown = (users: User[]): DropdownItemProps[] =>
    users.map((u) => ({ key: u.userId, text: u.fullName, value: u.userId }));

  const [editedJobValues, setEditedJobValues] = useState<EditedJob>({
    name: "",
    description: "",
    assignedUserId: 0,
    jobId: selectedJobId,
    deadline: new Date(),
    priority: Priority.None,
  });
  const initialValues: EditedJob = {
    name: editedJobValues.name,
    description: editedJobValues.description,
    assignedUserId: editedJobValues.assignedUserId,
    jobId: editedJobValues.jobId,
    deadline: editedJobValues.deadline,
    priority: editedJobValues.priority,
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
          let priority = +value.priority;
          console.log(typeof(priority));
          editJob(
            value.jobId,
            value.name,
            value.description,
            value.assignedUserId,
            deadline,
            priority
          ).then((resp) => resp);
        }}
        render={() => (
          <Form>
            <NameInput initialValues={initialValues} />
            <DescriptionInput initialValues={initialValues} />
            <AssignedUserInput usersDropdown={usersDropdown(usersToAssign)} />
            <CalendarInput />
            <PriorityInput />
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
    render={({ field, meta }: any) => (
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
    render={({ field, meta }: any) => (
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
    {({ form: { setFieldValue } }: any) => (
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

const PriorityInput = () => (
  <Field name="priority" as="select" placeholder="Choose Priority">
        <option value={Priority.None}>{Priority[Priority.None]}</option>
        <option value={Priority.Low}>{Priority[Priority.Low]}</option>
        <option value={Priority.Medium}>{Priority[Priority.Medium]}</option>
        <option value={Priority.High}>{Priority[Priority.High]}</option>
  </Field>
);
