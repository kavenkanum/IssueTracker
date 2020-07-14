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
  width: "50%",
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
  borderRadius: "25px",
  padding: "1em 5em 1em 5em",
  margin: "1em 1em 1em 0em",
  background: "#FF715B",
  color: "white",
};

export const EditJob: React.FC = () => {
  const selectedJobId = useSelector(
    (state: RootState) => state.job.selectedJobId
  );
  const currentJobDetails = useSelector(
    (state: RootState) => state.job.jobDetails
  );
  const currentProjectId = useSelector(
    (state: RootState) => state.project.selectedProjectId
  );
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
    deadline: null,
    priority: Priority.None,
  });

  useEffect(() => {
    getEditJob(selectedJobId).then((resp) => setEditedJobValues(resp));
    getUsers(selectedJobId).then((resp) => setUsersToAssign(resp));
  }, []);

  return (
    <Container style={editJobStyle}>
      <Header style={headerStyle}>{currentJobDetails?.name}</Header>
      <Formik<EditedJob>
        initialValues={editedJobValues}
        enableReinitialize={true}
        onSubmit={(value) => {
          console.log(value.deadline);
          console.log(typeof(value.deadline));
          let deadline;
          value.deadline === null
            ? (deadline = "")
            : (deadline = moment(value.deadline).format());
          let priority = +value.priority;
          editJob(
            value.jobId,
            value.name,
            value.description,
            value.assignedUserId,
            deadline,
            priority
          )
            .then(() =>
              history.push(`/dashboard/${currentProjectId}/${selectedJobId}`)
            )
            .catch((err) => {
              history.push("/error");
              console.log(`Error found - ${err}`);
            });
        }}
      >
        {(props: any) => (
          <Form>
            <NameInput initialName={props.values.name} />
            <DescriptionInput initialDescription={props.values.description} />
            <AssignedUserInput
              usersDropdown={usersDropdown(usersToAssign)}
              initialUser={props.values.assignedUserId}
            />
            <CalendarInput initialDeadline={props.values.deadline} />
            <PriorityInput
              priorityDropdown={priorityDropdown}
              initialPriority={props.values.priority}
            />
            <Button style={buttonStyle}>Save</Button>
            <Button
              style={buttonStyle}
              onClick={() =>
                history.push(`/dashboard/${currentProjectId}/${selectedJobId}`)
              }
            >
              Back
            </Button>
          </Form>
        )}
      </Formik>
    </Container>
  );
};

const NameInput = (props: any) => (
  <Field name="name" required>
    {({ field, meta }: any) => (
      <div>
        <Input
          type="text"
          {...field}
          placeholder="Name"
          style={textInputStyle}
          value={props.initialName}
        />
        {meta.touched && meta.error && meta.error}
      </div>
    )}
  </Field>
);

const DescriptionInput = (props: any) => (
  <Field name="description" required>
    {({ field, meta }: any) => (
      <div>
        <Input
          type="text"
          {...field}
          placeholder="Description"
          style={textInputStyle}
          value={props.initialDescription}
        />
        {meta.touched && meta.error && meta.error}
      </div>
    )}
  </Field>
);

const AssignedUserInput = (props: any) => (
  <Field name="assignedUserId" placeholder="Assign user">
    {({ field: { value }, form: { setFieldValue } }: any) => (
      <div>
        <Dropdown
          placeholder="Select user"
          fluid
          selection
          options={props.usersDropdown}
          onChange={(e, { value }) => setFieldValue("assignedUserId", value)}
          style={dropdownInputStyle}
          value={props.initialUser}
        />
        <p>{JSON.stringify(value)}</p>
      </div>
    )}
  </Field>
);

const CalendarInput = (props: any) => {
  const dateAlreadyClicked = (newDate: Date, savedDate: Date | null): boolean => {
    if(savedDate === null) {
      return false;
    }
    const oldDate = new Date(savedDate);
    return newDate.getTime() === oldDate.getTime();
  }
  return (
  <Field name="deadline" type="date">
    {({ field: { value }, form: { setFieldValue } }: any) => (
      <div className="custom-calendar-style">
        Select a deadline
        <Calendar
          onChange={(date: any) => {
            if(dateAlreadyClicked(date, value)) {
              setFieldValue("deadline", null)
              console.log("true");
            }
            else {
              setFieldValue("deadline", date)
              console.log("false");
            }
          }}
          locale="EN"
          value={props.initialDeadline !== null ? new Date(props.initialDeadline) : new Date()}
        />
        <p>{JSON.stringify(value)}</p>
      </div>
    )}
  </Field>
)};

const PriorityInput = (props: any) => (
  <Field name="priority" placeholder="Choose Priority">
    {({ form: { setFieldValue } }: any) => (
      <Dropdown
        placeholder="Select priority"
        fluid
        selection
        options={props.priorityDropdown}
        onChange={(e, { value }) => setFieldValue("priority", value)}
        style={dropdownInputStyle}
        value={props.initialPriority}
      />
    )}
  </Field>
);
