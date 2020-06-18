import React from "react";
import { Container, Image, List, Segment } from "semantic-ui-react";

const footHeaderStyle = {
  position: "absolute",
  margin: "0em 0em 0em 0rem",
  padding: "1em 0em",
  bottom: "0",
  width: "100%",
  background: "black",
};

interface AppFooterProps {
  logo: any;
}

export const AppFooter: React.FC<AppFooterProps> = (props: AppFooterProps) => {
  return (
      <Container textAlign="center" style={footHeaderStyle}>
        <Image src={props.logo} centered size="mini" />
        <List horizontal inverted divided link size="small">
          <List.Item as="a" href="#">
            Site Map
          </List.Item>
          <List.Item as="a" href="#">
            Contact Us
          </List.Item>
          <List.Item as="a" href="#">
            Terms and Conditions
          </List.Item>
          <List.Item as="a" href="#">
            Privacy Policy
          </List.Item>
        </List>
      </Container>
  );
};
