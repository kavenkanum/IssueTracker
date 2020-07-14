import React from "react";
import { Header, Image } from "semantic-ui-react";

const appLogoStyle = {
  color: "white",
  fontFamily: "Lucida Console, Courier, monospace",
  paddingLeft: "10px"
};

export const AppLogo: React.FC = () => {
  return (
    <Header as="h1" style={appLogoStyle}>
      <Image src="https://img.icons8.com/plasticine/100/000000/dog-footprint.png" />
      PugTrack
    </Header>
  );
};
