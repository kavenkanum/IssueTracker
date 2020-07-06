import React from "react";
import { Button } from "semantic-ui-react";
import { Link } from "react-router-dom";

const buttonStyle = {
  "border-radius": "25px",
  padding: "1em 5em 1em 5em",
  background: "#FF715B",
};

export const CustomButton = (props: any) => {
  return (
    <div className="custom-button">
      <Button
        primary
        as={Link}
        to={props.href}
        style={buttonStyle}
        onClick={props.onClick}
      >
        {props.text}
      </Button>
    </div>
  );
};
