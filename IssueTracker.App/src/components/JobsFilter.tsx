import React from "react";
import { Status } from "./API";
import { Dropdown } from "semantic-ui-react";

const jobsStatusOptions = [
  { key: 1, text: "New", value: Status.New },
  { key: 2, text: "In progress", value: Status.InProgress },
  { key: 3, text: "Done", value: Status.Done },
  { key: 4, text: "None", value: Status.None },
];

export const JobsFilter = (props: any) => {
  return (
    <div>
        Show tasks with status: {" "}
      <Dropdown inline options={jobsStatusOptions} defaultValue={jobsStatusOptions[3].value} onChange={(event, data) => props.onChange(data.value)}/>
    </div>
  );
};
