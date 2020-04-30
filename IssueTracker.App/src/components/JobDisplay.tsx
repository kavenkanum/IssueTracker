import React, { useState } from "react";
import { Container, Header, Menu, MenuItemProps } from "semantic-ui-react";

const segmentDisplay = {
    backgroundColor: "white",
    border: "1px solid #ddd",
    height: "600px",
    margin: "0em 2em"
  };
  
export const JobDisplay = (props: any) => {
  const [activeItem, setActiveItem] = useState<any>(0);
  const handleItemClick = (
    e: React.MouseEvent<HTMLAnchorElement, MouseEvent>,
    { index }: MenuItemProps
  ) => setActiveItem(index);

  return (
    <Container style={segmentDisplay}>
      <Header>Job Name will be here</Header>
        <p>Description of the job</p>
    </Container>
  );
};
