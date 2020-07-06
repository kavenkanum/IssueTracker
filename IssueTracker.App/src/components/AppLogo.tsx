import React from "react";
import { Header, Image } from "semantic-ui-react";

const appLogoStyle = {
  color: "white",
  "font-family": "Lucida Console, Courier, monospace",
  "padding-left": "10px"
};

export const AppLogo: React.FC = () => {
  return (
    <Header as="h1" style={appLogoStyle}>
      <Image src="https://img.icons8.com/plasticine/100/000000/dog-footprint.png" />
      PugTrack
    </Header>
  );
};
