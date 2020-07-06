import React, { useEffect, useState } from "react";
import {
  Container,
  Button,
  Input,
  Dropdown,
  DropdownItemProps,
  Header,
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
import "react-calendar/dist/Calendar.css";
import { useHistory } from "react-router-dom";

const editJobStyle = {
  background: "white",
  padding: "1em",
  margin: "2em",
  border: "1px solid #ddd",
  height: "90%",
  width: "50%"
};

const headerStyle = {
  fontSize: "20px",
  display: "inline",
};
const textInputStyle = {
  padding: "1em 0em 0em 0em",
  width: "100%",
};

const dropdownInputStyle = {
  padding: "12px 0em 0em 1em",
  margin: "1em 0em 0em",
  width: "100%",
};
const buttonStyle = {
  "border-radius": "25px",
  padding: "1em 5em 1em 5em",
  margin: "1em 1em 1em 0em",
  background: "#FF715B",
  color: "white"
};

export const EditJob: React.FC = () => {
  const selectedJobId = useSelector(
    (state: RootState) => state.job.selectedJobId
  );
  const currentJobDetails = useSelector(
    (state: RootState) => state.job.jobDetails
  );
  const currentProjectId = useSelector((state: RootState) => state.project.selectedProjectId)
  const [usersToAssign, setUsersToAssign] = useState<User[]>([]);
  const history = useHistory();
  const usersDropdown = (users: User[]): DropdownItemProps[] =>
    users.map((u) => ({ key: u.userId, text: u.fullName, value: u.userId }));
  const priorityDropdown = [
    { key: Priority.None, text: Priority[Priority.None], value: Priority.None },
    { key: Priority.Low, text: Priority[Priority.Low], value: Priority.Low },
    {
      key: Priority.Medium,
      text: Priority[Priority.Medium],
      value: Priority.Medium,
    },
    { key: Priority.High, text: Priority[Priority.High], value: Priority.High },
  ];

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
    <Container style={editJobStyle}>
      
    <Header style={headerStyle}>CURRENT PROJ ID {currentProjectId}</Header>
      <Header style={headerStyle}>{currentJobDetails?.name}</Header>
      <Formik<EditedJob>
        initialValues={initialValues}
        onSubmit={(value) => {
          let deadline = moment(value.deadline).format();
          let priority = +value.priority;
          editJob(
            value.jobId,
            value.name,
            value.description,
            value.assignedUserId,
            deadline,
            priority
          ).then((resp) => resp);      
          history.push(`/dashboard/${currentProjectId}/${selectedJobId}`);
        }}
        render={() => (
          <Form>
            <NameInput initialValues={initialValues} />
            <DescriptionInput initialValues={initialValues} />
            <AssignedUserInput usersDropdown={usersDropdown(usersToAssign)} />
            <CalendarInput />
            <PriorityInput priorityDropdown={priorityDropdown} />
            <Button style={buttonStyle}>Save</Button><Button style={buttonStyle} onClick={() => history.push(`/dashboard/${currentProjectId}/${selectedJobId}`)}>Back</Button>
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
      <div>
        <Input
          type="text"
          {...field}
          placeholder={
            props.initialValues?.description != null
              ? props.initialValues?.description
              : "Description"
          }
          style={textInputStyle}
        />
        {meta.touched && meta.error && meta.error}
      </div>
    )}
  />
);

const NameInput = (props: any) => (
  <Field
    name="name"
    required
    render={({ field, meta }: any) => (
      <div>
        <Input
          type="text"
          {...field}
          placeholder={props.initialValues?.name}
          style={textInputStyle}
        />
        {meta.touched && meta.error && meta.error}
      </div>
    )}
  />
);

const CalendarInput = () => (
  <Field name="deadline" type="date">
    {({ field: { value }, form: { setFieldValue } }: any) => (
      <div className="custom-calendar-style">
        Select a deadline
        <Calendar
          value={value}
          onChange={(date) => setFieldValue("deadline", date)}
          locale="EN"
        />
      </div>
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
        style={dropdownInputStyle}
      />
    )}
  </Field>
);

const PriorityInput = (props: any) => (
  <Field name="priority" as="select" placeholder="Choose Priority">
    {({ form: { setFieldValue } }: any) => (
      <Dropdown
        placeholder="Select priority"
        fluid
        selection
        options={props.priorityDropdown}
        onChange={(e, { value }) => setFieldValue("priority", value)}
        style={dropdownInputStyle}
      />
    )}
  </Field>
);
