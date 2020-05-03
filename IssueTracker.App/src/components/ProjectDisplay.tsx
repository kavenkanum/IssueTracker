import React, { useState, Props, useEffect } from "react";
import { Container, Header, Menu, MenuItemProps, Label, Button } from "semantic-ui-react";
import {Job, getJobs} from "./API";
import HeaderSubHeader from "semantic-ui-react/dist/commonjs/elements/Header/HeaderSubheader";
import { Link } from "react-router-dom";

const segmentDisplay = {
  backgroundColor: "white",
  border: "1px solid #ddd",
  height: "600px",
  margin: "0em 2em"
};

export const ProjectDisplay = (props: any) => {
  const [activeItem, setActiveItem] = useState<any>(0);
  const [jobs, setJobs] = useState<Array<Job>>([]);
  const handleItemClick = (
    e: React.MouseEvent<HTMLAnchorElement, MouseEvent>,
    { index }: MenuItemProps
  ) => setActiveItem(index);

useEffect(() => {
  getJobs(props.projectId).then(resp => setJobs(resp));
}, [props.projectId]);

  return (
    <Container style={segmentDisplay}>
      <Button primary as={Link} to="/job/addJob">Add task</Button>
      <Header>Project Name will be here</Header>
      <HeaderSubHeader>Jobs' list</HeaderSubHeader>
      <Menu vertical>
      {jobs?.map(j => <Menu.Item
          name={j.name}
          index={j.id}
          active={activeItem === j.id}
          onClick={handleItemClick}
        ><Label>{j.status}</Label>{j.name}</Menu.Item>
        )}
      </Menu>
    </Container>
  );
};
